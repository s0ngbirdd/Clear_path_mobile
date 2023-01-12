using UnityEngine;
using UnityEngine.Events;

public class FinishZone : MonoBehaviour
{
    // Public
    public static UnityEvent OnFinishEnter = new UnityEvent();
    public static UnityEvent OnFinishExit = new UnityEvent();

    // Serialize
    [SerializeField] private string _colliderTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(_colliderTag))
        {
            OnFinishEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(_colliderTag))
        {
            OnFinishExit?.Invoke();
        }
    }
}
