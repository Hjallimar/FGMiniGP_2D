using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D RB;
    [SerializeField, Range(1.0f, 100.0f)] private float MoveSpeed = 10.0f;
    [SerializeField, Range(1.0f, 1000.0f)] private float Jumpforce = 10.0f;

    public void AddMovement(Vector2 direction)
    {
        RB.AddForce(direction * MoveSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        RB.AddForce(Vector2.up * Jumpforce);
    }
}
