using UnityEngine;

public class ObjectStretcher : MonoBehaviour
{
    // Serialize
    [SerializeField] private Transform _objectToStretch;

    // Private
    private float _distanceBetweenObjects;

    private void Start()
    {
        Stretch();
    }

    public void Stretch()
    {
        _distanceBetweenObjects = Vector3.Distance(transform.position, _objectToStretch.transform.position);
        transform.localScale = new Vector3(1, 1, _distanceBetweenObjects / 10);
    }
}
