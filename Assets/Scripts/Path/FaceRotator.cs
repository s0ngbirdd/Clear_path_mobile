using UnityEngine;

public class FaceRotator : MonoBehaviour
{
    // Serialize
    [SerializeField] private Transform _objectToLook;

    private void Start()
    {
        transform.LookAt(_objectToLook);
    }
}
