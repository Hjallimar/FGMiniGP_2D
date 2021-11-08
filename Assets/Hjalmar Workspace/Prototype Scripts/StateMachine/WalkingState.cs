using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/WalkState")]
public class WalkingState : BaseState
{
    private Rigidbody2D MyBody;
    [SerializeField] private float RunSpeed;
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
    }

    public override void OnUpdate()
    {
        Vector2 Direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0.0f);
        Debug.Log("Updating " + StateName + ", Direction is: " + Direction);
        MyBody.AddForce(Direction * RunSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Owner.ChangeState(StateEnums.JUMPING);
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exiting " + StateName);
    }

}
