using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

[CreateAssetMenu(menuName = "States/JumpState")]
public class JumpState : BaseState
{
    private Rigidbody2D MyBody;
    [SerializeField] private float JumpForce;
    private float timer = 0.0f;
    public override void Initialize(StateMachine NewOwner)
    {
        Owner = NewOwner;
        MyBody = Owner.GetPlayerRB();

    }
    public override void OnEnter()
    {
        Debug.Log("Entering " + StateName);
        if (MyBody == null)
        {
            MyBody = Owner.GetPlayerRB();
        }

        timer = 0.0f;
        MyBody.AddForce(new Vector2(0.0f, 1.0f) * JumpForce);
    }

    public override void OnUpdate()
    {
        //Check grounded and then sawp to walking
        Debug.Log("Updating " + StateName);
        timer += Time.deltaTime;
        if (timer > 2.0f)
        {
            Owner.ChangeState(StateEnums.WALKING);
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exiting " + StateName);
    }
}
