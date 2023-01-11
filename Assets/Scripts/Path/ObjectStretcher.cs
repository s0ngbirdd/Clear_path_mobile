using UnityEngine;

public class ObjectStretcher : MonoBehaviour
{
    // Serialize
    [SerializeField] private Transform _objectToStretch;
    [SerializeField] private Transform _objectWithScale;

    // Private
    private float _distanceBetweenObjects;

    private void Awake()
    {
        Bullet.OnBulletIncrease.AddListener(Stretch);
        Obstacle.OnObstacleDestruction.AddListener(Move);
    }

    private void Start()
    {
        Stretch();
    }

    private void Stretch()
    {
        float scale = _objectWithScale.transform.localScale.x;
        transform.LookAt(_objectToStretch);
        _distanceBetweenObjects = Vector3.Distance(transform.position, _objectToStretch.transform.position);
        transform.localScale = new Vector3(scale / 10, 1, _distanceBetweenObjects / 10);
    }

    private void Move()
    {
        transform.position = new Vector3(_objectWithScale.transform.position.x, transform.position.y, _objectWithScale.transform.position.z);
        Stretch();
    }
}
