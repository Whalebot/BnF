using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooSpike : MonoBehaviour {
    public int velocity;
    public int dmg;
    public float knockback;
    public float knockup;
    public float hitstun;
    int RNGCount;
    public GameObject hitParticle;
  //  GameObject target;
  //  Collider2D col;
    Rigidbody2D rb;
    Vector2 direction;
    public Quaternion q;
    void Awake()
    {
     //   target = GameObject.FindGameObjectWithTag("Player");
     //   col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        rb.velocity = transform.TransformDirection(Vector2.up) * velocity;
    }

    void OnTriggerEnter2D(Collider2D enemy)
    {

        if (enemy.CompareTag("Attack")) Destroy(gameObject);

        if (enemy.CompareTag("Player") && !enemy.CompareTag("Attack"))
        {
            DoDmg(enemy.gameObject);
            RNGCount = Random.Range(-3, 4);
            Instantiate(hitParticle, enemy.transform.position, Quaternion.Euler(0, 0, 15 * RNGCount));
        }
        if (enemy.CompareTag("Player") || enemy.CompareTag("Platform")) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
    }

    void DoDmg(GameObject enemy)
    {
        enemy.GetComponent<PlayerStatus>().TakeDamage(dmg);
        enemy.GetComponent<PlayerStatus>().Hitstun(hitstun);
        enemy.GetComponent<PlayerStatus>().Knockback(Mathf.Sign(rb.velocity.x), knockback, knockup);
    }
}
