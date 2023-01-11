using UnityEngine;

public class LaunchState : PlayerBaseState
{
    private Player _player;

    public LaunchState(Player player)
    {
        _player = player;
    }

    public override void EnterState()
    {
        //base.EnterState();
        Debug.Log("Enter LaunchState");
    }

    public override void UpdateState()
    {
        //base.UpdateState();
    }

    public override void ExitState()
    {
        //base.ExitState();
        Debug.Log("Exit LaunchState");
    }
}
