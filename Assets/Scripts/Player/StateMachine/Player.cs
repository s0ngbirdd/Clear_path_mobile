using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Private
    private PlayerStateMachine _playerStateMachine;
    private IdleState _idleState;
    private ChargeState _chargeState;
    private LaunchState _launchState;
    private ImpactState _impactState;
    private ExplosionState _explosionState;
    private MoveState _moveState;

    //private Bullet _bullet;
    private bool _isBulletLocked;

    private void Awake()
    {
        Mover.OnPlayerMove.AddListener(UnlockBullet);
    }

    private void Start()
    {
        _playerStateMachine = new PlayerStateMachine();

        _idleState = new IdleState(this);
        _chargeState = new ChargeState(this);
        _launchState = new LaunchState(this);
        _impactState = new ImpactState(this);
        _explosionState = new ExplosionState(this);
        _moveState = new MoveState(this);

        _playerStateMachine.Initialize(_idleState);
    }

    private void Update()
    {
        _playerStateMachine.CurrentState.UpdateState();

        if (Input.GetKeyDown(KeyCode.Space) && !_isBulletLocked)
        {
            _playerStateMachine.SwitchState(_chargeState);
            _isBulletLocked = true;
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {
            _playerStateMachine.SwitchState(_launchState);
            //_bullet = null;
        }

        /*else
        {
            _playerStateMachine.SwitchState(_idleState);
        }*/
    }

    public void UnlockBullet()
    {
        _isBulletLocked = false;
    }
}
