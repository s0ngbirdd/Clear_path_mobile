using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    public static UnityEvent OnObstacleDestruction = new UnityEvent();

    private Color _baseColor;

    private void Start()
    {
        _baseColor = GetComponent<MeshRenderer>().material.color;
    }

    private void Update()
    {
        if (GetComponent<MeshRenderer>().material.color != _baseColor)
        {
            Destroy(gameObject, 0.5f);
        }
    }

    private void OnDestroy()
    {
        OnObstacleDestruction?.Invoke();
    }
}
