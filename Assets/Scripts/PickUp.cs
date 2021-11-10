using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
    [SerializeField] private ParticleSystem OnCollect;
    [SerializeField] private CircleCollider2D Collider;
    [SerializeField] private List<SpriteRenderer> Sprites;
    [SerializeField] private UnityEvent OnTriggerCallBack;
    private float value = 0.5f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnTriggerCallBack.Invoke();
            SetActivation(false);
        }
    }

    public void SetActivation(bool Status)
    {
        Collider.enabled = Status;
        foreach (SpriteRenderer sprite in Sprites)
        {
            sprite.enabled = Status;
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

