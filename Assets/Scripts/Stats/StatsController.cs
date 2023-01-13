using UnityEngine;
using UnityEngine.Events;

public class StatsController : MonoBehaviour
{
    // Public
    public static UnityEvent OnPlayerDeath = new UnityEvent();
    public int PlayerHealth { get; private set; } = 100;

    // Serialize
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private int _bulletDamage = 10;
    [SerializeField] private float _baseSize = 10.0f;

    private void Awake()
    {
        Bullet.OnBulletIncrease.AddListener(DecreaseHealth);
    }

    private void Update()
    {
        if (PlayerHealth <= 0)
        {
            OnPlayerDeath?.Invoke();
        }
    }

    private void DecreaseHealth()
    {
        PlayerHealth -= _bulletDamage;
        _baseSize--;
        _playerObject.transform.localScale = new Vector3(_baseSize, _baseSize, _baseSize);
    }
}
