using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    [SerializeField] private float RotationSpeed;

    [SerializeField] private Transform RotationPivot;
    // Update is called once per frame
    void Update()
    {
        if (RotationPivot == null)
        {
            transform.RotateAround(Vector3.forward, RotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(RotationPivot.position, Vector3.forward, RotationSpeed * Time.deltaTime);
        }
    }
}
