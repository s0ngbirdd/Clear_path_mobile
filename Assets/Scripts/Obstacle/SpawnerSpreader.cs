using UnityEngine;

public class SpawnerSpreader : MonoBehaviour
{
    // Serialize
    [SerializeField] private GameObject _objectToSpread;
    [SerializeField] private float _objectXSpread = 10;
    [SerializeField] private float _objectYSpread = 0;
    [SerializeField] private float _objectZSpread = 10;
    [SerializeField] private int _objectToSpreadNumber = 100;

    private void Start()
    {
        for (int i = 0; i < _objectToSpreadNumber; i++)
        {
            Spread();
        }
    }

    public void Spread()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-_objectXSpread, _objectXSpread), Random.Range(-_objectYSpread, _objectYSpread), Random.Range(-_objectZSpread, _objectZSpread)) + transform.position;
        Instantiate(_objectToSpread, randomPosition, _objectToSpread.transform.rotation);
    }
}