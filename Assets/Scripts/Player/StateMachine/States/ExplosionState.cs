using UnityEngine;

public class ExplosionState : PlayerBaseState
{
    private Player _player;

    public ExplosionState(Player player)
    {
        _player = player;
    }

    public override void EnterState()
    {
        //base.EnterState();
        Debug.Log("Enter IdleState");
    }

    public override void UpdateState()
    {
        //base.UpdateState();
    }

    public override void ExitState()
    {
        //base.ExitState();
        Debug.Log("Exit IdleState");
    }
}
