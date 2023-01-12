using UnityEngine;
using UnityEngine.Events;

public class Mover : MonoBehaviour
{
    // Public
    public static UnityEvent OnPlayerMove = new UnityEvent();

    // Serialize
    [SerializeField] private Transform _objectToLookAt;

    // Private
    private Vector3 _colliderPosition;
    private SphereCollider _sphereCollider;

    private void Awake()
    {
        Bullet.OnObstacleImpact.AddListener(StoreCollisionInformatin);
        Obstacle.OnObstacleDestroy.AddListener(Move);
    }

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();

        transform.LookAt(_objectToLookAt);
    }

    public void StoreCollisionInformatin(Vector3 colliderPosition)
    {
        _colliderPosition = colliderPosition;
    }

    public void Move()
    {
        //Vector3 newPosition = _colliderPosition + (Vector3.back * _sphereCollider.radius * transform.localScale.x);
        Vector3 newPosition = _colliderPosition - (transform.forward * _sphereCollider.radius * transform.localScale.x);
        //Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, _colliderPosition.z) + Vector3.back * _sphereCollider.radius;


        //transform.position = new Vector3(_colliderPosition.x, transform.position.y, _colliderPosition.z);

        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(newPosition.x, transform.position.y, newPosition.z);
        transform.position = Vector3.Slerp(startPosition, endPosition, 2);

        //transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
        transform.LookAt(_objectToLookAt);
        OnPlayerMove?.Invoke();
    }
}
