using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D RB;
    [SerializeField, Range(1.0f, 100.0f)] private float MoveSpeed = 10.0f;
    [SerializeField] private float Acceleration = 5.0f;
    [SerializeField, Range(1.0f, 1000.0f)] private float Jumpforce = 10.0f;
    [SerializeField, Range(1.0f, 1000.0f)] private float DashForce = 10.0f;
    [SerializeField, Range(0.1f, 1.0f)] private float DashTime = 1.0f;
    [SerializeField] private float CoolDown = 3.0f;
    private Coroutine DashingCorutine;
    private bool Dashing = false;
    private bool DashCD;
    public bool Dead = false;
    private Vector2 SpawnPos;
    private PlayerController MyController;

    public void InitializePlayer(PlayerController playerController)
    {
        MyController = playerController;
        SpawnPos = transform.position;
        Dead = false;
    }

    public void AddMovement(Vector2 direction)
    {
        if(!Dashing)
        RB.AddForce(direction * MoveSpeed * Time.deltaTime * Acceleration);
    }

    public void Jump()
    {
        if(!Dashing)
         RB.AddForce(Vector2.up * Jumpforce);
    }

    public void DashDirection(Vector2 Direction)
    {
        if (!Dashing && !DashCD)
        {
            DashingCorutine = StartCoroutine(Dash(Direction));
        }
    }

    private IEnumerator Dash(Vector2 Direction)
    {
        Dashing = true;
        Vector2 Velocity = Vector2.zero;
        Vector2 DashVelocity = Direction * DashForce;
        float timer = 0.0f;
        while (timer < DashTime)
        {
            timer += Time.deltaTime;
            RB.velocity = DashVelocity;
            yield return new WaitForEndOfFrame();
        }
        RB.velocity = Velocity;
        Dashing = false;
        DashingCorutine = StartCoroutine(DashCoolDown());
    }

    private IEnumerator DashCoolDown()
    {
        DashCD = true;
        float timer = 0.0f;
        while (timer < CoolDown)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        DashCD = false;
        DashingCorutine = null;
    }


    public void Die()
    {
        Dead = true;
        RB.velocity = Vector2.zero;
        RB.isKinematic = true;
        MyController.ResetGame();
    }
    
    public void OnRespawn()
    {
        RB.isKinematic = false;
        transform.position = SpawnPos;
        Dead = false;
        RB.velocity = Vector2.zero;
    }

    public void UpdateCheckPoint(Vector2 NewSpawnPoint)
    {
        SpawnPos = NewSpawnPoint;
    }
    
}
