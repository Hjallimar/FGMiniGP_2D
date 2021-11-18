
using System;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public                 float            velocity;
    [NonSerialized] public                 List<GameObject> transformPoints;
    [NonSerialized] public                 Vector3          currentTarget;
   public int              currentIndex = 0;

    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * currentTarget * Time.deltaTime;
    }

    
}
