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
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _rigidbody = GetComponent<Rigidbody>();

        Bullet.OnObstacleImpact.AddListener(StoreCollisionInformatin);
        Obstacle.OnObstacleDestroy.AddListener(MoveObject);
        FinishZone.OnFinishEnter.AddListener(FinishMoveObject);
    }

    private void Start()
    {
        transform.LookAt(_objectToLookAt);
    }

    private void StoreCollisionInformatin(Vector3 colliderPosition)
    {
        _colliderPosition = colliderPosition;
    }

    private void MoveObject()
    {
        Vector3 newPosition = _colliderPosition - (transform.forward * _sphereCollider.radius * transform.localScale.x);

        transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
        transform.LookAt(_objectToLookAt);
        OnPlayerMove?.Invoke();
    }

    private void FinishMoveObject()
    {
        transform.position = new Vector3(_objectToLookAt.position.x, _objectToLookAt.position.y + _sphereCollider.radius * transform.localScale.x * 2, _objectToLookAt.position.z);
        _rigidbody.useGravity = true;
    }
}
