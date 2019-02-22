using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Enemy : MonoBehaviour
{
    public int velocity;
    public int dmg;
    public float knockback;
    public float knockup;
    public float hitstun;
    public float activeTime;
    public float magnitude;
    int RNGCount;
    public GameObject hitParticle;
    Collider2D col;
    Rigidbody2D rb;
    GameObject target;
    public float targetDistance;
    public Vector2 directionVector;
    public bool rotates = true;
    public float angle;
    // Use this for initialization
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        targetDistance = (target.transform.position - transform.position).magnitude;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 360.0f - Vector3.Angle(transform.right, rb.velocity.normalized)));
        if (transform.rotation.eulerAngles.y == 0) transform.Rotate(new Vector3(0, 0, 360.0f - Vector3.Angle(transform.right, rb.velocity.normalized)));
        else transform.Rotate(new Vector3(0, 0, 360.0f + Vector3.Angle(transform.right, rb.velocity.normalized)));
    }


    void Start()
    {
        directionVector = (target.transform.position - transform.position).normalized;
        if (transform.rotation.eulerAngles.y == 0 && directionVector.x > 0 || transform.rotation.eulerAngles.y == 180 && directionVector.x < 0)
        {

            rb.velocity = (target.transform.position - transform.position + Vector3.up * (Mathf.Pow(targetDistance / 10F, 2) - 1)).normalized * velocity;
            transform.rotation = Quaternion.Euler(0, transform.rotation.y, Vector2.Angle(rb.velocity, Vector2.right));
        }
        else
        {
            print("Miss");
            rb.velocity = new Vector2(-directionVector.x, directionVector.y).normalized * velocity;
            transform.rotation = Quaternion.Euler(0, transform.rotation.y, Vector2.Angle(rb.velocity, Vector2.right));
        }
    }

    void OnTriggerEnter2D(Collider2D enemy)
    {
        rotates = false;
        if (enemy.CompareTag("Attack")) Destroy(gameObject);

        if (enemy.CompareTag("Player") && !enemy.CompareTag("Attack"))
        {
            if (enemy.gameObject.layer != LayerMask.NameToLayer("Invul"))
            {
                DoDmg(enemy.gameObject);
                RNGCount = Random.Range(-3, 4);
                Instantiate(hitParticle, enemy.transform.position, Quaternion.Euler(0, 0, 15 * RNGCount));
            }
        }
        if (enemy.CompareTag("Player") || enemy.CompareTag("Platform")) Destroy(gameObject);
    }

    IEnumerator AttackOnce(float dur)
    {
        col.enabled = true;
        yield return new WaitForSeconds(dur);
        col.enabled = false;
    }

    void DoDmg(GameObject enemy)
    {
        enemy.GetComponent<PlayerStatus>().TakeDamage(dmg);
        enemy.GetComponent<PlayerStatus>().Hitstun(hitstun);
        if (transform.rotation.x == 0) 
        enemy.GetComponent<PlayerStatus>().Knockback(1, knockback, knockup);
        else enemy.GetComponent<PlayerStatus>().Knockback(-1, knockback, knockup);
    }
}
