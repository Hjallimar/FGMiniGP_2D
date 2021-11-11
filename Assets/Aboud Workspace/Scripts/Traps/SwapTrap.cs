using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapTrap : MonoBehaviour
{
    [SerializeField] private int   numberOfSwaps;
    [SerializeField] private float timeBetweenSwaps;

    private PlayerController controller;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
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
        GetComponent<Collider2D>().enabled = true;
    }
}
