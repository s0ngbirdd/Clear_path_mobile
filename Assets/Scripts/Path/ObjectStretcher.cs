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
        Bullet.OnBulletIncrease.AddListener(StretchObject);
        Obstacle.OnObstacleDestroy.AddListener(MoveObject);
        FinishZone.OnFinishEnter.AddListener(DestroyPath);
    }

    private void Start()
    {
        StretchObject();
    }

    private void StretchObject()
    {
        float scale = _objectWithScale.transform.localScale.x;
        transform.LookAt(_objectToStretch);
        _distanceBetweenObjects = Vector3.Distance(transform.position, _objectToStretch.transform.position);
        transform.localScale = new Vector3(scale / 10, 1, _distanceBetweenObjects / 10);
    }

    private void MoveObject()
    {
        transform.position = new Vector3(_objectWithScale.transform.position.x, transform.position.y, _objectWithScale.transform.position.z);
        StretchObject();
    }

    private void DestroyPath()
    {
        Destroy(gameObject);
    }
}
