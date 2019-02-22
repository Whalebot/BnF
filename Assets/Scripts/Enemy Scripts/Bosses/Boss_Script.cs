using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Script : MonoBehaviour
{
    [HeaderAttribute("Generic attributes")]
    public int bossID;
    public int mode = 0;
    public int health;
    public int phase2Health;

    [HeaderAttribute("Movement attributes")]
    public int velocity;
    public bool canKnockBack = true;
    public LayerMask modeLayers;
    public bool detected = false;
    public bool requiresTrigger = false;

    public float directionChangeDur = 2;
    float currentVel;
    public float maxSpeed;
    public bool mov = true;
    public bool ground;
    public float direction = 1;
    public float ray;
    public float rayGround;

    int RNGCounter;
    float lastDirectionChange = 0;

    [HeaderAttribute("Stun attributes")]
    public int dmgToStun = 50;
    public float comboCount;
    public float hitstunScaling = 0.8F;
    public float hitstun;
    public float minimumScaling = 0.35F;
    public bool stun;
    public bool dizzy;
    public bool isDead;
    int hitCounter;
    public float jumpHeight = 100;
    public float retreatJumpX = 20;
    public float retreatJumpY = 30;
    bool retreatJump = false;
    public float retreatTime = 0.3f;
    float retreatCounter;

    [HeaderAttribute("Flying attributes")]
    public bool flying = false;
    public float flyHeight;
    public float flyUp;
    public float flapFrequency;
    float lastFlap;

    [HeaderAttribute("Death attributes")]
    public GameObject Blood;

    Vector2 playerVision;
    public Rigidbody2D rb;
    RaycastHit2D hit;
    RaycastHit2D ihit;
    GameObject target;
    Boss_AttackScript bossAttack;

    public GameObject deathSound;


    bool hitStopped;
    Vector3 oldVel;

    // Use this for initialization
    void Start()
    {
        bossAttack = GetComponent<Boss_AttackScript>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        target = GameObject.FindGameObjectWithTag("Player");
        retreatCounter = retreatTime;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, -Vector3.up * rayGround);
        if (HitStopScript.hitStop)
        {
            if (!hitStopped) { oldVel = rb.velocity; hitStopped = true; }
            rb.velocity = Vector3.zero;
        }
        if (!HitStopScript.hitStop)
        {
            if (hitStopped) { hitStopped = false; rb.velocity = oldVel; }
            if (!PauseMenu.gameIsPaused)
            {

                if (Input.GetKeyDown("t")) Jump();
                if (mode == 0) direction = 0;
                if (mode == 1)
                {
                    if (bossID == 1) SwanBoss();
                    if (bossID == 2) ParryBoss();
                    if (bossID == 3) Hakuhei();
                }
                if (mode == 2)
                {

                    if (bossID == 1) SwanBoss();
                    if (bossID == 2)
                    {
                        ParryBoss();

            
                    }
                }

                if (direction < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
                else if (direction > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);

                if (health <= 0 && isDead != true)
                {
                    StartCoroutine("Death");
                    isDead = true;
                }
                if (hitCounter >= dmgToStun && canKnockBack == false)
                {
                    canKnockBack = true; hitstun = 240; dizzy = true; bossAttack.InterruptAttack(); rb.velocity = Vector3.zero;
                }
                if (hitstun > 0) { hitstun -= 1; stun = true; }
                if (hitstun <= 0 && !retreatJump && stun) { Retreat(); }
                if (retreatJump) retreatCounter -= 1;
                if (retreatJump && retreatCounter <= 0)
                {
                    retreatCounter = retreatTime; stun = false; comboCount = 0; hitstun = 0; canKnockBack = false; hitCounter = 0; Jump(); dizzy = false;
                    retreatJump = false;
                }

                ground = Physics2D.Raycast(transform.position, -Vector2.up, rayGround, LayerMask.GetMask("Platform"));
                hit = Physics2D.Raycast(transform.position, target.transform.position - transform.position, 100, modeLayers);
                if (requiresTrigger == false)
                {
                    if (hit && hit.collider.gameObject.CompareTag("Player")) { detected = true; }
                }

                if (detected)
                {
                    if (health > phase2Health) mode = 1;
                    else if (health <= phase2Health)
                    {
                        mode = 2;
                        bossAttack.minDelay = 0;
                        bossAttack.maxDelay = 2;
                    }
                }
                else mode = 0;

                if (!stun && flying && lastFlap + flapFrequency < Time.time && Physics2D.Raycast(transform.position, -Vector2.up, flyHeight, LayerMask.GetMask("Platform")))
                {
                    lastFlap = Time.time; rb.velocity += Vector2.up * flyUp;
                }

                if (!stun && mov)
                {
                    rb.velocity = new Vector2(direction * Time.deltaTime * velocity, rb.velocity.y);
                    rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -1000, 1000));
                }


                if (rb.velocity.y < 0) { rb.velocity += Vector2.up * Physics2D.gravity.y * 2 * Time.deltaTime; }
                else if (rb.velocity.y > 0 && target.transform.position.y < transform.position.y) rb.velocity += Vector2.up * Physics2D.gravity.y * 2 * Time.deltaTime;
            }
        }
    }

    public void Jump() { rb.velocity = new Vector2(0, jumpHeight); }

    public void Retreat()
    {
        retreatJump = true; rb.velocity = new Vector2(Mathf.Sign(transform.position.x - target.transform.position.x) * retreatJumpX, retreatJumpY);
        //  retreatJump = true; rb.velocity = new Vector2(Mathf.Sign(transform.position.x - target.transform.position.x) * retreatJumpX, retreatJumpY);
    }

    public void TakeDamage(int dmg)
    {
        health = health - dmg;

    }

    public void Hitstun(float dur, int poiseDamage)
    {
        hitCounter += poiseDamage;

        if (dizzy && hitstun < 200 || stun && hitstun < 200)
        {
            stun = true;
            if (comboCount < 4)
                hitstun = (dur * Mathf.Pow(hitstunScaling, comboCount));
            else hitstun = dur * minimumScaling;
            comboCount += 1;
        }

    }

    public void Knockback(Vector2 source, float force, float knockup)
    {
        if (!bossAttack.startup && !bossAttack.active && canKnockBack && hitstun < 200)
        {
            rb.velocity = -(new Vector2(source.x - transform.position.x, 0).normalized * force - Vector2.up * knockup);
            if (dizzy) dizzy = false;
        }
    }

    public void AddVelocity(Vector2 direction)
    {
        rb.velocity = new Vector2(direction.x, rb.velocity.y);
    }

    public void SetVelocity(Vector2 direction)
    {
        rb.velocity = new Vector2(direction.x, direction.y);
        //  rb.velocity = new Vector2(direction.x, rb.velocity.y);
    }


    void SwanBoss()
    {
        if (Time.time > lastDirectionChange + directionChangeDur && mov && bossAttack.canAttack)
        {
            RNGCounter = Random.Range(1, 7);
            lastDirectionChange = Time.time;
            if (RNGCounter <= 3)
            {
                if ((target.transform.position.x - transform.position.x) < 0) { direction = -1; }
                else direction = 1;
            }
            if (RNGCounter == 4 || RNGCounter == 5)
            {
                if ((target.transform.position.x - transform.position.x) < 0) { direction = 1; }
                else direction = -1;
            }
            if (RNGCounter == 6) direction = 0;
        }
    }

    void ParryBoss()
    {
        if (mode == 1)
        {
            if (Time.time > lastDirectionChange + directionChangeDur && mov && bossAttack.canAttack)
            {
                RNGCounter = Random.Range(1, 10);
                lastDirectionChange = Time.time;
                if (RNGCounter <= 4)
                {
                    if ((target.transform.position.x - transform.position.x) < 0) { direction = 1; }
                    else direction = -1;
                }
                if (RNGCounter == 8)
                {
                    if ((target.transform.position.x - transform.position.x) < 0) { direction = -1; }
                    else direction = 1;
                }
                if (RNGCounter == 9) direction = 0;
            }
        }
        if (mode == 2)
        {
            if (Time.time > lastDirectionChange + directionChangeDur && mov && bossAttack.canAttack)
            {
                RNGCounter = Random.Range(1, 10);
                lastDirectionChange = Time.time;
                if (RNGCounter <= 4)
                {
                    if ((target.transform.position.x - transform.position.x) < 0) { direction = -1; }
                    else direction = 1;
                }
                if (RNGCounter == 8)
                {
                    if ((target.transform.position.x - transform.position.x) < 0) { direction = 1; }
                    else direction = -1;
                }
                if (RNGCounter == 9) direction = 0;
            }
        }

    }


    void Hakuhei()
    {
        if (Time.time > lastDirectionChange + directionChangeDur && mov && bossAttack.canAttack)
        {
            RNGCounter = Random.Range(1, 7);
            lastDirectionChange = Time.time;
            if (RNGCounter <= 4)
            {
                if ((target.transform.position.x - transform.position.x) < 0) { direction = -1; }
                else direction = 1;
            }
            if (RNGCounter == 5)
            {
                if ((target.transform.position.x - transform.position.x) < 0) { direction = 1; }
                else direction = -1;
            }
            if (RNGCounter == 6) direction = 0;
        }
    }


    IEnumerator Death()
    {
        Instantiate(deathSound);
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        bossAttack.startup = false;
        bossAttack.active = false;
        bossAttack.recovery = false;
        bossAttack.canAttack = false;
        mov = false;
        AddVelocity(Vector2.zero);
        hitstun = 5;
        Blood.SetActive(true);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().shouldShake = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>().target = transform;
        yield return new WaitForSeconds(0.3f);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().shouldShake = false;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F;
        Destroy(gameObject);
    }
}
