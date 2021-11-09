using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/FallingState")]
public class FallingState : BaseState
{
    private float timer = 0.0f;
    public override void Initialize(StateMachine NewOwner)
    {
        Owner = NewOwner;

    }
    public override void OnEnter()
    {
        timer = 0.0f;
        Debug.Log("Entering " + StateName);
    }

    public override void OnUpdate()
    {
        //Check grounded and then sawp to walking
        Debug.Log("Updating " + StateName);
        timer += Time.deltaTime;
        if (timer > 1.5f)
        {
            Owner.ChangeState(StateEnums.WALKING);
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exiting " + StateName);
    }
}
