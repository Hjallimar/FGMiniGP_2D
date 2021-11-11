using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OneWayTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider2D Collider;
    private Vector3 point;
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            Vector3 Diff = other.gameObject.transform.position - transform.position;
            point = Diff.normalized;
            float Allign = Vector3.Dot(Diff.normalized, transform.up);
            Debug.Log(Allign + " is current dot, values are " + Diff.normalized +  ":" + transform.up);
            if (Allign > 0)
            {
                Collider.isTrigger = false;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 10.0f);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + point * 10.0f);
    }
}
