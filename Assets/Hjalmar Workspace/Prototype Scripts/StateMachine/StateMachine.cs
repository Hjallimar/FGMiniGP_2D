using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum StateEnums
{
    WALKING = 0, JUMPING = 1
}

[CreateAssetMenu(menuName = "StateMachine")]
public class StateMachine : ScriptableObject
{
    [SerializeField] private List<BaseState> MyStates;
    private P_OriController Player;
    private Rigidbody2D PlayerRB;
    
    
    public P_OriController GetPlayer()
    {
        return Player;
    }
    public Rigidbody2D GetPlayerRB()
    {
        return PlayerRB;
    }
    private BaseState CurrentState = null;
    private BaseState PreviousState = null;

    public void SetUpStateMashine(P_OriController NewPlayer, Rigidbody2D NewPlayerRB)
    {
        Player = NewPlayer;
        PlayerRB = NewPlayerRB;
        foreach (BaseState state in MyStates)
        {
            state.Initialize(this);
        }
        CurrentState = MyStates[0];
        CurrentState.OnEnter();
    }
    public void UpdateStateMachine()
    {
        if (CurrentState != null)
        {
            CurrentState.OnUpdate();
        }
    }

    public void ChangeState(StateEnums NewState)
    {
        BaseState ExistingState = null;
        foreach (BaseState state in MyStates)
        {
            if (state.CompareTo(NewState))
            {
                ExistingState = state;
                break;
            }
        }

        if (ExistingState != null)
        {
            PreviousState = CurrentState;
            CurrentState = ExistingState;
            PreviousState.OnExit();
            CurrentState.OnEnter();
        }

    }
}
