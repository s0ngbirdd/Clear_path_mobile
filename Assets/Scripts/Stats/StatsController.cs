using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class StatsController : MonoBehaviour
{
    // Public
    public static UnityEvent OnPlayerDeath = new UnityEvent();

    // Serialize
    [SerializeField] private GameObject _player;
    [SerializeField] private int _bulletDamage = 10;
    [SerializeField] private float _baseSize = 10.0f;

    // Private
    private int _playerHealth = 100;
    private bool _isDeath;
    private bool _isFinish;

    private void Awake()
    {
        Bullet.OnBulletIncrease.AddListener(DecreaseHealth);
    }

    private void Update()
    {
        if (_playerHealth <= 0)
        {
            OnPlayerDeath?.Invoke();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void DecreaseHealth()
    {
        _playerHealth -= _bulletDamage;
        _baseSize--;
        _player.transform.localScale = new Vector3(_baseSize, _baseSize, _baseSize);
        Debug.Log(_playerHealth);
    }
}
