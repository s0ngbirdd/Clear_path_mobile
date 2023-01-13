using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraMatchWidth : MonoBehaviour
{
    // Serialize
    [SerializeField] private float _sceneWidth = 20.0f;  // Set this to the in-world distance between the left & right edges of your scene

    // Private
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Adjust the camera's height so the desired scene width fits in view even if the screen/window size changes dynamically
    private void Update()
    {
        float unitsPerPixel = _sceneWidth / Screen.width;
        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;
        _camera.orthographicSize = desiredHalfHeight;
    }
}
