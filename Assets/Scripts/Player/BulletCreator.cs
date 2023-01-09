using UnityEngine;
using UnityEngine.Pool;

public class BulletCreator : MonoBehaviour
{
    // Serialize
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _spawnPoint;

    // Private
    private IObjectPool<Bullet> _bulletPool;

    private void Awake()
    {
        _bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            GetBullet,
            ReleaseBullet,
            DestroyBullet,
            maxSize: 4
            );
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _bulletPool.Get();
        }
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(_bulletPrefab);
        bullet.SetPool(_bulletPool);
        return bullet;
    }

    private void GetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.transform.position = _spawnPoint.position;
        bullet.transform.rotation = _spawnPoint.rotation;
    }

    private void ReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void DestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
