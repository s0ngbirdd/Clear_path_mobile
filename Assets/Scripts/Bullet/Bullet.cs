using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    // Public
    public static UnityEvent OnBulletIncrease = new UnityEvent();
    public static UnityEvent<Vector3> OnObstacleImpact = new UnityEvent<Vector3>();
    public static UnityEvent OnOtherImpact = new UnityEvent();

    // Serialize
    [SerializeField] private string _collisionLayerMask = "Obstacle";
    [SerializeField] private LayerMask _overlapLayerMask;
    [SerializeField] private float _speed = 20.0f;
    [SerializeField] private float _baseSize = 5.0f;
    [SerializeField] private float _timeBeforeIncrease = 1.0f;
    [SerializeField] private Color _infectionColor;
    [SerializeField] private float _timeBeforeDestroy = 0.5f;

    // Private
    private IObjectPool<Bullet> _bulletPool;
    private bool _isCoroutineEnd = true;
    private bool _isBulletLaunched;
    private Coroutine _coroutine;
    private SphereCollider _sphereCollider;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();

        StatsController.OnPlayerDeath.AddListener(DestoryBullet);
        FinishZone.OnFinishEnter.AddListener(DestoryBullet);
    }

    private void OnEnable()
    {
        IncreaseSize();
        _isCoroutineEnd = false;
    }

    private void Update()
    {
        // Charge bullet
        if (Input.GetMouseButton(0) && !_isBulletLaunched)
        {
            if (_isCoroutineEnd)
            {
                IncreaseSize();
                _isCoroutineEnd = false;
            }
        }
        // Launch bullet
        else
        {
            StopCoroutine(_coroutine);
            _isBulletLaunched = true;
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Infect obstacles in radius
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer(_collisionLayerMask)))
        {
            OnObstacleImpact?.Invoke(transform.position);
            Collider[] hitColliders = Physics.OverlapSphere(collision.transform.position, _sphereCollider.radius * _baseSize, _overlapLayerMask);
            InfectColliders(hitColliders);
        }
        else
        {
            OnOtherImpact?.Invoke();
        }

        ResetFields();
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        _bulletPool = pool;
    }

    private void IncreaseSize()
    {
        _baseSize++;
        transform.localScale = new Vector3(_baseSize, _baseSize, _baseSize);
        OnBulletIncrease?.Invoke();
        _coroutine = StartCoroutine(WaitToIncreaseSize());
    }

    private IEnumerator WaitToIncreaseSize()
    {
        yield return new WaitForSeconds(_timeBeforeIncrease);
        _isCoroutineEnd = true;
    }

    private void ResetFields()
    {
        _baseSize = 5.0f;
        transform.localScale = new Vector3(_baseSize, _baseSize, _baseSize);
        _isBulletLaunched = false;
        _isCoroutineEnd = true;
        _bulletPool.Release(this);
    }

    private void DestoryBullet()
    {
        Destroy(gameObject);
    }

    private void InfectColliders(Collider[] colliders)
    {
        foreach (Collider collider in colliders)
        {
            collider.gameObject.GetComponent<MeshRenderer>().material.color = _infectionColor;
            Destroy(collider.gameObject, _timeBeforeDestroy);
        }
    }
}
