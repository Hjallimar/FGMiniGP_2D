using UnityEngine;

public class GravityTrigger : MonoBehaviour
{
    public  Vector3 gravity;
    public  float   gravityMultiplier = 30;
    public bool    hasTriggered;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !hasTriggered)
        {
            Projectile projectile = other.GetComponent<Projectile>();
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
