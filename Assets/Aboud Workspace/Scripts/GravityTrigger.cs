using UnityEngine;

public class GravityTrigger : MonoBehaviour
{
    public Vector3 gravity;
    public float   gravityMultiplier = 30;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            gravity = (other.transform.position - transform.position) * gravityMultiplier;
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
