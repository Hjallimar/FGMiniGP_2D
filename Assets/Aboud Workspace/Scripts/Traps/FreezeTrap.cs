using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTrap : MonoBehaviour
{
    [SerializeField] private float freezeDuration;

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
            StartCoroutine("FreezePlayer");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            needsReset = false;
        }
    }

    private IEnumerator FreezePlayer()
    {
        yield return new WaitForSeconds(freezeDuration);
        rigidBody.gravityScale = 1;
        controller.enabled = true;
    }
}
