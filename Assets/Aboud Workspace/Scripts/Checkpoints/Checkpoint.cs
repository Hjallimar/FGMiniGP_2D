using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool greenReached;
    private bool redReached;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMov = other.GetComponent<PlayerMovement>();
            if (playerMov.playerEnum == PlayerMovement.Player.GreenPlayer)
                greenReached = true;
            if (playerMov.playerEnum == PlayerMovement.Player.RedPlayer)
                redReached = true;
        }
    }
}
