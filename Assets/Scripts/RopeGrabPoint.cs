using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RopeGrabPoint : MonoBehaviour
{
    private PlayerMovement Player;
    private bool active;
    [SerializeField] private Rigidbody2D MyRB;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (active)
            {
                Player.gameObject.transform.SetParent(null);
                Player.StopHanging();
                Player = null;
                active = false;
            }
            else if (Player != null)
            {
                Player.gameObject.transform.SetParent(transform);
                Player.StartHanging(MyRB);
                active = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player = other.gameObject.GetComponent<PlayerMovement>();
        }
    }
}
