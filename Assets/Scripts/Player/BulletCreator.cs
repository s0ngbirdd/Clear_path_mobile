using UnityEngine;
using UnityEngine.Pool;

public class BulletCreator : MonoBehaviour
{
    // Serialize
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _spawnPoint;

    // Private
    private IObjectPool<Bullet> _bulletPool;
    private bool _canLaunchBullet = true;
    private ParentBullet _parentObject;

    private void Awake()
    {
        Mover.OnPlayerMove.AddListener(UnlockLaunchBullet);
        Bullet.OnOtherImpact.AddListener(UnlockLaunchBullet);
        FinishZone.OnFinishEnter.AddListener(LockLaunchBullet);
        StatsController.OnPlayerDeath.AddListener(LockLaunchBullet);

        _bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            GetBullet,
            ReleaseBullet,
            DestroyBullet,
            maxSize: 4
            );
    }

    private void Start()
    {
        _parentObject = FindObjectOfType<ParentBullet>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _canLaunchBullet)
        {
            _bulletPool.Get();
            LockLaunchBullet();
        }
    }

    private void LockLaunchBullet()
    {
        _canLaunchBullet = false;
    }

    private void UnlockLaunchBullet()
    {
        _canLaunchBullet = true;
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(_bulletPrefab);
        bullet.SetPool(_bulletPool);
        bullet.transform.SetParent(_parentObject.transform, true);
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
