using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instant_Projectile : MonoBehaviour
{

    public GameObject projectile;

    public bool pillar;
    public int pillarDelay;
    int pillarCounter;
    public int postLifetime;
    bool dying;
    int deathCounter;
    public GameObject hitParticle;
    Rigidbody2D rb;

    public LayerMask pillarCollision;
    public float rayLength;
    GameObject target;
    RaycastHit2D downRay;

    public bool doesCollide;
    public int velocity;
    public int dmg;
    public float knockback;
    public float knockup;
    public float hitstun;
    public bool homing;

    int RNGCount;


    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        if (pillar)
        {
            downRay = Physics2D.Raycast(target.transform.position, Vector2.down, rayLength, pillarCollision);
            transform.position = downRay.point;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dying) deathCounter++;
        if (pillar)
        {
            pillarCounter++;
            if (downRay && pillarCounter > pillarDelay && !dying) { StartCoroutine("Destroy"); Instantiate(projectile, transform.position, Quaternion.identity); dying = true; }
        }

    }

    public IEnumerator Destroy()
    {

        while (deathCounter < postLifetime) yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }

    void DoDmg(GameObject enemy)
    {
        enemy.GetComponent<PlayerStatus>().TakeDamage(dmg);
        enemy.GetComponent<PlayerStatus>().Hitstun(hitstun);
        enemy.GetComponent<PlayerStatus>().Knockback(transform.localScale.normalized.x, knockback, knockup);
    }

    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (doesCollide)
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

    }
}
