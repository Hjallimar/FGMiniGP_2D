using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Trampoline trampoline;
    [SerializeField] private bool       withAnimation;
    private                  Animator   anim;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(withAnimation)
                anim.SetTrigger("Press");
            trampoline.LaunchPlayer();
        }
    }
}
