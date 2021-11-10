using System;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float      jumpForce;
    private                  GameObject player;
    
    public bool inPosition;
    
    private void OnTriggerEnter2D(Collider2D other)
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
            player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
            Debug.Log("Heya");
        }
    }
}
