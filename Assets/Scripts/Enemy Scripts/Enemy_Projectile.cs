using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile : MonoBehaviour {

    public int dmg;
    public float knockback;
    public float knockup;
    public float hitstun;
    public float activeTime;
    public bool ranged;
    public GameObject fireball;
    GameObject player;
    Collider2D col;
    // Use this for initialization
    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    void OnEnable()
    {
        StartCoroutine("AttackOnce", activeTime);
        if (ranged) Instantiate(fireball, transform.position, Quaternion.identity);
    }
    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.CompareTag("Player")) { DoDmg(enemy.gameObject); Debug.Log("Target Hit"); }
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
        enemy.GetComponent<PlayerStatus>().Knockback(transform.localScale.x, knockback, knockup);
    }
}
