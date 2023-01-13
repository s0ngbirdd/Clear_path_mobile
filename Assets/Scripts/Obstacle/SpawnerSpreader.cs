using UnityEngine;

public class SpawnerSpreader : MonoBehaviour
{
    // Serialize
    [SerializeField] private GameObject _objectToSpread;
    [SerializeField] private float _objectXSpread = 10.0f;
    [SerializeField] private float _objectYSpread = 0.0f;
    [SerializeField] private float _objectZSpread = 10.0f;
    [SerializeField] private int _objectToSpreadNumber = 100;

    private void Start()
    {
        for (int i = 0; i < _objectToSpreadNumber; i++)
        {
            SpreadObject();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(_objectXSpread, _objectYSpread, _objectZSpread));
    }

    private void SpreadObject()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-_objectXSpread, _objectXSpread), Random.Range(-_objectYSpread, _objectYSpread), Random.Range(-_objectZSpread, _objectZSpread)) + transform.position;
        Instantiate(_objectToSpread, transform.TransformPoint(randomPosition), _objectToSpread.transform.rotation);
    }
}