using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Parry : MonoBehaviour
{

    [HeaderAttribute("Hit attributes")]
    public int dmg;
    public float knockback;
    public float knockup;
    public float hitstun;
    public float activeTime;

    public float parryStartup;
    public float parryActive;
    public float parryRecovery;

    public GameObject hitParticle;
    Collider2D col;


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
        col.enabled = true;
          StartCoroutine("AttackOnce", activeTime);

    }
    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.CompareTag("Attack"))
        {
            col.enabled = false;
          //  gameObject.transform.parent.parent.GetComponent<Boss_AttackScript>().InterruptAttack();
            gameObject.transform.parent.GetComponent<Enemy_Weaponscript>().ExtraMove();
            print("Beautiful");
        }
    }

    void OnDisable() { }

    IEnumerator AttackOnce(float dur)
    {
        col.enabled = true;
        yield return new WaitForSeconds(dur);
        col.enabled = false;
    }
}
