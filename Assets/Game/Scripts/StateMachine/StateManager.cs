using UnityEngine;

public class StateManager : MonoBehaviour
{
    private IState currentState;
    public IState CurrentState => currentState;

    void Update()
    {
        if (currentState != null)
            currentState.Execute();
    }

    private void FixedUpdate()
    {
        if (currentState != null)
            currentState.FixedExecute();
    }

    public void ChangeState(IState enterState)
    {
        if (currentState != null && currentState == enterState)
            return;

        if (currentState != null)
            currentState.Exit();

        currentState = enterState;

        if (currentState != null)
            currentState.Enter();
    }
}
