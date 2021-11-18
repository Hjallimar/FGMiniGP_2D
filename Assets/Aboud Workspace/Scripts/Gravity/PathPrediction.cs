using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPrediction : MonoBehaviour
{
    private List<GameObject> stepColliders = new List<GameObject>();
    
    public                 float      distanceBetweenPoints;
    public                 int        numSteps;
    public                 GameObject stepColliderObject;
    [NonSerialized] public Vector3    gravity;

    private void Awake()
    {
        for (int i = 0; i < numSteps; i++)
        {
            GameObject stepCollider = Instantiate(stepColliderObject);
            stepColliders.Add(stepCollider);
        }
    }

    private void Update()
    {
        UpdatePath(transform.position+transform.right*2, transform.right, gravity);
    }

    private void UpdatePath(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
    {
 
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(numSteps);
 
        Vector3 position = initialPosition;
        Vector3 currentVelocity = initialVelocity * distanceBetweenPoints;
        
        lineRenderer.SetPosition(0, transform.position);
        
        for (int i = 0; i < numSteps; ++i)
        {
            GameObject stepCollider = stepColliders[i];
            stepCollider.transform.position = position;
            Vector3 stepColliderGravity = stepCollider.GetComponent<GravityTrigger>().gravity;
            
            gravity = stepColliderGravity;

            lineRenderer.SetPosition(i+1, position);
 
            position += currentVelocity;
            currentVelocity += gravity;
        }

        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (other.CompareTag("Player"))
                other.GetComponent<Projectile>().currentIndex = 0;

            for (int i = 0; i < stepColliders.Count; i++)
                stepColliders[i].GetComponent<GravityTrigger>().hasTriggered = false;
            
            other.GetComponent<Rigidbody2D>().gravityScale = 0;
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.GetComponentInParent<PlayerController>().isTransporting = true;
            
            Projectile otherProjectile = other.gameObject.GetComponent<Projectile>();
            otherProjectile.enabled = true;
            otherProjectile.transformPoints = stepColliders;
            otherProjectile.currentTarget = otherProjectile.transformPoints[0].transform.position - other.transform.position;
        }
    }
    
    
}
