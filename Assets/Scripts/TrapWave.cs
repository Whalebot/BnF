using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapWave : MonoBehaviour {
    Rigidbody2D rb;
    Vector2 direction;
    public int velocity;
    public int lifetime;
    int deathCounter;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * velocity;
    }

    void OnTriggerEnter2D(Collider2D enemy)
    {

        if (enemy.CompareTag("Wall")) Destroy(gameObject);
    }

    void FixedUpdate()
    {
        deathCounter++;
        if (deathCounter >= lifetime) Destroy(gameObject);

    }
}