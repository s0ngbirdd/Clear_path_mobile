using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    // Public
    public static UnityEvent OnObstacleDestroy = new UnityEvent();

    private void OnDestroy()
    {
        OnObstacleDestroy?.Invoke();
    }
}
