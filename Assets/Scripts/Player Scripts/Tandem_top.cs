using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tandem_top : MonoBehaviour
{


    public int dmg;
    public int poiseDamage;
    public float knockback;
    public float knockup;
    public float hitstun;
    public float chargeAmount = 1;
    public float hitPause = 0.1F;
    public int postLifetime;
    bool hasHit;

    public float activeFrames = 0.1F;

    public bool pulling;

    public int multiHitDelay;
    int multiHitCounter;

    public GameObject hitParticle;
    public GameObject pullTarget;

    PlayerStatus playerStatus;
    Collider2D col;
    int RNGCount;

    [HeaderAttribute("Recoil attributes")]
    public bool hasRecoil = false;
    public float upRecoil;
    public float sideRecoil;


    // Use this for initialization
    void Awake()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        col = GetComponent<Collider2D>();
        multiHitCounter = multiHitDelay;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hasHit) postLifetime--;
    }
    void OnEnable()
    {
        StartCoroutine("AttackOnce", activeFrames);
    }

    void OnTriggerStay2D(Collider2D enemy)
    {
        {
            if (enemy.CompareTag("Enemy") || enemy.CompareTag("Boss"))
            {
                if (multiHitCounter >= multiHitDelay)
                {
                    if (!hasHit) StartCoroutine("DelayDestroy");
                    hasHit = true;
                    multiHitCounter = 0;
                    DoDmg(enemy.gameObject);
                    RNGCount = Random.Range(-3, 4);
                    Instantiate(hitParticle, enemy.transform.position, Quaternion.Euler(0, 0, 15 * RNGCount));
                }
                else multiHitCounter++;

            }
            else if (enemy.CompareTag("EnemyProjectile"))
            {
                if (multiHitCounter >= multiHitDelay)
                {
                    if(!hasHit)StartCoroutine("DelayDestroy");
                    hasHit = true;
                    RNGCount = Random.Range(-3, 4);
                    Instantiate(hitParticle, enemy.transform.position, Quaternion.Euler(0, 0, 15 * RNGCount));
                }
                else multiHitCounter++;
            }
        }
    }

    IEnumerator DelayDestroy() {
        
        while(postLifetime > 0)  yield return new WaitForEndOfFrame(); 
        Destroy(gameObject);
    }

    IEnumerator AttackOnce(float dur)
    {
        col.enabled = true;
        yield return new WaitForSeconds(dur * Time.deltaTime);
        col.enabled = false;
    }

    void DoDmg(GameObject enemy)
    {
        if (enemy.name.Contains("Boss"))
        {
            playerStatus.special += chargeAmount;
            if (hasRecoil) GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().AddRecoil(sideRecoil, upRecoil);
            GameObject.FindGameObjectWithTag("Manager").GetComponent<UIManager>().ComboUp();
            enemy.GetComponent<Boss_Script>().Hitstun(hitstun, poiseDamage);
            enemy.GetComponent<Boss_Script>().TakeDamage(dmg);
            if (pulling && enemy.GetComponent<EnemyScript>().stun) enemy.GetComponent<EnemyScript>().Pull(pullTarget.transform.position, 0.3F);
            else enemy.GetComponent<EnemyScript>().Knockback(transform.localScale, knockback, knockup);
            if (enemy.GetComponent<Boss_Script>().canKnockBack) GameObject.FindGameObjectWithTag("Manager").GetComponent<HitStopScript>().HitStop(hitPause);
        }
        else
        {
            playerStatus.special += chargeAmount;
            if (hasRecoil) GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().AddRecoil(sideRecoil, upRecoil);
            GameObject.FindGameObjectWithTag("Manager").GetComponent<UIManager>().ComboUp();
            enemy.GetComponent<EnemyScript>().Hitstun(hitstun, poiseDamage);
            enemy.GetComponent<EnemyScript>().TakeDamage(dmg);

            if (pulling && enemy.GetComponent<EnemyScript>().stun) enemy.GetComponent<EnemyScript>().Pull(pullTarget.transform.position, 0.3F);
            else enemy.GetComponent<EnemyScript>().Knockback(transform.localScale, knockback, knockup);
            if (enemy.GetComponent<EnemyScript>().stun) GameObject.FindGameObjectWithTag("Manager").GetComponent<HitStopScript>().HitStop(hitPause);
        }
    }
}
