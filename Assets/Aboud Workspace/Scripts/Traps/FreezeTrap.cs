using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTrap : MonoBehaviour
{
    [SerializeField] private float        freezeDuration;
    [SerializeField] private                  GameObject[] ropeShooters;

    private PlayerController controller;
    private Rigidbody2D      rigidBody;
    private Vector3          freezePosition;
    private bool             needsReset;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !needsReset)
        {
            controller = other.GetComponentInParent<PlayerController>();
            controller.enabled = false;
            
            rigidBody = other.GetComponent<Rigidbody2D>();
            rigidBody.velocity = Vector2.zero;
            rigidBody.gravityScale = 0;
            
            needsReset = true;
            
            
            for (int i = 0; i < ropeShooters.Length; i++)
            {
                ropeShooters[i].GetComponent<LineRenderer>().SetVertexCount(2);
                ropeShooters[i].GetComponent<LineRenderer>().SetPosition(0, ropeShooters[i].transform.position);
                ropeShooters[i].GetComponent<LineRenderer>().SetPosition(1, other.transform.position);
            }
            
            StartCoroutine("UnFreezePlayer");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            needsReset = false;
        }
    }

    private IEnumerator UnFreezePlayer()
    {
        yield return new WaitForSeconds(freezeDuration);
        rigidBody.gravityScale = 1;
        
        controller.enabled = true;
        
        for (int i = 0; i < ropeShooters.Length; i++)
        {
            ropeShooters[i].GetComponent<LineRenderer>().SetVertexCount(0);
            
        }
    }
}
