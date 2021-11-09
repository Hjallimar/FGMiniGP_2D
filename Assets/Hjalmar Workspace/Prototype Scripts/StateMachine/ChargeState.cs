using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/ChargeState")]
public class ChargeState : BaseState
{
    private Rigidbody2D MyBody;
    private Vector2 StaticPos;
    [SerializeField] private float ChargeSpeed;

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
        StaticPos = MyBody.transform.position;
    }

    public override void OnUpdate()
    {
        Vector2 Direction = GatherDirection();
        MyBody.velocity = Vector2.zero;
        MyBody.transform.position = StaticPos;
        if (Input.GetMouseButtonUp(0))
        {
            MyBody.AddForce(Direction * ChargeSpeed);
            Owner.ChangeState(StateEnums.FALLING);
        }
    }
    Vector2 GatherDirection()
    {
        Vector2 PositionOnScreen = (Vector2)Input.mousePosition - new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        Vector2 diff = (Vector2)MyBody.gameObject.transform.position - PositionOnScreen;
        Debug.Log(MyBody.transform.position + ": " + diff);
        return diff.normalized;
    }
    public override void OnExit()
    {
        Debug.Log("Exiting " + StateName);
    }
}
