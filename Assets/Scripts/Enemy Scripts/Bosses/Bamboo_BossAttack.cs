using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bamboo_BossAttack : MonoBehaviour {

    public GameObject moveset1;
    Puppet_Moveset activeMoveset;
    Boss_Script Boss_Script;
    Boss_AttackScript attackScript;
    GameObject target;
    public float ray;
    public bool tornado;
    public float minDelay;
    public float maxDelay;
    public float directionChangeDelay;
    public float distance;
    public float distance2;
    int crossUpDirection;
    int RNGCount;
    float directionChange;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        activeMoveset = moveset1.GetComponent<Puppet_Moveset>();
        Boss_Script = GetComponent<Boss_Script>();
        attackScript = GetComponent<Boss_AttackScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      ParryBoss();
        directionChangeDelay = Random.Range(minDelay, maxDelay);
        if (tornado && Time.time > directionChange + directionChangeDelay) { attackScript.forward2 = transform.localScale.x * attackScript.trueDirection * activeMoveset.air3Forward2; }
        if (transform.position.x <= target.transform.position.x + 5 && transform.position.x >= target.transform.position.x - 5) directionChange = Time.time;

    }

    void ParryBoss()
    {
        if (Boss_Script.mode == 1)
        {
            if (Physics2D.Raycast(transform.position, -Vector2.right, ray, LayerMask.GetMask("Platform")) && (transform.position - target.transform.position).magnitude < distance2 && attackScript.canAttack && attackScript.attackStart + attackScript.attackSpeed < Time.time && !Boss_Script.stun)
            { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); Boss_Script.Jump(); Boss_Script.direction = -attackScript.trueDirection; crossUpDirection = -1; CrossUp(); }
            else if (Physics2D.Raycast(transform.position, Vector2.right, ray, LayerMask.GetMask("Platform")) && (transform.position - target.transform.position).magnitude < distance2 && attackScript.canAttack && attackScript.attackStart + attackScript.attackSpeed < Time.time && !Boss_Script.stun) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); Boss_Script.Jump(); Boss_Script.direction = -attackScript.trueDirection; crossUpDirection = -1; CrossUp(); }
            if ((transform.position - target.transform.position).magnitude < distance2 && attackScript.canAttack && attackScript.attackStart + attackScript.attackSpeed < Time.time && !Boss_Script.stun)
            {
                Boss_Script.direction = attackScript.trueDirection;
                RNGCount = Random.Range(0, 12);
                if (RNGCount < 3 && Boss_Script.ground) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); SpecialAttack(); }
                if (RNGCount >= 3 && RNGCount < 7)
                {
                    if (attackScript.combo == 0) { attackScript.attackSpeed = 0; NormalSlash(); attackScript.comboCounter = 1; }
                    else if (attackScript.combo == 1) { NormalSlash2(); attackScript.comboCounter =2; }
                    else if (attackScript.combo == 2)
                    {
                        NormalSlash3();
                        attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay);
                    }
                }
                if (RNGCount == 7) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); Boss_Script.Jump(); Ranged(); }
                if (RNGCount == 8) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); Boss_Script.Jump(); Boss_Script.direction = -attackScript.trueDirection; crossUpDirection = -1; CrossUp(); }
                if (RNGCount >= 9) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); Boss_Script.Jump(); AirRanged(); }
            }
            else if (!Boss_Script.stun && (transform.position - target.transform.position).magnitude > distance2 && (transform.position - target.transform.position).magnitude < distance && attackScript.canAttack && attackScript.attackStart + attackScript.attackSpeed < Time.time)
            {
                Ranged();
                Boss_Script.direction = attackScript.trueDirection;
                attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay);
            }
        }

        if (Boss_Script.mode == 2)
        {
            if ((transform.position - target.transform.position).magnitude < distance2 && attackScript.canAttack && attackScript.attackStart + attackScript.attackSpeed < Time.time && !Boss_Script.stun)
            {
                tornado = false;
                if (Physics2D.Raycast(transform.position, -Vector2.right, ray, LayerMask.GetMask("Platform")) && (transform.position - target.transform.position).magnitude < distance2 && attackScript.canAttack && attackScript.attackStart + attackScript.attackSpeed < Time.time && !Boss_Script.stun) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); Boss_Script.Jump(); Boss_Script.direction = -attackScript.trueDirection; crossUpDirection = -1; CrossUp(); }
                else if (Physics2D.Raycast(transform.position, Vector2.right, ray, LayerMask.GetMask("Platform")) && (transform.position - target.transform.position).magnitude < distance2 && attackScript.canAttack && attackScript.attackStart + attackScript.attackSpeed < Time.time && !Boss_Script.stun) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); Boss_Script.Jump(); Boss_Script.direction = -attackScript.trueDirection; crossUpDirection = -1; CrossUp(); }
                Boss_Script.direction = attackScript.trueDirection;
                RNGCount = Random.Range(1, 11);
                if (RNGCount < 3 && Boss_Script.ground) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); SpecialAttack(); }
                if (RNGCount >= 3 && RNGCount < 7)
                {
                    if (target.transform.position.y > transform.position.y && Boss_Script.ground) Boss_Script.Jump();
                    if (attackScript.combo == 0) { attackScript.attackSpeed = 0; NormalSlash(); attackScript.comboCounter = 0; }
                    else if (attackScript.combo == 1) { NormalSlash2(); attackScript.comboCounter = 0; }
                    else if (attackScript.combo == 2)
                    {
                        NormalSlash3();
                        attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay);
                    }
                }
                if (RNGCount == 7) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); Ranged(); }
                if (RNGCount == 8) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); Boss_Script.Jump(); Boss_Script.direction = -attackScript.trueDirection; crossUpDirection = -1; CrossUp(); }
                if (RNGCount == 9) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); AirRanged(); }
                if (RNGCount >= 10) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); TornadoAttack(); }
            }
            else if (!Boss_Script.stun && (transform.position - target.transform.position).magnitude > distance2 && (transform.position - target.transform.position).magnitude < distance && attackScript.canAttack && attackScript.attackStart + attackScript.attackSpeed < Time.time)
            {
                tornado = false;
                RNGCount = Random.Range(1, 3);
                Boss_Script.direction = attackScript.trueDirection;
                attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay);
                if (RNGCount == 1)
                    TornadoAttack();
                if (RNGCount == 2)
                    Ranged();

            }
        }
    }

    void NormalSlash()
    {
        attackScript.attackID = activeMoveset.attack1ID;
        attackScript.canAttack = false;
        attackScript.combo = 1;
        attackScript.momentumDuration1 = activeMoveset.attack1Duration1;
        attackScript.momentumDuration2 = activeMoveset.attack1Duration2;
        attackScript.momentumDuration3 = activeMoveset.attack1Duration3;
        attackScript.attackStart = Time.time;
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
        attackScript.massive = false;

    }
    void NormalSlash2()
    {
        attackScript.attackID = activeMoveset.attack2ID;
        attackScript.combo = 2;
        attackScript.attackStart = Time.time;
        attackScript.momentumDuration1 = activeMoveset.attack2Duration1;
        attackScript.momentumDuration2 = activeMoveset.attack2Duration2;
        attackScript.momentumDuration3 = activeMoveset.attack2Duration3;
        attackScript.attackStart = Time.time;
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
        attackScript.combo = 0;
        attackScript.attackStart = Time.time;
        attackScript.momentumDuration1 = activeMoveset.attack3Duration1;
        attackScript.momentumDuration2 = activeMoveset.attack3Duration2;
        attackScript.momentumDuration3 = activeMoveset.attack3Duration3;
        attackScript.attackStart = Time.time;
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
        attackScript.combo = 0;
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
        attackScript.massive = true;
    }

    void AirRanged()
    {
        attackScript.attackID = activeMoveset.upID;
        attackScript.combo = 0;
        attackScript.attackStart = Time.time;
        attackScript.momentumDuration1 = activeMoveset.upAttackDuration1;
        attackScript.momentumDuration2 = activeMoveset.upAttackDuration2;
        attackScript.momentumDuration3 = activeMoveset.upAttackDuration3;
        attackScript.canAttack = false;
        attackScript.forward = activeMoveset.upAttackForward;
        attackScript.up = activeMoveset.upAttackUp;
        attackScript.forward2 = activeMoveset.upAttackForward2;
        attackScript.up2 = activeMoveset.upAttackUp2;
        attackScript.currentAttack = activeMoveset.upAttack;
        attackScript.startupFrames = activeMoveset.upAttackStartUp;
        attackScript.activeFrames = activeMoveset.upAttackActive;
        attackScript.recoveryFrames = activeMoveset.upAttackRecovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
        attackScript.massive = true;
    }
    void SpecialAttack()
    {
        attackScript.attackID = activeMoveset.specialID;
        attackScript.canAttack = false;
        attackScript.attackStart = Time.time;
        attackScript.momentumDuration1 = activeMoveset.specialDuration1;
        attackScript.momentumDuration2 = activeMoveset.specialDuration2;
        attackScript.momentumDuration3 = activeMoveset.specialDuration3;
        attackScript.forward = activeMoveset.specialForward;
        attackScript.up = activeMoveset.specialUp;
        attackScript.forward2 = activeMoveset.specialForward2;
        attackScript.up2 = activeMoveset.specialUp2;
        attackScript.currentAttack = activeMoveset.special;
        attackScript.startupFrames = activeMoveset.specialStartUp;
        attackScript.activeFrames = activeMoveset.specialActive;
        attackScript.recoveryFrames = activeMoveset.specialRecovery;
        attackScript.massive = true;
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
        attackScript.massive = true;
    }

    void TornadoAttack()
    {
        attackScript.attackID = activeMoveset.air3ID;
        attackScript.attackStart = Time.time;
        attackScript.canAttack = false;
        attackScript.momentumDuration1 = activeMoveset.air3Duration1;
        attackScript.momentumDuration2 = activeMoveset.air3Duration2;
        attackScript.momentumDuration3 = activeMoveset.air3Duration3;
        attackScript.forward = activeMoveset.air3Forward;
        attackScript.up = activeMoveset.air3Up;
        tornado = true;
        attackScript.up2 = activeMoveset.air3Up2;
        attackScript.currentAttack = activeMoveset.air3;
        attackScript.startupFrames = activeMoveset.air3StartUp;
        attackScript.activeFrames = activeMoveset.air3Active;
        attackScript.recoveryFrames = activeMoveset.air3Recovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
        attackScript.massive = false;
    }
    public void Parry()
    {
        attackScript.attackID = activeMoveset.air1ID;
        Boss_Script.direction = attackScript.trueDirection;
        attackScript.combo = 0;
        attackScript.attackStart = Time.time;
        attackScript.momentumDuration1 = activeMoveset.air1Duration1;
        attackScript.momentumDuration2 = activeMoveset.air1Duration2;
        attackScript.momentumDuration3 = activeMoveset.air1Duration3;
        attackScript.canAttack = false;
        attackScript.forward = activeMoveset.air1Forward;
        attackScript.up = activeMoveset.air1Up;
        attackScript.forward2 = activeMoveset.air1Forward2;
        attackScript.up2 = activeMoveset.air1Up2;
        attackScript.currentAttack = activeMoveset.air1;
        attackScript.startupFrames = activeMoveset.air1StartUp;
        attackScript.activeFrames = activeMoveset.air1Active;
        attackScript.recoveryFrames = activeMoveset.air1Recovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
        attackScript.massive = true;
    }

}
