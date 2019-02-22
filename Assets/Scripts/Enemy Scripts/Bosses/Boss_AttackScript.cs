using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AttackScript : MonoBehaviour
{
    [HeaderAttribute("Moveset attributes")]
    public GameObject moveset1;
    public int bossID;
    //  Weapon_MovesetScript activeMoveset;
    Boss_Script Boss_Script;
    GameObject target;

    [HeaderAttribute("Attack attributes")]
    public GameObject currentAttack;
    public int attackID;
    public float attackSpeed;
    public float maxDelay;
    public float minDelay;
    public float comboDelay;
    public int combo;
    public int comboCounter;
    public bool comboStart;
    public bool canAttack = true;
    bool hasIFrames;

    public float attackStart;
    public float actualDistance;
    public float distance;
    public float distance2;
    public float trueDirection;

    public bool massive;
    public bool tornado;
    public float directionChangeDelay;
    int crossUpDirection;
    int ray;
    [HeaderAttribute("Frame attributes")]


    float directionChange;
    int RNGCount;

    [HeaderAttribute("Frame attributes")]
    public bool startup;
    public bool active;
    public bool recovery;
    public bool startupMov;
    public bool activeMov;
    public bool recoveryMov;
    public float startupFrames;
    public float activeFrames;
    public float recoveryFrames;
    public bool setStartVelocity;
    public bool setEndVelocity;
    Vector3 startVelocity;

    public float forward;
    public float forward2;
    public float forward3;
    public float up;
    public float up2;
    public float up3;
    public float momentumDuration1;
    public float momentumDuration2;
    public float momentumDuration3;

    public bool setYVel;
    public bool willJump;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Boss_Script = GetComponent<Boss_Script>();
    }

    void Update()
    {
        if (!PauseMenu.gameIsPaused && Boss_Script.mode != 0)
        {
            actualDistance = (transform.position - target.transform.position).magnitude;
            trueDirection = -Mathf.Sign(transform.position.x - target.transform.position.x);

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PauseMenu.gameIsPaused && Boss_Script.mode != 0)
        {
            comboCounter++;
            if (comboDelay < comboCounter) combo = 0;
            Startup();
            Active();
            Recovery();
        }
    }


    void Startup()
    {
        if (startup)
        {
            active = false;
            startupFrames -= 1;
            if (startupMov)
            {
                if (!setYVel) Momentum(new Vector2(transform.localScale.x * forward, up));
                else SetVelocity(new Vector2(transform.localScale.x * forward, up));
                momentumDuration1 -= 1;
            }
        }
        if (startupFrames <= 0 && startup)
        {
            startup = false;
            active = true;
            canAttack = false;
            currentAttack.SetActive(true);
            activeMov = true;
        }
        if (momentumDuration1 <= 0 && startupMov)
        {
            Momentum(new Vector2(0, Boss_Script.rb.velocity.y));
            startupMov = false;
        }
    }

    void Active()
    {
        if (active)
        {
            if (massive) gameObject.layer = LayerMask.NameToLayer("Enemy");
            else gameObject.layer = LayerMask.NameToLayer("EnemyColission");
            activeFrames -= 1;
            if (activeMov)
            {
                momentumDuration2 -= 1;
                if (!setYVel) Momentum(new Vector2(transform.localScale.x * forward2, up2));
                else SetVelocity(new Vector2(transform.localScale.x * forward2, up2));
            }
        }
        if (activeFrames <= 0 && active)
        {
            active = false;
            recovery = true;
            recoveryMov = true;
            DisableObjects();
        }
        if (momentumDuration2 <= 0 && activeMov)
        {
            Momentum(new Vector2(0, Boss_Script.rb.velocity.y));
            activeMov = false;
        }
    }

    void Recovery()
    {
        if (recovery)
        {
            if (willJump) { Boss_Script.Jump(); print("Jump attempted"); willJump = false; }
            tornado = false;
            recoveryFrames -= 1;
            gameObject.layer = LayerMask.NameToLayer("Enemy");
            if (recoveryMov)
            {
                momentumDuration3 -= 1;
                if (!setYVel) Momentum(new Vector2(transform.localScale.x * forward3, up3));
                else SetVelocity(new Vector2(transform.localScale.x * forward3, up3));
            }
        }
        if (recoveryFrames <= 0 && recovery)
        {
            canAttack = true;
            Boss_Script.mov = true;
            recovery = false;

        }
        if (momentumDuration3 <= 0 && recoveryMov)
        {
            Momentum(new Vector2(0, Boss_Script.rb.velocity.y));
            recoveryMov = false;
        }
    }

    public void Momentum(Vector2 vel)
    {
        Boss_Script.mov = false;
        Boss_Script.AddVelocity(vel);

    }

    public void SetVelocity(Vector2 vel)
    {
        Boss_Script.mov = false;
        Boss_Script.SetVelocity(vel);
    }

    public void DisableObjects()
    {
        foreach (Transform child in transform)
        {
            foreach (Transform child2 in child) { child2.gameObject.SetActive(false); }
        }

    }

    public void InterruptAttack()
    {
        startup = false;
        startupFrames = 0;
        active = false;
        activeFrames = 0;
        recovery = true;
        recoveryFrames = 0;
        DisableObjects();
        Boss_Script.rb.velocity = Vector3.zero;
    }
}
