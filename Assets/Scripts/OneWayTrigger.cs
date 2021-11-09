using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider2D Collider;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Collider.isTrigger = false;
        }
    }
}
