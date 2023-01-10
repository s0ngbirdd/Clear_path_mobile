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
    }

    private void Start()
    {
        Stretch();
    }

    public void Stretch()
    {
        float scale = _objectWithScale.transform.localScale.x;
        _distanceBetweenObjects = Vector3.Distance(transform.position, _objectToStretch.transform.position);
        //transform.localScale = new Vector3(1, 1, _distanceBetweenObjects / 10);
        transform.localScale = new Vector3(scale / 10, 1, _distanceBetweenObjects / 10);
    }
}
