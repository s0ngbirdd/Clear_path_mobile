using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    // Serialize
    [SerializeField] private string _collisionLayerName = "Obstacle";
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _baseSize = 1.0f;
    [SerializeField] private float _timeGap = 1.0f;

    // Private
    private IObjectPool<Bullet> _bulletPool;

    private bool _isCoroutineEnd = true;
    private bool _isBulletLaunched;
    private Coroutine _coroutine;


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
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer(_collisionLayerName)))
        {
            Destroy(collision.gameObject);
            ResetFields();
            _bulletPool.Release(this);
        }
    }

    private IEnumerator IncreaseSize()
    {
        yield return new WaitForSeconds(_timeGap);

        _baseSize++;
        transform.localScale = new Vector3(_baseSize, _baseSize, _baseSize);
        _isCoroutineEnd = true;
    }

    private void ResetFields()
    {
        _baseSize = 1.0f;
        transform.localScale = new Vector3(_baseSize, _baseSize, _baseSize);
        _isBulletLaunched = false;
        _isCoroutineEnd = true;
    }
}
