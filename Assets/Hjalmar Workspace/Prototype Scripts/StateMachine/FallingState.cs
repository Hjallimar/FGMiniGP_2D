using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/FallingState")]
public class FallingState : BaseState
{
    //[SerializeField] private LayerMask GroundLayer;
    private float timer = 0.0f;
    private Transform Player;
    private Rigidbody2D PlayerRB;
    public override void Initialize(StateMachine NewOwner)
    {
        Owner = NewOwner;
        Player = Owner.GetPlayer().transform;
        PlayerRB = Owner.GetPlayerRB();
    }
    public override void OnEnter()
    {
        timer = 0.0f;
        Debug.Log("Entering " + StateName);
    }

    public override void OnUpdate()
    {
        Vector3 StartPos = Player.position;
        RaycastHit2D Hit = Physics2D.CircleCast(StartPos, 0.5f,  Vector2.down, 0.1f);//, GroundLayer);
        if (Hit)
        {
            Owner.ChangeState(StateEnums.WALKING);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Owner.ChangeState(StateEnums.CHARGE);
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exiting " + StateName);
    }
}
