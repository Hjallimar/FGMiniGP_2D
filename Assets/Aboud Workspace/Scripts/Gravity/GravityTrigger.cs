using System;
using UnityEngine;

public class GravityTrigger : MonoBehaviour
{
    public                 float          gravityMultiplier = 30;
    [NonSerialized] public Vector3        gravity;
    [NonSerialized] public bool           hasTriggered;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player") && !other.isTrigger && other.GetComponent<Projectile>().enabled)
        {
            Projectile projectile = other.GetComponent<Projectile>();
            Debug.Log(projectile.enabled);
            if (projectile.currentIndex < projectile.transformPoints.Count-1)
            {
                projectile.currentTarget = projectile.transformPoints[projectile.currentIndex].transform.position - projectile.transform.position;
                projectile.currentIndex++;
                hasTriggered = true;
            }
            else
            {
                projectile.enabled = false;
                other.GetComponent<Rigidbody2D>().gravityScale = 1;
                other.GetComponentInParent<PlayerController>().isTransporting = false;
            }

        }
        
        if (other.CompareTag("Finish"))
        {
            gravity = (other.transform.position - transform.position) * gravityMultiplier;
            gravity.y = 0;
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
