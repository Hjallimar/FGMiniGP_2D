using System;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float      jumpForce;
    [SerializeField] private bool       withAnimation;
    private                  GameObject player;
    private                  Animator   anim;
    public                   bool       inPosition;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            inPosition = true;
            player = other.gameObject;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            inPosition = false;
            player = null;
        }
        
    }

    public void LaunchPlayer()
    {
        if (inPosition)
        {
            if(withAnimation)
                anim.SetTrigger("Launch");
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
            Debug.Log("Heya");
        }
    }
}
