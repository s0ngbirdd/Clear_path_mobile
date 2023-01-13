using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    // Serialize
    [SerializeField] private float _doorOpenPosition = 90.0f;
    [SerializeField] private float _doorClosePosition = 0.0f;

    // Private
    private HingeJoint _hingeJoint;

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();

        FinishZone.OnFinishEnter.AddListener(OpenDoor);
        FinishZone.OnFinishExit.AddListener(CloseDoor);
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
