using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();

    protected BaseState<EState> CurrentState;

    protected bool IsTransitioningState = false;

    void Start(){
        CurrentState.EnterState();
    }

    void Update(){
        EState nextStateKey = CurrentState.GetNextState();

        if(!IsTransitioningState && nextStateKey.Equals(CurrentState.StateKey)) {
            CurrentState.UpdateState();
        } else {
            TransitionToState(nextStateKey);
        }
    }

    public void TransitionToState(EState stateKey) {
        IsTransitioningState = true;
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
        IsTransitioningState = false;
    }

    void OnTriggerEnter(Collision other){
        CurrentState.OnTriggerEnter(other);
    }
    void OnTriggerStay(Collision other){
        CurrentState.OnTriggerStay(other);
    }
    void OnTriggerExit(Collision other){
        CurrentState.OnTriggerExit(other);
    }
}