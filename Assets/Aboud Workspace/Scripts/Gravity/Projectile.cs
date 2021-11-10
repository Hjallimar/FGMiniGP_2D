using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 initialVelocity;
    public Vector3 velocity;
    public Vector3 gravity;

    
    // Start is called before the first frame update
    void Start()
    {
        initialVelocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity *Time.deltaTime;
        velocity += gravity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            gravity = (other.transform.position - transform.position)*0.05f;
            gravity.x = 0;
            gravity.z = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            gravity = Vector3.zero;

        }
    }
}
