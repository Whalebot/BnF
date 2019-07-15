using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp_Strike : MonoBehaviour
{

    public bool warp;
    public bool backWarp;
    public float distance;
    Transform warpTarget;
    public int delay;
    int delayCounter;

    public float hitPause;
    bool hitPauseOnce;
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
    PlayerStatus playerStatus;

    public bool freeze;
    public int freezeFrames;

    public bool pulling;

    public GameObject pullTarget;


    [HeaderAttribute("Recoil attributes")]
    public bool hasRecoil = false;
    public float upRecoil;
    public float sideRecoil;

    [HeaderAttribute("Ground Bounce")]
    public bool hasGroundBounce;
    public float gBKnockback;
    public float gBKnockup;
    public float gBHitstun;

    [HeaderAttribute("Clash attributes")]
    public bool canClash = false;
    public bool clashActive = false;
    public int clashFrames;
    public float hitstopframes;
    int clashCounter;
    public GameObject clashFX;

    int RNGCount;

    public bool stopOnHit = true;

    Rigidbody2D rb;
    GameObject player;
    Weapon_Attackscript weaponScript;
    HitStopScript hitStopScript;
    UIManager uiManager;
    GameObject manager;
    // Use this for initialization
    void Awake()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        manager = GameObject.FindGameObjectWithTag("Manager");
        if (manager != null)
        {

            hitStopScript = manager.GetComponent<HitStopScript>();
            uiManager = manager.GetComponent<UIManager>();
        }



        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        weaponScript = GameObject.FindGameObjectWithTag("Iris").GetComponent<Weapon_Attackscript>();
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
            warpTarget = enemy.transform;
            Warp();
            Instantiate(hitParticle, enemy.transform.position, Quaternion.identity);
            // if (enemy.gameObject != null && enemy.gameObject. != null)
            DoDmg(enemy.gameObject);
            if (stopOnHit)
            {
                StartCoroutine("Death");
                Destroy(gameObject);
            }

        }
    }

    void Warp()
    {
        if (backWarp)
        {
           

            player.transform.position = new Vector3(warpTarget.position.x, player.transform.position.y, transform.position.z) + 4 * transform.right * Mathf.Sign(transform.localScale.x);
            player.transform.localScale = new Vector3(-player.transform.localScale.x, player.transform.localScale.y, player.transform.localScale.z);
        }
        else
            player.transform.position = transform.position;
        weaponScript.AttackCancel();
        weaponScript.ExtraMove(1);

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
                {/*
                    Instantiate(hitParticle, enemy.transform.GetChild(0).transform.position, Quaternion.Euler(0, 0, 15 * RNGCount));
                    enemyList.Add(enemy);
                    playerStatus.special += chargeAmount;

                    if (manager != null)
                        if (enemy.GetComponent<EnemyScript>().stun && !HitStopScript.hitStop && !hitPauseOnce)
                        {
                            hitStopScript.HitStop(hitPause); hitPauseOnce = true;
                        }

                    if (hasRecoil) GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().AddRecoil(sideRecoil, upRecoil);
                    if (manager != null) uiManager.ComboUp();
                    enemy.GetComponent<EnemyScript>().Hitstun(hitstun, poiseDamage);
                    enemy.GetComponent<EnemyScript>().TakeDamage(dmg);


                
                    if (pulling && enemy.GetComponent<EnemyScript>().stun) enemy.GetComponent<EnemyScript>().Pull(pullTarget.transform.position, 0.3F);
                    else enemy.GetComponent<EnemyScript>().Knockback(player.transform.position, knockback, knockup);


                    if (hasGroundBounce) { enemy.GetComponent<EnemyScript>().GroundBounce(transform.parent.parent.position, gBKnockback, gBKnockup, gBHitstun); print("poi"); }

                    if (freeze) { enemy.GetComponent<EnemyScript>().Freeze(freezeFrames); 
                    
                    }*/

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

