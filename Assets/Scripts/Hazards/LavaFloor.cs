using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class LavaFloor : MonoBehaviour
{
    
    [SerializeField] private float Width = 10.0f;
    [SerializeField] private float Height = 10.0f;
    [SerializeField, Range(0.1f, 1.0f)] private float MoveSpeed = 1.0f;
    [SerializeField] private GameObject LavaSprite;
    [SerializeField] private GameObject LavaBackground;
    private BoxCollider2D LavaCollider;
    private Vector2 StartPosition;
    void Start()
    {
        StartPosition = transform.position;
    }

    private void OnDrawGizmos()
    {
        if (!LavaCollider)
        {
            LavaCollider = GetComponent<BoxCollider2D>();
        } 
        LavaCollider.offset = new Vector3(0, -.5f,0);
        LavaCollider.size = new Vector2(Width, 1);
        LavaSprite.transform.localScale = new Vector3(Width, 1,0);
        LavaSprite.transform.localPosition = new Vector3(0, -.5f,0);
        LavaBackground.transform.localScale = new Vector3(Width, Height,0);
        LavaBackground.transform.localPosition = new Vector2(0.0f,-Height * 0.5f);
    }

    void Update()
    {
        transform.position += Vector3.up * MoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Kill ze player");
        }
    }
}
