using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private ParticleSystem OnCollect;
    [SerializeField] private CircleCollider2D Collider;
    [SerializeField] private List<SpriteRenderer> Sprites;
    private float value = 0.5f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnCollect.Play();
            Collider.enabled = false;
            foreach (SpriteRenderer sprite in Sprites)
            {
                sprite.enabled = false;
            }
            Debug.Log("Shieet dawn I got collected");
            
        }
    }

    void Update()
    {
        Vector2 TransChange = new Vector2(value, 0.0f);
        transform.localScale -= (Vector3)TransChange * Time.deltaTime;
        if (transform.localScale.x < 0.2f)
        {
            value *= -1;
            transform.localScale = new Vector3(0.2f, 1,1);
        }
        else if (transform.localScale.x > 1.0f)
        {
            transform.localScale = new Vector3(1f, 1,1);
            value *= -1;
        }
    }
}

