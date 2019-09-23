using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    [HeaderAttribute("Movement attributes")]
    public bool boss;
    public int velocity;
    public float airMultiplier = 1F;
    public float maxSpeed;
    public float directionChangeDur = 2;
    public int movementID = 0;
    public int mode = 0;
    public float jumpVel = 50;


    public bool mov = true;
    public float ray;
    public float rayGround;
    public bool ground;

    public bool inContact;
    public float push = 10;

    bool inRange;
    public float actualDistance;
    public float stoppingDistance;
    public float distance;
    public float distance2;

    [HeaderAttribute("Flying attributes")]
    public bool flying;
    public float flySpeed;
    public float flyHeightRay;
    public float flyDistance;
    float flyingRay;
    RaycastHit2D flyRay;
    public bool hover;
    public float hoverSpeed = 0.5F;
    float hoverProgress;
    bool hoverUp;
    public float hoverVariation = 10;
    bool flown;
    public float direction;
    float lastDirectionChange = 0;
    public GameObject target;
    int RNGCounter;

    bool hitStopped;
    EnemyScript enemyScript;
    Enemy_AttackScript attackScript;
    [HideInInspector] public Rigidbody2D rb;
    RaycastHit2D hit;
    RaycastHit2D ihit;
    Vector3 oldVel;

    [HeaderAttribute("Generic dash attributes")]
    public GameObject dashObject;
    [HideInInspector] public bool usingMovesetDash;
    [HideInInspector] public float dashSpeed;
    [HideInInspector] public float dashDuration;
    [HideInInspector] public float dashRecovery;
    [HideInInspector] public bool dashing = false;
    [HideInInspector] public float currentDuration;
  [HideInInspector]  public float currentRecovery;
    Player_Movement playerMov;

    // Use this for initialization
    void Start()
    {
        enemyScript = GetComponent<EnemyScript>();
        attackScript = GetComponent<Enemy_AttackScript>();
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        direction = attackScript.trueDirection;
        playerMov = target.GetComponent<Player_Movement>();
    }

    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");


        
        }
        if (mov && attackScript.canAttack)
        {
            MeleeMovement();
        }
    }




    void FixedUpdate()
    {

        if (target != null)
        {
            hit = Physics2D.Raycast(transform.position, target.transform.position - Vector3.up * 2 - transform.position, 100, enemyScript.modeLayers);
            Debug.DrawLine(transform.position, (target.transform.position - Vector3.up * 2 - transform.position).normalized * 100, Color.green);
        }


        Detection();
        HitstopProperties();



        PushAway();
        Movement();
    }

    void HitstopProperties()
    {
        if (HitStopScript.hitStop)
        {
            if (!hitStopped)
            {
                oldVel = rb.velocity;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                hitStopped = true;

            }

        }
        else if (!HitStopScript.hitStop)
        {
            if (hitStopped)
            {
                hitStopped = false;
                rb.velocity = oldVel;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

    }

    void Detection()
    {
        actualDistance = Mathf.Abs(target.transform.position.x - transform.position.x);
        if (actualDistance < stoppingDistance)
        {
            inRange = true;
        }
        else inRange = false;


        if (enemyScript.requiresTrigger)
        {
            if (enemyScript.detected)
            {
                if (hit && hit.collider.CompareTag("Player") && hit.collider != null && hit.collider.gameObject != null)
                    mode = 2;
                else mode = 0;
            }
            else mode = 0;
        }
        else
        {
            if (hit && hit.collider.CompareTag("Player") && hit.collider != null && hit.collider.gameObject != null)
                mode = 2;
            else mode = 0;
        }
    }

    void PushAway()
    {
        if (!playerMov.ground && inContact && playerMov.rb.velocity.y <= 0)
        {
            print("push");
            mov = false;
           transform.Translate(new Vector2(Mathf.Sign((transform.position.x - target.transform.position.x)) * push, 0));
        }
    }

    void Movement()
    {

        //Flip sprite
        if (direction < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else if (direction > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        ground = Physics2D.Raycast(transform.position, -Vector2.up, rayGround, LayerMask.GetMask("Platform"));

        if (!enemyScript.stun && mov)
        {
            if (!inRange)
            {
                if (ground)
                {
                    rb.velocity = new Vector2(direction * velocity, rb.velocity.y);

                }
                else
                {
                    rb.AddForce(new Vector2(direction * velocity * airMultiplier, 0), ForceMode2D.Force);
                    rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, Physics2D.gravity.y, 50));
                }
            }


            //Flying 
            Flying();

        }
        //Gravity
        if (!flying || flying && enemyScript.stun)
        {
            if (target != null)
            {
                if (rb.velocity.y < 0) { rb.velocity += Vector2.up * Physics2D.gravity.y * 2 * Time.deltaTime; }
                else if (rb.velocity.y > 0 && target.transform.position.y < transform.position.y) rb.velocity += Vector2.up * Physics2D.gravity.y * 2 * Time.deltaTime;
            }

        }
    }

    void Flying()
    {
        if (flying)
        {
            flyRay = Physics2D.Raycast(this.transform.position, -Vector2.up, 500, LayerMask.GetMask("Platform"));
            flyingRay = flyHeightRay;


            if (flyRay.distance > flyingRay - 3 && flyRay.distance < flyingRay + 1 && !hover)
            {
                { hover = true; hoverProgress = rb.velocity.y; print("hotpotato"); }
            }
            else if (flyRay.distance > flyingRay + 3 || flyRay.distance < flyingRay - 5) hover = false;

            if ((target.transform.position - transform.position).magnitude > flyDistance || flyRay.distance > flyingRay + 3) rb.velocity = (target.transform.position - transform.position).normalized * Mathf.Abs(velocity) + Vector3.up;
            else if (hover)
            {
                rb.velocity = new Vector2(rb.velocity.x, hoverProgress);

                if (hoverUp) hoverProgress = hoverProgress + hoverSpeed;
                else hoverProgress = hoverProgress - hoverSpeed;
                if (hoverProgress >= hoverVariation) { hoverUp = false; }
                if (hoverProgress <= -hoverVariation + 2) { hoverUp = true; }
            }

            else if (flyRay.distance < flyingRay - 1) rb.velocity = new Vector2(rb.velocity.x, flySpeed);
        }
    }

    IEnumerator TurnSpeed()
    {
        float tempVel = velocity;
        velocity = 0;

        while (velocity < tempVel)
        {
            velocity++;
            yield return null;

        }
    }


    void SwanMovement()
    {
        if (mode == 0) direction = 0;
        if (mode != 0)
            if (Time.time > lastDirectionChange + directionChangeDur && mov && attackScript.canAttack)
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


    void MeleeMovement()
    {
        // if (mode == 0) direction = 0;
        //   if (mode == 1)

        /*                if (Physics2D.Raycast(transform.position, -Vector2.right, ray, LayerMask.GetMask("Platform")) || Physics2D.Raycast(transform.position, -Vector2.right, ray, LayerMask.GetMask("Enemy"))) direction = 1;
                else if (Physics2D.Raycast(transform.position, Vector2.right, ray, LayerMask.GetMask("Platform")) || Physics2D.Raycast(transform.position, Vector2.right, ray, LayerMask.GetMask("Enemy"))) direction = -1;*/
        {
            if (!inRange)
            {
                if ((target.transform.position.x - transform.position.x) < 0) { direction = -1; }
                else direction = 1;

            }
            else direction = 0;
        }
    }

    void RangedMovement()
    {
        if (mode == 0) direction = 0;
        if (mode == 1)
        {
            if (Physics2D.Raycast(transform.position, -Vector2.right, ray, LayerMask.GetMask("Platform")) || Physics2D.Raycast(transform.position, -Vector2.right, ray, LayerMask.GetMask("Enemy"))) direction = 1;
            else if (Physics2D.Raycast(transform.position, Vector2.right, ray, LayerMask.GetMask("Platform")) || Physics2D.Raycast(transform.position, Vector2.right, ray, LayerMask.GetMask("Enemy"))) direction = -1;
        }
        if (mode == 2)
        {
            if (Time.time > lastDirectionChange + directionChangeDur && mov)
            {
                lastDirectionChange = Time.time;
                if ((target.transform.position.x - transform.position.x) > 0 && direction == -1) { direction = -1; }
                else if ((target.transform.position.x - transform.position.x) < 0 && direction == 1) { direction = +1; }
            }
            /* if (Time.time > lastDirectionChange + directionChangeDur && mov)
             {
                 lastDirectionChange = Time.time;
                 if ((target.transform.position - transform.position).magnitude <= distance && (target.transform.position - transform.position).magnitude >= distance + 5) { direction = 0; }
                 else if ((target.transform.position - transform.position).magnitude > distance) { direction = Mathf.Sign(target.transform.position.x - transform.position.x); }
                 else if ((target.transform.position - transform.position).magnitude < distance - 5) { direction = -Mathf.Sign(target.transform.position.x - transform.position.x); }
             }*/
        }
    }

    void FlyingMovement()
    {
        // if (mode == 0) direction = 0;
        if (mode == 1)
        {
            if (Physics2D.Raycast(transform.position, -Vector2.right, ray, LayerMask.GetMask("Platform")) || Physics2D.Raycast(transform.position, -Vector2.right, ray, LayerMask.GetMask("Enemy"))) direction = 1;
            else if (Physics2D.Raycast(transform.position, Vector2.right, ray, LayerMask.GetMask("Platform")) || Physics2D.Raycast(transform.position, Vector2.right, ray, LayerMask.GetMask("Enemy"))) direction = -1;
        }
        if (mode == 2)
        {

            if (Time.time > lastDirectionChange + directionChangeDur && mov)
            {
                lastDirectionChange = Time.time;
                if ((target.transform.position.x - transform.position.x) > 0) { direction = 1; StartCoroutine("TurnSpeed"); }
                else if ((target.transform.position.x - transform.position.x) < 0) { direction = -1; StartCoroutine("TurnSpeed"); }
            }
            //       if (Physics2D.Raycast(transform.position, Vector2.right * direction, ray, LayerMask.GetMask("Obstacle"))) direction = 0;
        }
    }
    void LilacMovement()
    {
        if (mode == 0) direction = 0;
        if (mode == 1)
        {
            if (Physics2D.Raycast(transform.position, -Vector2.right, ray, LayerMask.GetMask("Platform")) || Physics2D.Raycast(transform.position, -Vector2.right, ray, LayerMask.GetMask("Enemy"))) direction = 1;
            else if (Physics2D.Raycast(transform.position, Vector2.right, ray, LayerMask.GetMask("Platform")) || Physics2D.Raycast(transform.position, Vector2.right, ray, LayerMask.GetMask("Enemy"))) direction = -1;
        }
        if (mode == 2)
        {

            if (Time.time > lastDirectionChange + directionChangeDur && mov)
            {
                lastDirectionChange = Time.time;
                if (Vector2.Distance(target.transform.position, transform.position) > 0)
                    if ((target.transform.position.x - transform.position.x) > 0) { direction = 1; }
                    else if ((target.transform.position.x - transform.position.x) < 0) { direction = -1; }
            }
            //       if (Physics2D.Raycast(transform.position, Vector2.right * direction, ray, LayerMask.GetMask("Obstacle"))) direction = 0;
        }
    }

    public void Jump() { rb.velocity += new Vector2(0, jumpVel); }

    public void AddVelocity(Vector2 dir)
    {
        rb.velocity = dir;

    }
    public void SetVelocity(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x, rb.velocity.y);
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (!enemyScript.knockout)
            if (other.CompareTag("Feet"))
            {
                 inContact = true;
            }
            else if (other.CompareTag("Enemy"))
            {
                //if (other.GetComponent<Enemy_Movement>().inContact) inContact = true;
            }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!enemyScript.knockout)
            if (other.CompareTag("Feet"))
            {
                inContact = false;
                mov = true;
            }
            else if (other.CompareTag("Enemy"))
            {
                { inContact = false; mov = true; }
            }
    }

    public void Dash()
    { }
}

