using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
    [SerializeField] private CircleCollider2D Collider;
    [SerializeField] private List<SpriteRenderer> Sprites;
    [SerializeField] private UnityEvent OnTriggerCallBack;
    [SerializeField, Range(10.0f, 180.0f)] private float RotateSpeed = 10.0f;
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
        transform.RotateAround(transform.position, Vector3.up, RotateSpeed * Time.deltaTime);
    }
}

