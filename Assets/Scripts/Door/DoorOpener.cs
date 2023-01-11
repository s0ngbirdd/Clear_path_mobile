using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    // Serialize
    [SerializeField] private string _colliderTag = "Player";
    [SerializeField] private float _doorOpenPosition = 90.0f;
    [SerializeField] private float _doorClosePosition = 0.0f;

    // Private
    private HingeJoint _hingeJoint;

    private void Start()
    {
        _hingeJoint = GetComponent<HingeJoint>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(_colliderTag))
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(_colliderTag))
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        JointSpring jointSpring = _hingeJoint.spring;
        jointSpring.targetPosition = _doorOpenPosition;
        _hingeJoint.spring = jointSpring;
    }

    private void CloseDoor()
    {
        JointSpring jointSpring = _hingeJoint.spring;
        jointSpring.targetPosition = _doorClosePosition;
        _hingeJoint.spring = jointSpring;
    }
}
