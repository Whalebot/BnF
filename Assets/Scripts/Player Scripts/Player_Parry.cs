using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Parry : MonoBehaviour
{
    public int extraID;
    public bool triggered;
    public GameObject sfx;
 //   public GameObject vfx;
    public GameObject sfx2;
    public bool active;
    public int activeFrames = 1;
    public int durationCounter = 1;
    public int hitPause;

    public int meterGain;

    public GameObject hitParticle;
    public LayerMask currentLayer;
    PlayerStatus playerStatus;
    Collider2D col;
    HitStopScript hitStopScript;
    int RNGCount;

    // Use this for initialization
    void Awake()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        hitStopScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<HitStopScript>();
        col = GetComponent<Collider2D>();
        durationCounter = activeFrames;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Active();
        if (playerStatus.stuncounter > 0 || playerStatus.hitInvul) Reset();
    }

    void OnEnable()
    {
        // StartCoroutine("AttackOnce", activeFrames);
        ParryState();
    }

    void ParryState() {
        active = true;
        col.enabled = true;
        transform.parent.parent.gameObject.layer = LayerMask.NameToLayer("Invul");
        transform.parent.parent.gameObject.GetComponent<Rigidbody2D>().mass = 10000;

    }

    void DisableParry() {
        active = false;
        col.enabled = false;
        transform.parent.parent.gameObject.layer = LayerMask.NameToLayer("Player");
        transform.parent.parent.gameObject.GetComponent<Rigidbody2D>().mass = 1;
        Reset();
    }

    void OnDisable()
    {
        Reset();
        if (extraID == 0) DisableParry();
    }

    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.CompareTag("EnemyAttack") || enemy.CompareTag("EnemyProjectile"))
        {
            if (!triggered && playerStatus.canTakeDmg)
            {
                hitStopScript.HitStop(hitPause);
                Instantiate(sfx, transform.position,Quaternion.identity);
           //     Instantiate(sfx2);
                triggered = true;
                transform.parent.parent.gameObject.layer = LayerMask.NameToLayer("Invul");
                StartCoroutine("ParryStart");
                enemy.gameObject.SetActive(false);
                //        transform.parent.GetComponent<Weapon_Attackscript>().ExtraMove();
                print("PARRY!");
                playerStatus.special += meterGain;
            }

        }
    }
    IEnumerator ParryStart()
    {
        while (HitStopScript.hitStop) yield return new WaitForEndOfFrame();
        if (extraID == 0) { transform.parent.GetComponent<Weapon_Attackscript>().AttackCancel(); DisableParry(); }
        if (extraID == 1)transform.parent.GetComponent<Weapon_Attackscript>().ExtraMove(1);
        if (extraID == 2) transform.parent.GetComponent<Weapon_Attackscript>().ExtraMove(2);
    }
    void Active()
    {

        if (active) { durationCounter--; }
        if (!triggered && durationCounter <= 0)
        {
            //   transform.parent.parent.GetComponent<Player_AttackScript>().attackID = 4;
            col.enabled = false;
            transform.parent.parent.gameObject.layer = LayerMask.NameToLayer("Player"); 
        }
    }

    void Reset() { triggered = false; active = false; durationCounter = activeFrames; gameObject.SetActive(false); }

    IEnumerator AttackOnce(float dur)
    {
        col.enabled = true; print("auto start");
        yield return new WaitForSeconds(dur);
        col.enabled = false;
        yield return new WaitForFixedUpdate();
        if (!triggered) { transform.parent.parent.gameObject.layer = LayerMask.NameToLayer("Player"); print("auto end"); }


    }
}
