using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Serialize
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private float _objectToSpawnYOffset = 0.5f;
    [SerializeField] private float _raycastDistance = 100.0f;
    [SerializeField] private float _overlapBoxSize = 0.5f;
    [SerializeField] private LayerMask _spawnedObjectLayer;
    [SerializeField] private float _timeBeforeDestory = 0.01f;

    // Private
    private ParentObstacle _parentObject;

    private void Start()
    {
        _parentObject = FindObjectOfType<ParentObstacle>();

        ObjectOverlappingCheck();
        StartCoroutine(DestroySelf());
    }

    private void ObjectOverlappingCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _raycastDistance))
        {
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            Vector3 overlapTestBoxScale = new Vector3(_overlapBoxSize, _overlapBoxSize, _overlapBoxSize);
            Collider[] collidersInsideOverlapBox = new Collider[1];
            int numberOfCollidersFound = Physics.OverlapBoxNonAlloc(hit.point, overlapTestBoxScale, collidersInsideOverlapBox, spawnRotation, _spawnedObjectLayer);

            if (numberOfCollidersFound == 0)
            {
                SpawnObject(hit.point, spawnRotation);
            }
        }
    }

    private void SpawnObject(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        GameObject obj = Instantiate(_objectToSpawn, positionToSpawn + new Vector3(0, _objectToSpawnYOffset, 0), rotationToSpawn);
        obj.transform.SetParent(_parentObject.transform, true);
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(_timeBeforeDestory);

        Destroy(gameObject);
    }
}
