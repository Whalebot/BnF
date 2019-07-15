using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AttackScript : MonoBehaviour
{

    //   public Movelist movelist;
    [HeaderAttribute("Moveset attributes")]
    public Weapon_MovesetScript moveset;
    public int bossID;
    //  Weapon_MovesetScript moveset;
    EnemyScript enemyScript;
    Enemy_Movement enemyMov;
    public GameObject target;
    public bool targetIsAirborne;

    [HeaderAttribute("Attack attributes")]
    public GameObject currentAttack;
    public int attackID;

    public float comboDelay;
    public int combo;
    public int comboCounter;
    public int resetCounter;
    public bool comboStart;
    public bool canAttack = true;
    public bool canMove;

    public bool hasIFrames;
    Vector3 targetAim;
    float lastAttacked;

    //   public float attackStart;

    public float actualDistance;
    public float trueDirection;

    public bool massive;
    public bool tornado;
    bool tornadoChange;
    bool isLeft;
    public bool homing;
    public bool tracking;
    public float directionChangeDelay;
    int crossUpDirection;
    int ray;
    [HeaderAttribute("Frame attributes")]


    int directionChange;
    int RNGCount;

    [HeaderAttribute("Frame attributes")]
    public bool startup;
    public bool active;
    public bool recovery;
    public bool startupMov;
    public bool activeMov;
    public bool recoveryMov;
    public int startupFrames;
    public int activeFrames;
    public int recoveryFrames;
    public bool setStartVelocity;
    public bool setEndVelocity;
    Vector3 startVelocity;

    public int forward;
    public int forward2;
    public int forward3;
    public int up;
    public int up2;
    public int up3;
    public int momentumDuration1;
    public int momentumDuration2;
    public int momentumDuration3;

    public bool setYVel;
    public bool willJump;
    public bool hasInvul;

    public bool jumpCancelable;
    public bool specialCancelable;
    public bool attackCancelable;
    public bool keepVel;
    public bool keepVerticalVel;
    public bool keepHorizontalVel;
    public bool landCancel;
    public bool landCancelRecovery;
    public bool interpolate;
    public int interpol1;
    public int interpol2;
    public int interpol3;

    void Awake()
    {
        enemyMov = GetComponent<Enemy_Movement>();
        target = GameObject.FindGameObjectWithTag("Player");
        enemyScript = GetComponent<EnemyScript>();
        lastAttacked = Random.Range(0, 1.5F);
        trueDirection = -Mathf.Sign(transform.position.x - target.transform.position.x);
    }

    void Start()
    {


    }

    void Update()
    {
        if (!PauseMenu.gameIsPaused && enemyScript.mode != 0)
        {
            if (target == null) { target = GameObject.FindGameObjectWithTag("Player"); }

            if (target != null)
            {
                actualDistance = (transform.position - target.transform.position).magnitude;
                trueDirection = -Mathf.Sign(transform.position.x - target.transform.position.x);
            }

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PauseMenu.gameIsPaused && enemyScript.mode != 0 && !HitStopScript.hitStop)
        {
            if (target != null)
                if (target.transform.position.y > (transform.position.y + 2)) targetIsAirborne = true;
                else targetIsAirborne = false;
            if (tornado && activeMov)
            {
                if (transform.position.x > target.transform.position.x && isLeft && !tornadoChange) { tornadoChange = true; isLeft = false; }
                if (transform.position.x < target.transform.position.x && !isLeft && !tornadoChange) { tornadoChange = true; isLeft = true; }
                if (tornadoChange) directionChange++;
                if (directionChangeDelay <= directionChange) { enemyMov.direction = trueDirection; directionChange = 0; tornadoChange = false; }

            }
            comboCounter++;
            //  if (comboDelay < comboCounter) { combo = 0; comboCounter = 0; }
            Startup();
            Active();
            Recovery();
        }
    }


    void Startup()
    {
        if (startup)
        {
            if (tracking) { enemyMov.direction = trueDirection; tracking = false; }
            if (!canMove)
            {
                startupFrames -= 1;
                if (startupMov && !keepVel)
                {
                    if (interpolate)
                        Momentum(new Vector2(transform.localScale.x * forward * ((momentumDuration1) / interpol1), up * ((momentumDuration1) / interpol1)));

                    else
                        Momentum(new Vector2(transform.localScale.x * forward, up));
                    momentumDuration1 -= 1;
                }
                else if (startupMov && keepVel) Momentum(new Vector2(enemyMov.direction * enemyMov.velocity * Time.deltaTime, enemyMov.rb.velocity.y));
            }


        }
        if (startupFrames <= 0 && startup)
        {
            startup = false;
            active = true;
            canAttack = false;
            currentAttack.SetActive(true);
            startupFrames = 1;
            activeMov = true;
            if (transform.position.x > target.transform.position.x) isLeft = true;
            else isLeft = false;
        }
        if (momentumDuration1 <= 0 && startupMov && !keepVel && !canMove)
        {
            targetAim = (target.transform.position - transform.position).normalized;
            Momentum(new Vector2(0, enemyMov.rb.velocity.y));
            startupMov = false;
        }
        else if (momentumDuration1 <= 0 && startupMov && keepVel)
        {
            targetAim = (target.transform.position - transform.position).normalized;
            startupMov = false;
        }
    }

    void Active()
    {
        if (active)
        {
            if (hasInvul) gameObject.layer = LayerMask.NameToLayer("Enemy");
            else if (hasIFrames) gameObject.layer = LayerMask.NameToLayer("iFrames");
            //       else if (hasInvul) gameObject.layer = LayerMask.NameToLayer("Invul");
            else gameObject.layer = LayerMask.NameToLayer("EnemyColission");
            activeFrames -= 1;

            if (!canMove)
            {
                if (activeMov)
                {

                    momentumDuration2 -= 1;

                    if (homing) Momentum(targetAim * forward2);
                    else if (interpolate) Momentum(new Vector2(transform.localScale.x * forward2 * (1 - (momentumDuration2) / interpol2), up2 * (1 - (momentumDuration2) / interpol2)));
                    else Momentum(new Vector2(transform.localScale.x * forward2, up2));

                }
                //else if (activeMov && keepVel) Momentum(new Vector2(enemyMov.direction * enemyMov.velocity * Time.deltaTime, enemyMov.rb.velocity.y));
                else if (activeMov && keepVel) Momentum(new Vector2(enemyMov.direction * enemyMov.velocity * Time.deltaTime, enemyMov.rb.velocity.y));
            }
        }

        if (activeFrames <= 0 && active)
        {
            active = false;
            recovery = true;
            recoveryMov = true;
            DisableObjects();
            hasIFrames = false;
        }
        if (momentumDuration2 <= 0 && activeMov && !keepHorizontalVel)
        {
            Momentum(new Vector2(0, enemyMov.rb.velocity.y));
            activeMov = false;
        }
        else if (momentumDuration2 <= 0 && activeMov && keepVel || momentumDuration2 <= 0 && activeMov && keepHorizontalVel)
        {
            activeMov = false;
        }
    }

    void Recovery()
    {
        if (recovery)
        {
            if (willJump) { enemyMov.Jump(); print("Jump attempted"); willJump = false; }
            tornado = false;
            recoveryFrames -= 1;
            gameObject.layer = LayerMask.NameToLayer("Enemy");
            /*   if (recoveryMov)
                {
                    momentumDuration3 -= 1;
                    if (!setYVel) Momentum(new Vector2(transform.localScale.x * forward3, up3));
                    else SetVelocity(new Vector2(transform.localScale.x * forward3, up3));
                }
  */
            if (!canMove)
            {
                if (recoveryMov && !keepVel)
                {
                    momentumDuration3 -= 1;
                    if (!keepVerticalVel && !keepHorizontalVel)
                    {

                        Momentum(new Vector2(transform.localScale.x * forward3, up3));
                    }

                    else if (keepVerticalVel && !keepHorizontalVel) Momentum(new Vector2(enemyMov.rb.velocity.x, enemyMov.rb.velocity.y));
                    else if (!keepVerticalVel && keepHorizontalVel) { Momentum(new Vector2(enemyMov.rb.velocity.x, up3)); }
                    else if (keepVerticalVel && keepHorizontalVel)
                    {
                        Momentum(new Vector2(enemyMov.rb.velocity.x, enemyMov.rb.velocity.y));
                    }

                }
                else if (recoveryMov && keepVel) Momentum(new Vector2(enemyMov.direction * enemyMov.velocity * Time.deltaTime, enemyMov.rb.velocity.y));
            }

        }
        if (momentumDuration3 <= 0 && recoveryMov && !canMove && !keepHorizontalVel)
        {
            Momentum(new Vector2(0, enemyMov.rb.velocity.y));
            recoveryMov = false;
        }
        else if (momentumDuration3 <= 0 && recoveryMov && !canMove && keepHorizontalVel)
        {
            Momentum(new Vector2(enemyMov.rb.velocity.x, enemyMov.rb.velocity.y));
            recoveryMov = false;
        }

        if (recoveryFrames <= 0 && recovery)
        {
            //print("potato");
            specialCancelable = false;

            canAttack = true;

            recovery = false;
            keepVel = false;
            keepVerticalVel = false;
            landCancelRecovery = false;
            attackCancelable = false;
            enemyMov.mov = true;
            DisableObjects();
        }

    }

    public void Momentum(Vector2 vel)
    {
        enemyMov.mov = false;
        enemyMov.AddVelocity(vel);

    }

    public void SetVelocity(Vector2 vel)
    {
        enemyMov.mov = false;
        enemyMov.SetVelocity(vel);
    }

    public void DisableObjects()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Enemy"))
                foreach (Transform child2 in child) { child2.gameObject.SetActive(false); }
        }

    }

    public void InterruptAttack()
    {

        DisableObjects();

        momentumDuration1 = 0;
        momentumDuration2 = 0;
        momentumDuration3 = 0;
        startupMov = false;
        activeMov = false;
        recoveryMov = false;
        startup = false;
        active = false;
        recovery = false;
        if (!enemyScript.stun)
        {
            //       enemyMov.mov = true;
            //       canAttack = true;
        }
    }
}