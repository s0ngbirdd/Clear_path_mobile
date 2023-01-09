using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Serialize
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private float _objectToSpawnYOffset = 0.5f;
    [SerializeField] private float _raycastDistance = 100f;
    [SerializeField] private float _overlapBoxSize = 0.5f;
    [SerializeField] private LayerMask _spawnedObjectLayer;
    [SerializeField] private float _timeForDestory = 0.01f;

    // Private
    private SpawnerSpreader _spawnerSpreader;

    private void Start()
    {
        _spawnerSpreader = FindObjectOfType<SpawnerSpreader>();

        OverlappingCheck();
        StartCoroutine(DestroySelf());
    }

    private void OverlappingCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _raycastDistance))
        {
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            Vector3 overlapTestBoxScale = new Vector3(_overlapBoxSize, _overlapBoxSize, _overlapBoxSize);
            Collider[] collidersInsideOverlapBox = new Collider[1];
            int numberOfCollidersFound = Physics.OverlapBoxNonAlloc(hit.point, overlapTestBoxScale, collidersInsideOverlapBox, spawnRotation, _spawnedObjectLayer);

            if (numberOfCollidersFound == 0)
            {
                Spawn(hit.point, spawnRotation);
            }
            else
            {
                _spawnerSpreader.Spread();
            }
        }
    }

    private void Spawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        Instantiate(_objectToSpawn, positionToSpawn + new Vector3(0, _objectToSpawnYOffset, 0), rotationToSpawn);
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(_timeForDestory);

        Destroy(gameObject);
    }
}