using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{    
    [Header("Respawn Points")]
    [SerializeField] private Transform redSpawn;
    [SerializeField] private Transform greenSpawn;
    [Header("Lava Respawn")]
    [SerializeField] private float DistanceBetweenLavaAndPlayer;
    [SerializeField] private GameObject       LavaFloor;
    [SerializeField] private PlayerController playerController;

    private                  bool           greenReached;
    private                  bool           redReached;
    private                  PlayerMovement redPlayerMov;
    private                  PlayerMovement greenPlayerMov;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMov = other.GetComponent<PlayerMovement>();
            if (playerMov.playerEnum == PlayerMovement.Player.GreenPlayer)
            {
                greenReached = true;
                greenPlayerMov = playerMov;
            }
            if (playerMov.playerEnum == PlayerMovement.Player.RedPlayer)
            {
                redReached = true;
                redPlayerMov = playerMov;
            }

            if (redReached && greenReached)
            {
                redPlayerMov.UpdateCheckPoint(redSpawn.position);
                greenPlayerMov.UpdateCheckPoint(greenSpawn.position);
                playerController.checkpoint = this;
                
            }

        }
    }

    public void ResetLava()
    {
        if (LavaFloor != null)
        {
            LavaFloor.transform.position = new Vector3(transform.position.x, transform.position.y - DistanceBetweenLavaAndPlayer, transform.position.z);
        }
        else
        {
            Debug.LogError("You need to assign Lava Floor Variable");
        }
    }
}
