using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lilac_BossAttack : MonoBehaviour {

    [HeaderAttribute("Moveset attributes")]
    public GameObject moveset1;
    public int bossID;
    Weapon_MovesetScript activeMoveset;
    Boss_Script Boss_Script;
    Boss_AttackScript attackScript;
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

    float attackStart;
    public float actualDistance;
    public float distance;
    public float distance2;

    public bool tornado;
    public float directionChangeDelay;
    public float ray;
    int crossUpDirection;

    int RNGCount;

    float trueDirection;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        activeMoveset = moveset1.GetComponent<Weapon_MovesetScript>();
        attackScript = GetComponent<Boss_AttackScript>();
        Boss_Script = GetComponent<Boss_Script>();
    }

    void Update()
    {
        if (!PauseMenu.gameIsPaused && Boss_Script.mode != 0)
        {
            actualDistance = (transform.position - target.transform.position).magnitude;

            if (bossID == 2) ParryBoss();

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
        }
    }

    void SpecialAttack()
    {
        attackID = activeMoveset.specialID;
        attackStart = Time.time;
        canAttack = false;
        attackScript.momentumDuration1 = activeMoveset.specialDuration1;
        attackScript.momentumDuration2 = activeMoveset.specialDuration2;
        attackScript.momentumDuration3 = activeMoveset.specialDuration3;
        attackScript.forward = activeMoveset.specialForward;
        attackScript.up = activeMoveset.specialUp;
        attackScript.forward2 = activeMoveset.specialForward2;
        attackScript.up2 = activeMoveset.specialUp2;
        currentAttack = activeMoveset.special;
        attackScript.startupFrames = activeMoveset.specialStartUp;
        attackScript.activeFrames = activeMoveset.specialActive;
        attackScript.recoveryFrames = activeMoveset.specialRecovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
    }

    void NormalSlash()
    {
        attackID = activeMoveset.attack1ID;
        canAttack = false;
        combo = 1;
        print("combostart");
        attackScript.momentumDuration1 = activeMoveset.attack1Duration1;
        attackScript.momentumDuration2 = activeMoveset.attack1Duration2;
        attackScript.momentumDuration3 = activeMoveset.attack1Duration3;
        attackStart = Time.time;
        attackScript.forward = activeMoveset.attack1Forward;
        attackScript.up = activeMoveset.attack1Up;
        attackScript.forward2 = activeMoveset.attack1Forward2;
        attackScript.up2 = activeMoveset.attack1Up2;
        attackScript.currentAttack = activeMoveset.attack1;
        attackScript.startupFrames = activeMoveset.attack1StartUp;
        attackScript.activeFrames = activeMoveset.attack1Active;
        attackScript.recoveryFrames = activeMoveset.attack1Recovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
    }
    void NormalSlash2()
    {
        attackID = activeMoveset.attack2ID;
        combo = 2;
        attackStart = Time.time;
        attackScript.momentumDuration1 = activeMoveset.attack2Duration1;
        attackScript.momentumDuration2 = activeMoveset.attack2Duration2;
        attackScript.momentumDuration3 = activeMoveset.attack2Duration3;
        attackScript.canAttack = false;
        attackScript.forward = activeMoveset.attack2Forward;
        attackScript.up = activeMoveset.attack2Up;
        attackScript.forward2 = activeMoveset.attack2Forward2;
        attackScript.up2 = activeMoveset.attack2Up2;
        attackScript.currentAttack = activeMoveset.attack2;
        attackScript.startupFrames = activeMoveset.attack2StartUp;
        attackScript.activeFrames = activeMoveset.attack2Active;
        attackScript.recoveryFrames = activeMoveset.attack2Recovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
    }
    void NormalSlash3()
    {
        attackScript.attackID = activeMoveset.attack3ID;
        combo = 0;
        attackScript.attackStart = Time.time;
        attackScript.momentumDuration1 = activeMoveset.attack3Duration1;
        attackScript.momentumDuration2 = activeMoveset.attack3Duration2;
        attackScript.momentumDuration3 = activeMoveset.attack3Duration3;
        attackScript.canAttack = false;
        attackScript.forward = activeMoveset.attack3Forward;
        attackScript.up = activeMoveset.attack3Up;
        attackScript.forward2 = activeMoveset.attack3Forward2;
        attackScript.up2 = activeMoveset.attack3Up2;
        attackScript.currentAttack = activeMoveset.attack3;
        attackScript.startupFrames = activeMoveset.attack3StartUp;
        attackScript.activeFrames = activeMoveset.attack3Active;
        attackScript.recoveryFrames = activeMoveset.attack3Recovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
    }
    void Ranged()
    {
        attackScript.attackID = activeMoveset.downID;
        combo = 0;
        attackScript.attackStart = Time.time;
        attackScript.momentumDuration1 = activeMoveset.downAttackDuration1;
        attackScript.momentumDuration2 = activeMoveset.downAttackDuration2;
        attackScript.momentumDuration3 = activeMoveset.downAttackDuration3;
        attackScript.canAttack = false;
        attackScript.forward = activeMoveset.downAttackForward;
        attackScript.up = activeMoveset.downAttackUp;
        attackScript.forward2 = activeMoveset.downAttackForward2;
        attackScript.up2 = activeMoveset.downAttackUp2;
        attackScript.currentAttack = activeMoveset.downAttack;
        attackScript.startupFrames = activeMoveset.downAttackStartUp;
        attackScript.activeFrames = activeMoveset.downAttackActive;
        attackScript.recoveryFrames = activeMoveset.downAttackRecovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
    }

    void CrossUp()
    {
        attackScript.attackID = activeMoveset.air2ID;
        attackScript.attackStart = Time.time;
        attackScript.canAttack = false;
        attackScript.momentumDuration1 = activeMoveset.air2Duration1;
        attackScript.momentumDuration2 = activeMoveset.air2Duration2;
        attackScript.momentumDuration3 = activeMoveset.air2Duration3;
        attackScript.forward = activeMoveset.air2Forward;
        attackScript.up = activeMoveset.air2Up;
        attackScript.forward2 = activeMoveset.air2Forward2 * crossUpDirection;
        attackScript.up2 = activeMoveset.air2Up2;
        attackScript.currentAttack = activeMoveset.air2;
        attackScript.startupFrames = activeMoveset.air2StartUp;
        attackScript.activeFrames = activeMoveset.air2Active;
        attackScript.recoveryFrames = activeMoveset.air2Recovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
    }


    public void Momentum(Vector2 vel)
    {
        Boss_Script.mov = false;
        Boss_Script.AddVelocity(vel);
    }

    void ParryBoss()
    {
        if (Boss_Script.mode == 1)
        {
            if (Physics2D.Raycast(transform.position, -Vector2.right, ray, LayerMask.GetMask("Platform")) && (transform.position - target.transform.position).magnitude < distance2 && canAttack && attackStart + attackSpeed < Time.time && !Boss_Script.stun) { attackSpeed = Random.Range(minDelay, maxDelay); Boss_Script.Jump(); Boss_Script.direction = -trueDirection; crossUpDirection = -1; CrossUp(); }
            else if (Physics2D.Raycast(transform.position, Vector2.right, ray, LayerMask.GetMask("Platform")) && (transform.position - target.transform.position).magnitude < distance2 && canAttack && attackStart + attackSpeed < Time.time && !Boss_Script.stun) { attackSpeed = Random.Range(minDelay, maxDelay); Boss_Script.Jump(); Boss_Script.direction = -trueDirection; crossUpDirection = -1; CrossUp(); }
            if ((transform.position - target.transform.position).magnitude < distance2 && canAttack && attackStart + attackSpeed < Time.time && !Boss_Script.stun)
            {
                Boss_Script.direction = trueDirection;
                RNGCount = Random.Range(0, 12);
                if (RNGCount < 3 && Boss_Script.ground) { attackSpeed = Random.Range(minDelay, maxDelay); SpecialAttack(); }
                if (RNGCount >= 3 && RNGCount < 7)
                {
                    if (combo == 0) { attackSpeed = 0; NormalSlash(); comboCounter = 0; }
                    else if (combo == 1) { NormalSlash2(); comboCounter = 0; }
                    else if (combo == 2)
                    {
                        NormalSlash3();
                        attackSpeed = Random.Range(minDelay, maxDelay);
                    }
                }
                if (RNGCount == 7) { attackSpeed = Random.Range(minDelay, maxDelay); Boss_Script.Jump(); Ranged(); }
                if (RNGCount == 8) { attackSpeed = Random.Range(minDelay, maxDelay); Boss_Script.Jump(); Boss_Script.direction = -trueDirection; crossUpDirection = -1; CrossUp(); }
            }
            else if (!Boss_Script.stun && (transform.position - target.transform.position).magnitude > distance2 && (transform.position - target.transform.position).magnitude < distance && canAttack && attackStart + attackSpeed < Time.time)
            {
                Ranged();
                Boss_Script.direction = trueDirection;
                attackSpeed = Random.Range(minDelay, maxDelay);
            }
        }
    }

}
