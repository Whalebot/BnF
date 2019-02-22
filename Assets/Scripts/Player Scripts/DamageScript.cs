using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public int dmg;
    public float knockback;
	public float launch;
    public float activeSeconds = 0.1F;
    public float hitstun;
    Collider2D col;
    // Use this for initialization
    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnEnable()
    {
        StartCoroutine("AttackOnce",activeSeconds);
    }
    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.CompareTag("Enemy")) { Debug.Log("Target Hit"); }
    }

    IEnumerator AttackOnce(float dur)
    {
        col.enabled = true;
        yield return new WaitForSeconds(dur);
        col.enabled = false;
    }
/*

    void DoDmg(GameObject enemy)
    {
        enemy.GetComponent<EnemyScript>().Hitstun(hitstun);
        enemy.GetComponent<EnemyScript>().TakeDamage(dmg);
        enemy.GetComponent<EnemyScript>().Knockback(new Vector2(transform.parent.position.x, transform.parent.position.y-launch), knockback);
    }*/
}
