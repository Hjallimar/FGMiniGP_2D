using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    [SerializeField] private Transform MagnetPoint;
    private Rigidbody2D player;
    private float previousGravity;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger enter");
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<Rigidbody2D>();
            player.gameObject.transform.position = MagnetPoint.position;
            previousGravity = player.gravityScale;
            player.isKinematic = true;
            player.gravityScale = 0.0f;
        }
    }

    private void Update()
    {
        if (player != null)
        {
            player.velocity = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Trigger Exit");
        if (other.gameObject.CompareTag("Player"))
        {
            player.gravityScale = previousGravity;
            player.isKinematic = false;
            player = null;
        }
    }
}
