using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/WalkState")]
public class WalkingState : BaseState
{
    private Rigidbody2D MyBody;
    [SerializeField] private float RunSpeed;

    //[SerializeField] private LayerMask GroundLayer;
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
        MyBody.AddForce(Direction * RunSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Owner.ChangeState(StateEnums.JUMPING);
        }

        Vector3 StartPos = MyBody.gameObject.transform.position;
        RaycastHit2D Hit = Physics2D.CircleCast(StartPos, 0.5f,  Vector2.down, 0.1f);//, GroundLayer);
        if (!Hit)
        {
            Owner.ChangeState(StateEnums.FALLING);
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exiting " + StateName);
    }
}
