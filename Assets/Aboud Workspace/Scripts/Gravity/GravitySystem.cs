using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySystem : MonoBehaviour
{
    public GravityObject[] gravityObjects;
    private float        gravity;
    private Vector3      currentVelocity;
    private Vector3      dir;
    private Vector3[]    orbitPoints;
    private int          maxCount = 10000;
    private int          simplify = 5;
    private int          privateMaxCount;
    private LineRenderer lineRenderer;

    private void Start()
    {
        currentVelocity = transform.up;
        privateMaxCount = maxCount;
        orbitPoints = new Vector3[privateMaxCount];
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.SetWidth(0.2f, 0.2f);
        ComputeTrajectory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            ComputeTrajectory();
    }

    private void ComputeTrajectory()
    {
        currentVelocity = transform.up;

        float angle = 0;
        float dt = Time.fixedDeltaTime;
        Vector3 pos = transform.position;
        Vector3 lastPos = pos;

        Vector3 velocity = currentVelocity;
        Vector3 accel = AccelerationCalc(gravityObjects, pos);
        float tempAngleSum = 0;
        int step = 0;
        while (angle < 360 && step < privateMaxCount * simplify)
        {
            if (step % simplify == 0)
            {
                orbitPoints[step / simplify] = pos;
                angle += tempAngleSum;
                tempAngleSum = 0;
            }

            accel = AccelerationCalc(gravityObjects, pos);
            velocity += accel * dt;
            pos += velocity * dt;
            if (gravityObjects.Length == 1)
            {
                tempAngleSum += Mathf.Abs(Vector3.Angle(pos, lastPos));
            }

            lastPos = pos;
            step++;
            
        }
        lineRenderer.SetVertexCount(step / simplify);
        for (int i = 0; i < step/simplify; i++)
        {
            lineRenderer.SetPosition(i, orbitPoints[i]);
        }
        
    }

    private Vector3 AccelerationCalc(GravityObject[] goArray, Vector3 simPos)
    {
        Vector3 accel = Vector3.zero;
        for (int i = 0; i < goArray.Length; i++)
        {
            dir = goArray[i].trans.position - simPos;
            gravity = goArray[i].g;
            accel += dir.normalized * gravity / dir.sqrMagnitude;
        }
        return accel;
    }
}

