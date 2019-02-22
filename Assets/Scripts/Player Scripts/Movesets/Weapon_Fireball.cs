using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Fireball : MonoBehaviour
{

    public int delay;
    int delayCounter;
    public float velocity;
    public float deathTime;
    public float maxLifetime;
    int lifetime;
    public GameObject hitParticle;
    public int poiseDamage;
    public int dmg;
    public float knockback;
    public float knockup;
    public float hitstun;
    public float chargeAmount;
    public int activeFrames = 1;
    public bool singleHit = true;
    public List<GameObject> enemyList;
    int activeCounter;
    bool activated;
    Collider2D col;
    SpriteRenderer SR;

    public bool stopOnHit = true;

    Rigidbody2D rb;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        col = GetComponent<Collider2D>();
        transform.localScale = new Vector2(Mathf.Sign(player.transform.localScale.x) * transform.localScale.x, transform.localScale.y);
        rb = GetComponent<Rigidbody2D>();
        SR.enabled = false;
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        //if (startTime + maxLifetime < Time.time) StartCoroutine("Death");
    }

    void FixedUpdate()
    {
        if (activated)
        {
            activeCounter++;
        }
        lifetime++;
        delayCounter++;
        if (lifetime > maxLifetime) StartCoroutine("Death");
        if (delayCounter >= delay)
        {
            StartCoroutine("AttackOnce");
            if (!activated)
            {
                col.enabled = true;
                SR.enabled = true;
            }
            activated = true;

            rb.velocity = new Vector2(transform.localScale.x * velocity, 0);
        }

    }
    IEnumerator AttackOnce()
    {
        while (activeCounter < activeFrames) yield return new WaitForEndOfFrame();
        col.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.CompareTag("Enemy") || enemy.CompareTag("Boss"))
        {
            Instantiate(hitParticle, enemy.transform.position, Quaternion.identity);
            // if (enemy.gameObject != null && enemy.gameObject. != null)
            DoDmg(enemy.gameObject);
            if (stopOnHit) StartCoroutine("Death");

        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTime);
        SR.enabled = false;
        col.enabled = false;
        yield return new WaitForSeconds(0);
        Destroy(gameObject);
    }

    void DoDmg(GameObject enemy)
    {


        if (player != null)
        {
            if (singleHit)
            {
                if (!enemyList.Contains(enemy))
                {
                    enemyList.Add(enemy);
                    player.GetComponent<PlayerStatus>().special += chargeAmount;
                    enemy.GetComponent<EnemyScript>().Hitstun(hitstun, poiseDamage);
                    enemy.GetComponent<EnemyScript>().TakeDamage(dmg);
                    enemy.GetComponent<EnemyScript>().Knockback(player.transform.position, knockback, knockup);
                }
            }

        }
    }
}

