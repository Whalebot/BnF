using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDmg : MonoBehaviour
{
    [HeaderAttribute("Hit attributes")]
    public int dmg;
    public float knockback;
    public float knockup;
    public float hitstun;
    public float activeTime;
    public GameObject hitParticle;
    int RNGCount;
    SpriteRenderer SR;
    [HeaderAttribute("Ranged attributes")]
    public bool ranged;
    public bool aimShot = false;
    public bool spread;
    public bool noRotation;
    public GameObject fireball;
    Collider2D col;
    public bool blank;
    GameObject target;
    Vector3 direction;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        SR = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            direction = ((target.transform.position + Vector3.down) - transform.position).normalized;
    }
    void OnEnable()
    {
        if (!ranged && !blank) { SR.enabled = true; StartCoroutine("AttackOnce", activeTime); }

        if (ranged && noRotation) Instantiate(fireball, transform.parent.parent.position, transform.parent.parent.rotation);
        else if (ranged && !aimShot && !noRotation)
        {
            print("potato");
            if (transform.parent.parent.localScale.x > 0) { Instantiate(fireball, transform.parent.parent.position, Quaternion.Euler(0, 0, 0)); }
            else Instantiate(fireball, transform.parent.parent.position, Quaternion.Euler(0, 180, 0));
        }
        else if (ranged && aimShot && !spread)
        {
            if (transform.parent.parent.localScale.x > 0)
            {
                Instantiate(fireball, transform.parent.parent.position, Quaternion.Euler(0, 0, 360.0f - Vector3.Angle(Vector3.up, direction)));
            }
            else
            {
                Instantiate(fireball, transform.parent.parent.position, Quaternion.Euler(0, 0, 360.0f + Vector3.Angle(Vector3.up, direction)));
            }
        }

        else if (ranged && aimShot && spread)
        {

            if (transform.parent.parent.localScale.x > 0)
            {
                Instantiate(fireball, transform.parent.parent.position, Quaternion.identity);
                Instantiate(fireball, transform.parent.parent.position + Vector3.up * 5, Quaternion.identity);
                Instantiate(fireball, transform.parent.parent.position - Vector3.up * 5, Quaternion.identity);
            }
            else
            {
                Instantiate(fireball, transform.parent.parent.position, Quaternion.Euler(0, 180, 0));
                Instantiate(fireball, transform.parent.parent.position + Vector3.up * 5, Quaternion.Euler(0, 180, 0));
                Instantiate(fireball, transform.parent.parent.position - Vector3.up * 5, Quaternion.Euler(0, 180, 0));
            }
        }

    }
    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.CompareTag("Player") && !ranged)
        {
            DoDmg(enemy.gameObject);
            RNGCount = Random.Range(-3, 4);

        }
    }

    IEnumerator AttackOnce(float dur)
    {
        col.enabled = true;
        yield return new WaitForSeconds(dur * Time.deltaTime);
        col.enabled = false;
        SR.enabled = false;
    }

    void DoDmg(GameObject enemy)
    {
        if (enemy.GetComponent<PlayerStatus>().canTakeDmg == true) Instantiate(hitParticle, enemy.transform.position, Quaternion.Euler(0, 0, 15 * RNGCount));
        enemy.GetComponent<PlayerStatus>().TakeDamage(dmg);
        enemy.GetComponent<PlayerStatus>().Hitstun(hitstun);
        enemy.GetComponent<PlayerStatus>().Knockback(transform.parent.parent.localScale.x, knockback, knockup);
    }
}
