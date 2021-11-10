using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody2D MyRigidBody;
    [SerializeField] private Transform EndLocation;
    [SerializeField] private Collider2D TriggerZone;
    [SerializeField, Range(1.0f, 10.0f)] private float TravelTime = 5.0f;
    [SerializeField, Range(0.0f, 10.0f)] private float WaitTime = 5.0f;
    [SerializeField] private bool StickyPlatform = true;
    private Vector2 StartPos;
    private Vector2 EndPos;
    private float LerpValue;
    private int Direction = 1;

    private bool Travel = true;
    private bool Wait = false;
    
    private float TimeCounter = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        MyRigidBody.isKinematic = true;
        MyRigidBody.gravityScale = 0.0f;
        StartPos = transform.position;
        EndPos = EndLocation.position;
        TimeCounter = 0.0f;
        LerpValue = 0.0f;
        if (StickyPlatform)
        {
            TriggerZone.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateNewValue();
        transform.position = Vector2.Lerp(StartPos, EndPos, LerpValue);
        Debug.Log("Lerp: " + LerpValue + ", Timer :" + TimeCounter + ", Direction:" + Direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.SetParent(transform);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.transform.SetParent(null);
    }

    void CalculateNewValue()
    {
        TimeCounter += Time.deltaTime;
        if (Travel)
        {
            if(Direction > 0)
                LerpValue = TimeCounter / TravelTime;
            else
                LerpValue = 1.0f - (TimeCounter / TravelTime);
            if (TimeCounter >= TravelTime)
            {
                Travel = false;
                TimeCounter = 0.0f;
                Direction *= -1;
                Wait = true;
            }
        }
        else if (Wait)
        {
            if (TimeCounter >= WaitTime)
            {
                TimeCounter = 0.0f;
                Travel = true;
                Wait = false;
            }
        }
    }
}
