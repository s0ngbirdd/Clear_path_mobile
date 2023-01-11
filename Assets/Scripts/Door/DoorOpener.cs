using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private HingeJoint _hingeJoint;
    private float _doorOpenDirection = 90f;

    private void Start()
    {
        _hingeJoint = GetComponent<HingeJoint>();
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            JointSpring jointSpring = _hingeJoint.spring;
            jointSpring.targetPosition = _doorOpenDirection;
            _doorOpenDirection = Mathf.Abs(_doorOpenDirection - 90);
            _hingeJoint.spring = jointSpring;
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            CloseDoor();
        }
    }

    public void ChangeDoorPosition()
    {
        JointSpring jointSpring = _hingeJoint.spring;
        jointSpring.targetPosition = _doorOpenDirection;
        _doorOpenDirection = Mathf.Abs(_doorOpenDirection - 90);
        _hingeJoint.spring = jointSpring;
    }

    public void OpenDoor()
    {
        JointSpring jointSpring = _hingeJoint.spring;
        jointSpring.targetPosition = 90f;
        _hingeJoint.spring = jointSpring;
    }

    public void CloseDoor()
    {
        JointSpring jointSpring = _hingeJoint.spring;
        jointSpring.targetPosition = 0f;
        _hingeJoint.spring = jointSpring;
    }
}
