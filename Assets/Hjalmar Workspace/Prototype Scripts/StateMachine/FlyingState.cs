using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

[CreateAssetMenu(menuName = "States/FlyState")]
public class FlyingState : BaseState
{
    private Rigidbody2D MyBody;
    [SerializeField] private float FlyingSpeed;
    public override void Initialize(StateMachine NewOwner)
    {
        Owner = NewOwner;
        MyBody = Owner.GetPlayerRB();
    }
    public override void OnEnter()
    {
        Debug.Log("Entering " + StateName);
    }

    public override void OnUpdate()
    {
        Vector2 Direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        MyBody.velocity = Direction.normalized * FlyingSpeed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Owner.ChangeState(StateEnums.FALLING);
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exiting " + StateName);
    }
}
