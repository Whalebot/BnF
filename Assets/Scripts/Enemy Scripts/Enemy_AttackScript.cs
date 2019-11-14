using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AttackScript : MonoBehaviour
{

    [HeaderAttribute("Moveset attributes")]
    [HideInInspector] public Weapon_MovesetScript moveset;
    EnemyScript enemyScript;
    Enemy_Movement enemyMov;
    [HideInInspector] public GameObject target;
    [HideInInspector] public bool targetIsAirborne;

    [HeaderAttribute("Attack attributes")]
    [HideInInspector] public GameObject currentAttack;
    [HideInInspector] public int attackID;


    public bool canAttack = true;
    public bool canMove;

    [HideInInspector] public bool hasIFrames;
    [HideInInspector] public bool noClip;
    Vector3 targetAim;


    [HideInInspector] public float actualDistance;
    [HideInInspector] public float trueDirection;

    [HideInInspector] public bool massive;
    [HideInInspector] public bool tornado;
    [HideInInspector] bool tornadoChange;
    [HideInInspector] bool isLeft;
    public bool homing;
    public bool tracking;
    public float directionChangeDelay;

    [HeaderAttribute("Frame attributes")]
    private int directionChange;
    private readonly int RNGCount;

    [HeaderAttribute("Frame attributes")]

    [HideInInspector] public bool startup;
    [HideInInspector] public bool active;
    [HideInInspector] public bool recovery;
    [HideInInspector] public bool startupMov;
    [HideInInspector] public bool activeMov;
    [HideInInspector] public bool recoveryMov;
    [HideInInspector] public int startupFrames;
    [HideInInspector] public int activeFrames;
    [HideInInspector] public int recoveryFrames;
    [HideInInspector] public bool setStartVelocity;
    [HideInInspector] public bool setEndVelocity;
    Vector3 startVelocity;

    [HideInInspector] public int forward;
    [HideInInspector] public int forward2;
    [HideInInspector] public int forward3;
    [HideInInspector] public int up;
    [HideInInspector] public int up2;
    [HideInInspector] public int up3;
    [HideInInspector] public int momentumDuration1;
    [HideInInspector] public int momentumDuration2;
    [HideInInspector] public int momentumDuration3;

    [HideInInspector] public bool setYVel;
    [HideInInspector] public bool willJump;
    [HideInInspector] public bool hasInvul;

    [HideInInspector] public bool jumpCancelable;
    [HideInInspector] public bool specialCancelable;
    [HideInInspector] public bool attackCancelable;
    [HideInInspector] public bool keepVerticalVel;
    [HideInInspector] public bool keepHorizontalVel;
    [HideInInspector] public bool landCancel;
    [HideInInspector] public bool landCancelRecovery;
    [HideInInspector] public bool interpolate;
    [HideInInspector] public int interpol1;
    [HideInInspector] public int interpol2;
    [HideInInspector] public int interpol3;

    void Awake()
    {
        enemyMov = GetComponent<Enemy_Movement>();
        target = GameObject.FindGameObjectWithTag("Player");
        enemyScript = GetComponent<EnemyScript>();
        trueDirection = -Mathf.Sign(transform.position.x - target.transform.position.x);
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
            startupFrames -= 1;
            if (startupMov && !canMove)
            {
                if (interpolate)
                    Momentum(new Vector2(transform.localScale.x * forward * ((momentumDuration1) / interpol1), up * ((momentumDuration1) / interpol1)));

                else
                    Momentum(new Vector2(transform.localScale.x * forward, up));
                momentumDuration1 -= 1;
            }
            else if (startupMov && canMove) { }//Momentum(new Vector2(enemyMov.direction * enemyMov.velocity, enemyMov.rb.velocity.y));
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
        if (momentumDuration1 <= 0 && startupMov && !canMove)
        {
            targetAim = (target.transform.position - transform.position).normalized;
            Momentum(new Vector2(0, enemyMov.rb.velocity.y));
            startupMov = false;
        }
        else if (momentumDuration1 <= 0 && startupMov && canMove)
        {
            targetAim = (target.transform.position - transform.position).normalized;
            startupMov = false;
        }
    }

    void Active()
    {
        if (active)
        {
            if (hasIFrames) gameObject.layer = LayerMask.NameToLayer("iFrames");
            else if (hasInvul) gameObject.layer = LayerMask.NameToLayer("Invul");
            else if (noClip) gameObject.layer = LayerMask.NameToLayer("NoClip");
            else gameObject.layer = LayerMask.NameToLayer("EnemyColission");

            activeFrames -= 1;


            if (activeMov && !canMove)
            {
                momentumDuration2 -= 1;
                if (homing) Momentum(targetAim * forward2);
                else if (interpolate) Momentum(new Vector2(transform.localScale.x * forward2 * (1 - (momentumDuration2) / interpol2), up2 * (1 - (momentumDuration2) / interpol2)));
                else Momentum(new Vector2(transform.localScale.x * forward2, up2));
            }
            else if (activeMov && canMove)
            { //Momentum(new Vector2(enemyMov.direction * enemyMov.velocity * Time.deltaTime, enemyMov.rb.velocity.y));
            }

            if (momentumDuration2 <= 0 && activeMov && !canMove)
            {
                if (!keepHorizontalVel && !keepVerticalVel)
                    Momentum(new Vector2(0, enemyMov.rb.velocity.y));
                activeMov = false;
            }

            if (momentumDuration2 <= 0 && !activeMov && !canMove && enemyMov.rb.velocity.x != 0)
            {
                if (!keepHorizontalVel && !keepVerticalVel)
                    Momentum(new Vector2(0, enemyMov.rb.velocity.y));
            }

            if (momentumDuration2 <= 0 && activeMov && canMove)
            {
                activeMov = false;
            }
        }

        if (activeFrames <= 0 && active)
        {
            active = false;
            recovery = true;
            if (noClip) gameObject.layer = LayerMask.NameToLayer("NoClip");
            else gameObject.layer = LayerMask.NameToLayer("Enemy");
            hasIFrames = false;
            DisableObjects();
            activeFrames = 1;
            recoveryMov = true;
        }
    }

    void Recovery()
    {
        if (recovery)

        {
            //enemyMov.rb.mass = 1;
            if (willJump) { enemyMov.Jump(); print("Jump attempted"); willJump = false; }
            tornado = false;
            recoveryFrames -= 1;
            gameObject.layer = LayerMask.NameToLayer("Enemy");

            //enemyMov.DashRecovery();
            if (recoveryMov)
            {

                momentumDuration3 -= 1;
                if (!keepVerticalVel && !keepHorizontalVel && !canMove)
                {

                    Momentum(new Vector2(transform.localScale.x * forward3, up3));
                }

                else if (keepVerticalVel && !keepHorizontalVel) Momentum(new Vector2(transform.localScale.x * forward3, enemyMov.rb.velocity.y));
                else if (!keepVerticalVel && keepHorizontalVel) { Momentum(new Vector2(enemyMov.rb.velocity.x, up3)); }
                else if (keepVerticalVel && keepHorizontalVel)
                {
                    Momentum(new Vector2(enemyMov.rb.velocity.x, enemyMov.rb.velocity.y));
                }
            }
        }
        if (momentumDuration3 <= 0 && recoveryMov && !canMove)
        {
            if (!keepHorizontalVel && !keepVerticalVel)
                Momentum(new Vector2(0, enemyMov.rb.velocity.y));
            recoveryMov = false;
        }
        else if (momentumDuration3 <= 0 && recoveryMov && canMove)
        {
            recoveryMov = false;
        }
        if (recoveryFrames <= 0 && recovery)
        {
            
            specialCancelable = false;
            gameObject.layer = LayerMask.NameToLayer("Enemy");
            canAttack = true;

            recovery = false;
            keepVerticalVel = false;
            landCancelRecovery = false;
            attackCancelable = false;
            enemyMov.dashing = false;
            enemyMov.mov = true;
            DisableObjects();

            enemyMov.direction = 0;
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