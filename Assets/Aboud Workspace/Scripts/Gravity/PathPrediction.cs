using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPrediction : MonoBehaviour
{
    public float      distanceBetweenPoints;
    public Vector3    gravity;
    public int        numSteps;
    public GameObject stepColliderObject;
    public Projectile projectile;

    private List<GameObject> stepColliders = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < numSteps; i++)
        {
            GameObject StepCollider = Instantiate(stepColliderObject);
            stepColliders.Add(StepCollider);
        }
    }

    private void Update()
    {
        UpdatePath(transform.position, transform.right, gravity);
    }

    private void UpdatePath(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
    {
 
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(numSteps);
 
        Vector3 position = initialPosition;
        Vector3 currentVelocity = initialVelocity * distanceBetweenPoints;
        
        
        for (int i = 0; i < numSteps; ++i)
        {
            GameObject stepCollider = stepColliders[i];
            stepCollider.transform.position = position;
            Vector3 stepColliderGravity = stepCollider.GetComponent<GravityTrigger>().gravity;
            
            gravity = stepColliderGravity;

            lineRenderer.SetPosition(i, position);
 
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
