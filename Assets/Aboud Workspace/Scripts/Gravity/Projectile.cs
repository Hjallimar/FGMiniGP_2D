
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public  float          velocity;
    public  List<GameObject> transformPoints;
    public  Vector3          currentTarget;
    
    public int currentIndex = 0;

    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * currentTarget * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       Debug.Log("Current collision" + other.gameObject);
        if (other.CompareTag("transTarget"))
        {
            // currentIndex++;
            // if (currentIndex < transformPoints.Count)
            //     currentTarget = transformPoints[currentIndex].transform.position - transform.position;
            // else
            //     this.enabled = false;
            //

        }
    }
}
