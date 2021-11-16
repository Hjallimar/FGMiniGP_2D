using UnityEngine;

public class GravityTrigger : MonoBehaviour
{
    public Vector3 gravity;
    public float   gravityMultiplier = 30;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Projectile projectile = other.GetComponent<Projectile>();
            projectile.currentIndex++;
            if (projectile.currentIndex < projectile.transformPoints.Count)
                projectile.currentTarget = projectile.transformPoints[projectile.currentIndex].transform.position - projectile.transform.position;
            else
                projectile.enabled = false;
            
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
