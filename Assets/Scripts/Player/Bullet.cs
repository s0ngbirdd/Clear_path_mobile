using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public static UnityEvent OnBulletIncrease = new UnityEvent();
    public static UnityEvent<Vector3> OnBulletImpact = new UnityEvent<Vector3>();


    // Serialize
    //[SerializeField] private string _collisionLayerName = "Obstacle";
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _baseSize = 1.0f;
    [SerializeField] private float _timeGap = 1.0f;

    [SerializeField] private Color _infectionColor;
    [SerializeField] private float _timeBeforeDestruction = 0.5f;

    //[SerializeField] private UnityEvent _onBulletIncrease = new UnityEvent();

    // Private
    private IObjectPool<Bullet> _bulletPool;

    private bool _isCoroutineEnd = true;
    private bool _isBulletLaunched;
    private Coroutine _coroutine;
    private SphereCollider _sphereCollider;

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        //_onBulletIncrease = new UnityEvent();
    }


    public void SetPool(IObjectPool<Bullet> pool)
    {
        _bulletPool = pool;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !_isBulletLaunched)
        {
            if (_isCoroutineEnd)
            {
                _coroutine = StartCoroutine(IncreaseSize());
                _isCoroutineEnd = false;
            }
        }
        else
        {
            StopCoroutine(_coroutine);
            _isBulletLaunched = true;
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
        //transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.layer.Equals(LayerMask.NameToLayer(_collisionLayerName)))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _sphereCollider.radius, _layerMask);

            foreach (Collider collider in hitColliders)
            {
                Destroy(collider.gameObject);
            }

            //Destroy(collision.gameObject);
            ResetFields();
            _bulletPool.Release(this);
        }*/

        //OnBulletImpact?.Invoke(collision.gameObject.transform.position);
        OnBulletImpact?.Invoke(transform.position);

        Collider[] hitColliders = Physics.OverlapSphere(collision.transform.position, _sphereCollider.radius * _baseSize, _layerMask);

        InfectColliders(hitColliders);

        /*foreach (Collider collider in hitColliders)
        {
            collider.gameObject.GetComponent<MeshRenderer>().material.color = _infectionColor;
            Destroy(collider.gameObject, 1f);
        }*/



        //DeleteColliders();
        //Destroy(collision.gameObject);


        ResetFields();
        _bulletPool.Release(this);


        //OnBulletImpact?.Invoke();
        //hitColliders = null;
    }

    private IEnumerator IncreaseSize()
    {
        yield return new WaitForSeconds(_timeGap);

        _baseSize++;
        transform.localScale = new Vector3(_baseSize, _baseSize, _baseSize);
        _isCoroutineEnd = true;

        OnBulletIncrease?.Invoke();
    }

    private void ResetFields()
    {
        _baseSize = 1.0f;
        transform.localScale = new Vector3(_baseSize, _baseSize, _baseSize);
        _isBulletLaunched = false;
        _isCoroutineEnd = true;
    }
    private void InfectColliders(Collider[] colliders)
    {
        foreach (Collider collider in colliders)
        {
            collider.gameObject.GetComponent<MeshRenderer>().material.color = _infectionColor;
            //Destroy(collider.gameObject, _timeBeforeDestruction);
        }

        //StartCoroutine(DestroyColliders(colliders));
        //OnBulletImpact?.Invoke();

    }

    private IEnumerator DestroyColliders(Collider[] colliders)
    {
        yield return new WaitForSeconds(_timeBeforeDestruction);

        foreach (Collider collider in colliders)
        {
            Destroy(collider.gameObject);
        }

        ResetFields();
        _bulletPool.Release(this);

        //OnBulletImpact?.Invoke();
    }
}
