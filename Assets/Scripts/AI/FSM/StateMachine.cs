using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class StateMachine
{
    public State currentState;
    public StateMachine(AIController aiController)
    {
        SetState(new MovingState(aiController));
    }

    public void SetState(State newState)
    {
        Debug.Log($"NewState: {newState.GetType()}");
        currentState?.Exit();

        currentState = newState;
        currentState.Enter();
    }
}
