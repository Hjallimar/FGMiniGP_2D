using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPrediction : MonoBehaviour
{
    public  Vector3          velocity;
    public  Vector3          gravity;
    public  int              numSteps;
    public  GameObject       stepColliderObject;
    public  Projectile       projectile;
    private List<GameObject> stepColliders = new List<GameObject>();

    private void Awake()
    {
        
        for (int i = 0; i < numSteps; i++)
        {
            GameObject StepCollider = Instantiate(stepColliderObject);
            stepColliders.Add(StepCollider);

        }

        projectile.transformPoints = stepColliders;
        UpdatePath(transform.position, velocity, gravity);
    }

    private void Update()
    {
        UpdatePath(transform.position, velocity, gravity);
    }

    private void UpdatePath(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
    {
 
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(numSteps);
 
        Vector3 position = initialPosition;
        Vector3 currentVelocity = initialVelocity;
        
        
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
}
