
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public  Vector3          velocity;
    public  Vector3          gravity;
    public  List<GameObject> transformPoints;
    
    private Vector3 currentTarget;
    private int     currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentTarget = transformPoints[currentIndex].transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (velocity+currentTarget) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            gravity = (other.transform.position - transform.position)*0.05f;
            gravity.x = 0;
            gravity.z = 0;
        }
        
        if (other.CompareTag("transTarget"))
        {
            currentIndex++;
            if(currentIndex < transformPoints.Count)
                currentTarget = transformPoints[currentIndex].transform.position - transform.position;
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
