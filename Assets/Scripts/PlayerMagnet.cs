using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerMagnet : MonoBehaviour
{
    [SerializeField] private Transform MagnetPoint;
    [SerializeField] private Rigidbody2D player;
    private float previousGravity;
    private bool Loose = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger enter");
        if (other.gameObject.CompareTag("Player"))
        {
            
            if (Loose)
            {
                Loose = false;   
                return;
            }
            if (player == null)
            {
                player = other.gameObject.GetComponent<Rigidbody2D>();
                player.gameObject.transform.position = MagnetPoint.position;
                previousGravity = player.gravityScale;
                player.isKinematic = true;
                player.gravityScale = 0.0f;
            }
        }
    }

    private void Update()
    {
        if (player != null)
        {
            player.velocity = Vector2.zero;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(MagnetPoint.transform.position, 0.05f);
    }
#endif

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Trigger Exit");
        if (other.gameObject.CompareTag("Player"))
        {
            if (player != null)
            {
                Loose = true;
                player.gravityScale = previousGravity;
                player.isKinematic = false;
                player = null;
            }
        }
    }
}
