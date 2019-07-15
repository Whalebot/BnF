using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [HeaderAttribute("Generic attributes")]
    public int enemyType;
    public bool isStoryMode;
    public bool knockout;
    

    public int health;
    public int triggerHealth;
    public int special;
    public int mode = 0;
    public LayerMask modeLayers;
    public GameObject knockdownSound;

    public bool isBoss;
    public bool requiresTrigger;
    public bool detected;

    [HeaderAttribute("Stun attributes")]
    public float comboCount;
    public float hitstunScaling;
    public float minimumScaling = 0.25F;
    public float hitstun;
    public float hitstunReductionAir = 0.5F;
    public bool stun;
    public bool canDizzy;
    public bool dizzy;
    public bool hyperArmor;
    public bool poiseArmor;
    public float poise;
    public float poiseHealth;
    public bool willRetreat = true;
    public bool canInterrupt = true;
    public bool canKnockback = true;


    public int moveDamage;
    public int comboDamage;
    public int comboHits;
    public float hitStunBar;

    //Ground bounce
    bool willGroundBounce;
    Vector2 gBSource;
    float gBKnockback;
    float gBKnockup;
    float gBHitstun;



    //Getting pulled
    Vector2 pullTarget;
    bool pulled;
    float pullSpeed = 0.5F;
    public int pullDuration;
    Rigidbody2D rb;

    [HeaderAttribute("Death attributes")]
    public GameObject Blood;

    bool playOnce = false;

    Vector2 playerVision;


    Enemy_AttackScript enemyAttack;
    Enemy_Movement enemyMov;

    [HeaderAttribute("Retreat jump")]
    public float retreatJumpX = 20;
    public float retreatJumpY = 30;
    public bool retreatJump = false;
    public float retreatTime = 12;
    float retreatCounter;

    int freezeTime;
    int freezeCounter;
    bool freezeStart;

    public bool knockdown;
    bool hasKnockdown;
    public int knockdownForce;
    public int knockdownTime;
    int knockdownCounter;
    int knockdownDirection;
    public GameObject deathObject;

    Vector3 oldVel;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMov = GetComponent<Enemy_Movement>();
        enemyAttack = GetComponent<Enemy_AttackScript>();
        retreatCounter = retreatTime;
        poise = poiseHealth;

    }

    void FixedUpdate()
    {
        if (!HitStopScript.hitStop)
        {
            if (isBoss)
            {
                if (health <= triggerHealth) mode = 2;
            }
            //poise = poiseHealth;
            if (health <= 0 && !playOnce)
            {
                if (!isStoryMode)
                {
                    StartCoroutine("Death");
                }
                else if (enemyMov.ground) Knockout();
            }
            if (!EnclosedBattle.inBattle && knockout) {
                gameObject.layer = LayerMask.NameToLayer("Enemy");
            }

            if (requiresTrigger)
            {
            }
            if (!knockout)
            {
                if (hitstun > 0)
                {
                    if (enemyAttack.active || enemyAttack.startup || enemyAttack.recovery) enemyAttack.InterruptAttack();
                    if (canDizzy && canKnockback)
                    {
                        if (enemyMov.ground) hitstun -= 1;
                        else hitstun -= hitstunReductionAir;
                    }
                    else if (!canDizzy)
                        if (enemyMov.ground) hitstun -= 1;
                        else hitstun -= hitstunReductionAir;

                    stun = true;
                }

                if (canDizzy && knockdown)
                {
                    if (knockdownCounter == 0)
                    {
                        enemyMov.direction = Mathf.Sign(-knockdownDirection);
                        Instantiate(knockdownSound, transform.position, Quaternion.identity);
                        rb.velocity = new Vector2(knockdownDirection * knockdownForce, 0); print(knockdownDirection);
                        print(rb.velocity);
                    }
                    knockdownCounter++;
                    canKnockback = false;
                    if (knockdownCounter > knockdownTime && !canKnockback)
                    {
                        rb.velocity = Vector2.zero;
                        knockdown = false;
                        dizzy = true;
                        knockdownCounter = 0;
                        canKnockback = true;
                        hitstun = 200;
                    }
                }
            }

            if (freezeStart && stun)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; ;
                freezeCounter++;
                if (freezeCounter > freezeTime) { rb.velocity = oldVel; freezeStart = false; rb.constraints = RigidbodyConstraints2D.FreezeRotation; }
            }

           if (hitstun <= 0 && !retreatJump && stun)
            {
                freezeStart = false; rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                Retreat(); poise = poiseHealth; willGroundBounce = false;
            }
            if (retreatJump) retreatCounter -= 1;
            if (retreatJump && retreatCounter <= 0) { comboDamage = 0;  comboHits = 0; retreatCounter = retreatTime; comboCount = 0; hitstun = 0; stun = false; retreatJump = false; dizzy = false; enemyAttack.canAttack = true; enemyMov.mov = true; }

            //Rays for ground detection, player detection and fly height
            if (enemyMov.ground && stun && willGroundBounce || Input.GetKeyDown("o"))
            {
                print("Ground Bounce");
                willGroundBounce = false;
                Knockback(gBSource, gBKnockback, gBKnockup);
                Hitstun(gBHitstun, 100);
            }


            Pulled();
        }
    }

    public void TakeDamage(int dmg)
    {
        if (knockout) StartCoroutine("Death");
        health = health - dmg;
        comboDamage += dmg;
        moveDamage = dmg;
    }

    public void Hitstun(float dur, int poiseDamage)
    {
        if (!retreatJump && canKnockback && !knockdown)
        {
            if (!hyperArmor && !poiseArmor)
            {
                if (canDizzy && !stun) knockdown = true;
                enemyAttack.InterruptAttack();
                stun = true;
                if (comboCount < 8)
                    hitstun = (dur * Mathf.Pow(hitstunScaling, comboCount));
                else hitstun = dur * minimumScaling;
                comboCount += 1;
                comboHits++;
            }
            else if (poiseArmor && canKnockback)
            {
                poise -= poiseDamage;
                if (poise <= 0)
                {
                    if (canDizzy && !stun) knockdown = true;
                    enemyAttack.InterruptAttack();
                    stun = true;
                    if (comboCount < 8)
                        hitstun = (dur * Mathf.Pow(hitstunScaling, comboCount));
                    else hitstun = dur * minimumScaling;
                    comboCount += 1;
                    comboHits++;
                }
            }
            else if (hyperArmor && canKnockback)
            {
                if (enemyAttack.startup && hyperArmor || enemyAttack.active && hyperArmor) poise -= poiseDamage;
                if (poise <= 0 || !enemyAttack.startup && !enemyAttack.active)
                {
                    if (canDizzy && !stun) knockdown = true;
                    enemyAttack.InterruptAttack();
                    stun = true;
                    if (comboCount < 8)
                        hitstun = (dur * Mathf.Pow(hitstunScaling, comboCount));
                    else hitstun = dur * minimumScaling;
                    comboCount += 1;
                    comboHits++;
                }
            }
        }
    }

    public void Freeze(int time)
    {
        freezeCounter = 0;
        freezeTime = time;
        freezeStart = true;
        oldVel = rb.velocity;
    }

    public void Knockback(Vector2 source, float force, float knockup)
    {
        if (canDizzy) knockdownDirection = (int)-Mathf.Sign(source.x - transform.position.x);
        if (!retreatJump && canKnockback && !knockdown)
        {

            if (dizzy) dizzy = false;
            if (!poiseArmor && !hyperArmor || !enemyAttack.startup && !enemyAttack.active && hyperArmor || stun)
            {
                rb.velocity = -(new Vector2(source.x - transform.position.x, 0).normalized * force - Vector2.up * knockup);
            }
        }
    }

    public void GroundBounce(Vector2 source, float force, float knockup, float hitstun)
    {
        print("poipoi");
        stun = true;
        willGroundBounce = true;
        gBSource = source;
        gBKnockback = force;
        gBKnockup = knockup;
        gBHitstun = hitstun;

    }

    public void Pull(Vector2 source, float speed)
    {
        pulled = true;
        pullSpeed = speed;
        pullTarget = source;
    }

    void Pulled()
    {
        if (pulled)
        {
            transform.position = Vector2.Lerp(transform.position, pullTarget, pullSpeed);
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), pullTarget) < 5) pullDuration++;
            if (pullDuration >= 10) { pulled = false; pullDuration = 0; }
        }
    }

    public void TriggerActivate()
    {
        detected = true;
    }

    public void Retreat() { retreatJump = true; rb.velocity = new Vector2(Mathf.Sign(transform.position.x - enemyMov.target.transform.position.x) * retreatJumpX, retreatJumpY);
        enemyMov.direction = -Mathf.Sign(transform.position.x - enemyMov.target.transform.position.x); }

    public void Knockout()
    {
      //  rb.isKinematic = true;
        gameObject.layer = LayerMask.NameToLayer("iFrames");
        retreatJump = false;
        stun = false;
        knockout = true;
        enemyAttack.startup = false;
        enemyAttack.active = false;
        enemyAttack.canAttack = false;
        enemyMov.mov = false;
        playOnce = true;
        rb.velocity = Vector2.zero;
   //     enemyMov.AddVelocity(Vector2.zero);
    }

    IEnumerator Death()
    {
        if (!isBoss) GameDataManager.killedEnemies++;
        if (deathObject != null) Instantiate(deathObject, transform.position, Quaternion.identity);
        enemyAttack.startup = false;
        enemyAttack.active = false;
        enemyAttack.canAttack = false;
        enemyMov.mov = false;
        playOnce = true;
        enemyMov.AddVelocity(Vector2.zero);
        hitstun = 60;
        Time.timeScale = 0.5f;
        //Time.fixedDeltaTime = 0.02F * Time.timeScale;
        //Time.
        print("Timescale set");
        Blood.SetActive(true);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().shouldShake = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>().target = transform;
        yield return new WaitForSeconds(0.3f);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().shouldShake = false;
        Time.timeScale = 1;
        //Time.fixedDeltaTime = 0.02F;
        Destroy(gameObject);
    }
}
