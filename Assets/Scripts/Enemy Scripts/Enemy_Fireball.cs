using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fireball : MonoBehaviour
{
    public int velocity;
    public int dmg;
    public float knockback;
    public float knockup;
    public float hitstun;

    public int activeDamageFrames;

    public bool horizontalOnly;
    public bool homing;
    public bool breakable = true;
    public GameObject breakParticle;
    public bool hitBreak;
    public bool random;
    public bool collissionBreak;
    public int lifetime;
    int deathCounter;
    public int directionChange;
    int directionCounter;

    GameObject target;

    int RNGCount;
    public GameObject hitParticle;
    Rigidbody2D rb;
    Vector2 direction;
    public Quaternion q;

    void Awake() { target = GameObject.FindGameObjectWithTag("Player"); }

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        if (random) directionChange = Random.Range(0, directionChange);
        directionCounter = directionChange;

        if (horizontalOnly)
        {
            if (transform.rotation.y != 0)
            { transform.rotation = Quaternion.Euler(0, 0, 0); transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); }
        }
    }

    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.CompareTag("Attack") && breakable) BreakObject();

        if (deathCounter < activeDamageFrames)
        {


            if (enemy.CompareTag("Player") && !enemy.CompareTag("Attack"))
            {
                if (enemy.gameObject.layer != LayerMask.NameToLayer("Invul"))
                {
                    DoDmg(enemy.gameObject);
                    RNGCount = Random.Range(-3, 4);
                    Instantiate(hitParticle, enemy.transform.position, Quaternion.Euler(0, 0, 15 * RNGCount));
                }
            }

        }
    }

    void OnTriggerStay2D(Collider2D enemy)
    {
        if (enemy.CompareTag("Platform") && collissionBreak) BreakObject();
        if (enemy.CompareTag("Player") && hitBreak) BreakObject();
    }

    public void BreakObject()
    {
        if (breakParticle != null) Instantiate(breakParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        deathCounter++;
        if (deathCounter >= lifetime) Destroy(gameObject);

        directionCounter++;
        if (horizontalOnly) {
            rb.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * velocity,0);
       //     if (transform.rotation.y != 0)
     //       { transform.rotation = Quaternion.Euler(0, 0, 0); transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); }
         //   else rb.velocity = Vector2.left * velocity;
        } 
        if (directionCounter > directionChange && homing) { rb.velocity = (target.transform.position - transform.position).normalized * velocity; }
        //    if (directionCounter > directionChange && homing) { rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0) + (target.transform.position - transform.position).normalized * velocity; directionCounter = 0; }
        //   if (homing) rb.velocity = (target.transform.position-transform.position).normalized * velocity;
        // if (homing) rb.velocity = new Vector2(((transform.position - target.transform.position).normalized * velocity).x, ((transform.position - target.transform.position).normalized * velocity).y);
        //if (homing) rb.velocity = rb.velocity + new Vector2(((transform.position - target.transform.position).normalized * velocity).x, ((transform.position - target.transform.position).normalized * velocity).y);

    }

    void DoDmg(GameObject enemy)
    {
        enemy.GetComponent<PlayerStatus>().TakeDamage(dmg);
        enemy.GetComponent<PlayerStatus>().Hitstun(hitstun);
        enemy.GetComponent<PlayerStatus>().Knockback((Mathf.Sign((target.transform.position - transform.localScale).x)), knockback, knockup);
    }
}
