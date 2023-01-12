using UnityEngine;

public class SpawnerSpreader : MonoBehaviour
{
    // Serialize
    [SerializeField] private GameObject _objectToSpread;
    //[SerializeField] private Transform _finishObjectPosition;
    [SerializeField] private float _objectXSpread = 10.0f;
    [SerializeField] private float _objectYSpread = 0.0f;
    [SerializeField] private float _objectZSpread = 10.0f;
    [SerializeField] private int _objectToSpreadNumber = 100;

    private void Start()
    {
        //SpreadFinishObject();

        for (int i = 0; i < _objectToSpreadNumber; i++)
        {
            SpreadObject();
        }
    }

    public void SpreadObject()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-_objectXSpread, _objectXSpread), Random.Range(-_objectYSpread, _objectYSpread), Random.Range(-_objectZSpread, _objectZSpread)) + transform.position;
        Instantiate(_objectToSpread, transform.TransformPoint(randomPosition), _objectToSpread.transform.rotation);
    }

    /*public void SpreadFinishObject()
    {
        //Vector3 randomPosition = new Vector3(Random.Range(-_objectXSpread, _objectXSpread), Random.Range(-_objectYSpread, _objectYSpread), Random.Range(-_objectZSpread, _objectZSpread)) + transform.position;
        GameObject obj = Instantiate(_objectToSpread, _finishObjectPosition.position - _finishObjectPosition.forward * 9, _objectToSpread.transform.rotation);
        obj.name = "FINISHOB";
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(_objectXSpread, _objectYSpread, _objectZSpread));
    }
}