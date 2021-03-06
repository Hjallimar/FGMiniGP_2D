using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapTrap : MonoBehaviour
{
    [SerializeField, Range(1, 10)]   private int   numberOfSwaps;
    [SerializeField, Range(1f, 10f)] private float timeBetweenSwaps;

    private PlayerController controller;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            controller = other.GetComponentInParent<PlayerController>();
            StartCoroutine("StartTrap");
        }
    }

    private IEnumerator StartTrap()
    {
        int counter = 0;
        while (counter < numberOfSwaps)
        {
            counter++;
            controller.Swap();
            yield return new WaitForSeconds(timeBetweenSwaps);

        }
        Destroy(this.gameObject);
    }
}
