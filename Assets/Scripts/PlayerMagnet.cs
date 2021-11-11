using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMagnet : MonoBehaviour
{
    [SerializeField] private Transform MagnetPoint;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private PlayerMovement.Player Repell;
    [SerializeField] private float RepellForce;
    private float previousGravity;
    private bool Loose = false;
    private GameObject player; 
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger enter");
        player = other.gameObject;
        if (player.CompareTag("Player"))
        {
            
            if (player.GetComponent<PlayerMovement>().playerEnum == Repell)
            {
                Vector2 Opposite = transform.position - player.transform.position;
                player.GetComponent<Rigidbody2D>().AddForce(-Opposite.normalized * RepellForce);
                return;
            }
            if (Loose)
            {
                Loose = false;   
                return;
            }
            if (rigidBody == null)
            {
                rigidBody = player.GetComponent<Rigidbody2D>();
                rigidBody.gameObject.transform.position = MagnetPoint.position;
                previousGravity = rigidBody.gravityScale;
                rigidBody.isKinematic = true;
                rigidBody.gravityScale = 0.0f;
            }
        }
    }

    private void Update()
    {
        if (rigidBody != null)
        {
            rigidBody.velocity = Vector2.zero;
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
            if (rigidBody != null)
            {
                Loose = true;
                rigidBody.gravityScale = previousGravity;
                rigidBody.isKinematic = false;
                rigidBody = null;
            }
        }
    }
}
