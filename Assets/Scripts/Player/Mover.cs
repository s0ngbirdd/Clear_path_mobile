using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mover : MonoBehaviour
{
    // Public
    public static UnityEvent OnPlayerMove = new UnityEvent();

    // Private
    private Vector3 _colliderPosition;

    private void Awake()
    {
        Bullet.OnBulletImpact.AddListener(StoreCollisionInformatin);
        Obstacle.OnObstacleDestruction.AddListener(Move);
    }

    public void StoreCollisionInformatin(Vector3 colliderPosition)
    {
        _colliderPosition = colliderPosition;
    }

    public void Move()
    {
        transform.position = new Vector3(_colliderPosition.x, transform.position.y, _colliderPosition.z);
        OnPlayerMove?.Invoke();
    }
}
