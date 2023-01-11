public class PlayerStateMachine
{
    public PlayerBaseState CurrentState { get; private set; }

    public void Initialize(PlayerBaseState startState)
    {
        CurrentState = startState;
        CurrentState.EnterState();
    }

    public void SwitchState(PlayerBaseState newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
}
