using UnityEngine;

public class ChargeState : PlayerBaseState
{
    private Player _player;

    public ChargeState(Player player)
    {
        _player = player;
    }

    public override void EnterState()
    {
        //base.EnterState();
        Debug.Log("Enter ChargeState");
        //_player.GetComponent<BulletCreator>().ChargeBullet();
    }

    public override void UpdateState()
    {
        //base.UpdateState();
    }

    public override void ExitState()
    {
        //base.ExitState();
        Debug.Log("Exit ChargeState");
    }
}
