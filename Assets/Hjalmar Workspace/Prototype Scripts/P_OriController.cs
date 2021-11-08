using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_OriController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D MyRigidBody;
    [SerializeField] private StateMachine MyStateMachine;
    
    //States
    
    void Awake()
    {
        MyStateMachine.SetUpStateMashine(this, MyRigidBody);
    }

    private void Update()
    {
        MyStateMachine.UpdateStateMachine();
    }
}
