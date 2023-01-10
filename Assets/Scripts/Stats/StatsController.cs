using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsController : MonoBehaviour
{
    // Public
    //public int PlayerHealth { get; private set; } = 100;

    // Serialize
    [SerializeField] private GameObject _player;
    [SerializeField] private int _bulletDamage = 10;
    [SerializeField] private float _baseSize = 10.0f;

    // Private
    private int PlayerHealth = 100;
    private bool _isDeath;
    private bool _isFinish;

    private void Awake()
    {
        //PlayerHealth = 100;
        Bullet.OnBulletIncrease.AddListener(DecreaseHealth);
    }

    private void Update()
    {
        if (PlayerHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //Debug.Log(PlayerHealth);
    }

    public void DecreaseHealth()
    {
        PlayerHealth -= _bulletDamage;
        _baseSize--;
        _player.transform.localScale = new Vector3(_baseSize, _baseSize, _baseSize);
        Debug.Log(PlayerHealth);
    }
}
