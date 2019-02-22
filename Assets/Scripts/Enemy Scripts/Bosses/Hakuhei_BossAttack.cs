using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hakuhei_BossAttack : MonoBehaviour
{

    public GameObject moveset1;
    Hakuhei_Moveset activeMoveset;
    Boss_Script Boss_Script;
    Boss_AttackScript attackScript;
    GameObject target;
    public float ray;
    public float directionChangeDelay;
    public float distance;
    public float distance2;
    public bool dive;
    int crossUpDirection;
    int RNGCount;
    int RNGMixup;
    float directionChange;
    public float minDelay;
    public float maxDelay;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        activeMoveset = moveset1.GetComponent<Hakuhei_Moveset>();
        Boss_Script = GetComponent<Boss_Script>();
        attackScript = GetComponent<Boss_AttackScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Hakuhei();
        directionChangeDelay = Random.Range(minDelay, maxDelay);
        if (attackScript.recovery || attackScript.active) dive = false;

        if (!attackScript.startupMov && dive || !dive) transform.GetChild(1).gameObject.SetActive(true);
        else if (dive)
        {
            gameObject.layer = LayerMask.NameToLayer("EnemyiFrames");
            transform.GetChild(1).gameObject.SetActive(false);
        }


        if (dive && Time.time > directionChange + directionChangeDelay) { attackScript.forward = transform.localScale.x * attackScript.trueDirection * activeMoveset.specialForward; }
        if (transform.position.x <= target.transform.position.x + 5 && transform.position.x >= target.transform.position.x - 5) directionChange = Time.time;

    }

    void Hakuhei()
    {
        if (Boss_Script.mode == 1)
        {
           
            /*
            if (Physics2D.Raycast(transform.position, -Vector2.right, ray, LayerMask.GetMask("Platform")) && (transform.position - target.transform.position).magnitude < distance2 && attackScript.canAttack && attackScript.attackStart + attackScript.attackSpeed < Time.time && !Boss_Script.stun)
            { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); Boss_Script.Jump(); Boss_Script.direction = -attackScript.trueDirection; crossUpDirection = -1; CrossUp(); }
            else if (Physics2D.Raycast(transform.position, Vector2.right, ray, LayerMask.GetMask("Platform"))
                && (transform.position - target.transform.position).magnitude < distance2 && attackScript.canAttack
                && attackScript.attackStart + attackScript.attackSpeed < Time.time
                && !Boss_Script.stun)
            { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); Boss_Script.Jump(); Boss_Script.direction = -attackScript.trueDirection; crossUpDirection = -1; CrossUp(); }
            */
            if ((transform.position - target.transform.position).magnitude > distance && attackScript.canAttack && attackScript.attackStart + attackScript.attackSpeed < Time.time && !Boss_Script.stun && Boss_Script.ground)
            {
                RNGMixup = Random.Range(0, 7);
                if (RNGMixup > 3)Feint();
                if (RNGMixup < 4) SpecialAttack();
                Boss_Script.direction = attackScript.trueDirection;

            }
            if ((transform.position - target.transform.position).magnitude < distance2 && attackScript.canAttack && attackScript.attackStart + attackScript.attackSpeed < Time.time && !Boss_Script.stun)
            {
                int RNG = Random.Range(0, 3);
                if (RNG == 2) attackScript.attackSpeed = maxDelay;
                Boss_Script.direction = attackScript.trueDirection;
                RNGCount = Random.Range(0, 7);
                //If grounded
                //attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay);
                if (Boss_Script.ground)
                {
                    //If target is airborne
                    if (target.transform.position.y > transform.position.y + 5)
                    {
                        if (RNGCount == 0)
                        {
                            RNGMixup = Random.Range(0, 7);
                            if (RNGMixup > 3) Feint();
                            if (RNGMixup < 4) SpecialAttack();
                        }
                        if (RNGCount < 3 && RNGCount > 0) { if (RNG == 2) attackScript.attackSpeed = maxDelay; UpSlash(); }
                        else if (RNGCount >= 3) { if (RNG == 2) attackScript.attackSpeed = maxDelay; DragonPunch(); }

                    }
                    //If target is grounded
                    else
                    {
                        if (RNGCount == 0)
                        {
                            RNGMixup = Random.Range(0, 7);
                            if (RNGMixup > 3) Feint();
                            if (RNGMixup < 4) SpecialAttack();
                        }
                        if (RNGCount == 1 && (transform.position - target.transform.position).magnitude < distance2) { DragonPunch(); }
                        else if (RNGCount == 1) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); UpSlash(); }
                        else if (RNGCount == 2) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); CrossUp(); }
                        else if (RNGCount >= 3)
                        {
                            if (attackScript.combo == 0) { attackScript.attackSpeed = 0; NormalSlash(); attackScript.comboCounter = 1; }
                            else if (attackScript.combo == 1) { NormalSlash2(); attackScript.comboCounter = 2; }
                            else if (attackScript.combo == 2)
                            {
                                NormalSlash();
                                attackScript.comboCounter = 0;
                                attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay);
                            }
                        }


                    }
                }
                //If airborne
                if (!Boss_Script.ground)
                {   //If target is airborne
                    if (target.transform.position.y >= transform.position.y - 5)
                    {
                        if (RNGCount < 3) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); DownDash(); }
                        if (RNGCount >= 3 && RNGCount < 7) { NormalSlash2(); }
                    }
                    //If target is grounded
                    else if (target.transform.position.y < transform.position.y - 5)
                    {
                        if (RNGCount == 3) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); DownDash(); }
                        if (RNGCount < 3 && (transform.position - target.transform.position).magnitude < distance2) { DownSlash(); }
                        if (RNGCount >= 5) { DashDownSlash(); }
                    }
                }//If target is airborne


                //   if (RNGCount == 7) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); Boss_Script.Jump(); DownDash(); }
                //  if (RNGCount == 8) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); Boss_Script.Jump(); Boss_Script.direction = -attackScript.trueDirection; crossUpDirection = -1; CrossUp(); }
                // if (RNGCount >= 9) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); Boss_Script.Jump(); UpDash(); }
            }

            /*   else if (!Boss_Script.stun && (transform.position - target.transform.position).magnitude > distance2 && (transform.position - target.transform.position).magnitude < distance && attackScript.canAttack && attackScript.attackStart + attackScript.attackSpeed < Time.time)
               {
                   DownDash();
                   Boss_Script.direction = attackScript.trueDirection;
                   attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay);
               }*/
        }
        if (Boss_Script.mode == 2)
        {
            if ((transform.position - target.transform.position).magnitude < distance2 && attackScript.canAttack && attackScript.attackStart + attackScript.attackSpeed < Time.time && !Boss_Script.stun)
            {
                if (Physics2D.Raycast(transform.position, -Vector2.right, ray, LayerMask.GetMask("Platform")) && (transform.position - target.transform.position).magnitude < distance2 && attackScript.canAttack && attackScript.attackStart + attackScript.attackSpeed < Time.time && !Boss_Script.stun)
                {
                    attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); Boss_Script.Jump(); Boss_Script.direction = -attackScript.trueDirection; crossUpDirection = -1; CrossUp();
                }
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
                        UpSlash();
                        attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay);
                    }
                }
                if (RNGCount == 7) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); DownDash(); }
                if (RNGCount == 8) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); Boss_Script.Jump(); Boss_Script.direction = -attackScript.trueDirection; crossUpDirection = -1; CrossUp(); }
                if (RNGCount == 9) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); UpDash(); }
                if (RNGCount >= 10) { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); DragonPunch(); }
            }
            else if (!Boss_Script.stun && (transform.position - target.transform.position).magnitude > distance2 && (transform.position - target.transform.position).magnitude < distance && attackScript.canAttack && attackScript.attackStart + attackScript.attackSpeed < Time.time)
            {
                RNGCount = Random.Range(1, 3);
                Boss_Script.direction = attackScript.trueDirection;
                attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay);
                if (RNGCount == 1)
                    DragonPunch();
                if (RNGCount == 2)
                    DownDash();

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
        attackScript.forward3 = activeMoveset.attack1Forward3;
        attackScript.up3 = activeMoveset.attack1Up3;
        attackScript.currentAttack = activeMoveset.attack1;
        attackScript.startupFrames = activeMoveset.attack1StartUp;
        attackScript.activeFrames = activeMoveset.attack1Active;
        attackScript.recoveryFrames = activeMoveset.attack1Recovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
        attackScript.setYVel = false;
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
        attackScript.forward3 = activeMoveset.attack2Forward3;
        attackScript.up3 = activeMoveset.attack2Up3;
        attackScript.currentAttack = activeMoveset.attack2;
        attackScript.startupFrames = activeMoveset.attack2StartUp;
        attackScript.activeFrames = activeMoveset.attack2Active;
        attackScript.recoveryFrames = activeMoveset.attack2Recovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
        attackScript.setYVel = false;
    }
    void UpSlash()
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
        attackScript.forward3 = activeMoveset.attack3Forward3;
        attackScript.up3 = activeMoveset.attack3Up3;
        attackScript.currentAttack = activeMoveset.attack3;
        attackScript.startupFrames = activeMoveset.attack3StartUp;
        attackScript.activeFrames = activeMoveset.attack3Active;
        attackScript.recoveryFrames = activeMoveset.attack3Recovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
        attackScript.setYVel = true;
    }
    void DownDash()
    {
        attackScript.attackID = activeMoveset.downID;
        attackScript.combo = 0;
        attackScript.attackStart = Time.time;
        attackScript.momentumDuration1 = activeMoveset.downDashDuration1;
        attackScript.momentumDuration2 = activeMoveset.downDashDuration2;
        attackScript.momentumDuration3 = activeMoveset.downDashDuration3;
        attackScript.canAttack = false;
        attackScript.forward = activeMoveset.downDashForward;
        attackScript.up = activeMoveset.downDashUp;
        attackScript.forward2 = activeMoveset.downDashForward2;
        attackScript.up2 = activeMoveset.downDashUp2;
        attackScript.forward3 = activeMoveset.downDashForward3;
        attackScript.up3 = activeMoveset.downDashUp3;
        attackScript.currentAttack = activeMoveset.downDash;
        attackScript.startupFrames = activeMoveset.downDashStartUp;
        attackScript.activeFrames = activeMoveset.downDashActive;
        attackScript.recoveryFrames = activeMoveset.downDashRecovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
        attackScript.setYVel = true;
    }

    void DownSlash()
    {
        attackScript.attackID = activeMoveset.downID;
        attackScript.combo = 0;
        attackScript.attackStart = Time.time;
        attackScript.momentumDuration1 = activeMoveset.downSlashDuration1;
        attackScript.momentumDuration2 = activeMoveset.downSlashDuration2;
        attackScript.momentumDuration3 = activeMoveset.downSlashDuration3;
        attackScript.canAttack = false;
        attackScript.forward = activeMoveset.downSlashForward;
        attackScript.up = activeMoveset.downSlashUp;
        attackScript.forward2 = activeMoveset.downSlashForward2;
        attackScript.up2 = activeMoveset.downSlashUp2;
        attackScript.forward3 = activeMoveset.downSlashForward3;
        attackScript.up3 = activeMoveset.downSlashUp3;
        attackScript.currentAttack = activeMoveset.downSlash;
        attackScript.startupFrames = activeMoveset.downSlashStartUp;
        attackScript.activeFrames = activeMoveset.downSlashActive;
        attackScript.recoveryFrames = activeMoveset.downSlashRecovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
        attackScript.setYVel = true;
    }

    void DashDownSlash()
    {
        attackScript.attackID = activeMoveset.downID;
        attackScript.combo = 0;
        attackScript.attackStart = Time.time;
        attackScript.momentumDuration1 = activeMoveset.dashDownSlashDuration1;
        attackScript.momentumDuration2 = activeMoveset.dashDownSlashDuration2;
        attackScript.momentumDuration3 = activeMoveset.dashDownSlashDuration3;
        attackScript.canAttack = false;
        attackScript.forward = activeMoveset.dashDownSlashForward;
        attackScript.up = activeMoveset.dashDownSlashUp;
        attackScript.forward2 = activeMoveset.dashDownSlashForward2;
        attackScript.up2 = activeMoveset.dashDownSlashUp2;
        attackScript.forward3 = activeMoveset.dashDownSlashForward3;
        attackScript.up3 = activeMoveset.dashDownSlashUp3;
        attackScript.currentAttack = activeMoveset.dashDownSlash;
        attackScript.startupFrames = activeMoveset.dashDownSlashStartUp;
        attackScript.activeFrames = activeMoveset.dashDownSlashActive;
        attackScript.recoveryFrames = activeMoveset.dashDownSlashRecovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
        attackScript.setYVel = true;
    }

    void UpDash()
    {
        attackScript.attackID = activeMoveset.upID;
        attackScript.combo = 0;
        attackScript.attackStart = Time.time;
        attackScript.momentumDuration1 = activeMoveset.upDashDuration1;
        attackScript.momentumDuration2 = activeMoveset.upDashDuration2;
        attackScript.momentumDuration3 = activeMoveset.upDashDuration3;
        attackScript.canAttack = false;
        attackScript.forward = activeMoveset.upDashForward;
        attackScript.up = activeMoveset.upDashUp;
        attackScript.forward2 = activeMoveset.upDashForward2;
        attackScript.up2 = activeMoveset.upDashUp2;
        attackScript.up3 = activeMoveset.upDashUp3;
        attackScript.forward3 = activeMoveset.upDashForward3;
        attackScript.currentAttack = activeMoveset.upDash;
        attackScript.startupFrames = activeMoveset.upDashStartUp;
        attackScript.activeFrames = activeMoveset.upDashActive;
        attackScript.recoveryFrames = activeMoveset.upDashRecovery;
        attackScript.startup = true;
        attackScript.setYVel = true;
        attackScript.startupMov = true;
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
        attackScript.startup = true;
        attackScript.startupMov = true;
        attackScript.setYVel = true;
        dive = true;
    }
    void Feint()
    {
        attackScript.attackID = activeMoveset.feintID;
        attackScript.canAttack = false;
        attackScript.attackStart = Time.time;
        attackScript.momentumDuration1 = activeMoveset.feintDuration1;
        attackScript.momentumDuration2 = activeMoveset.feintDuration2;
        attackScript.momentumDuration3 = activeMoveset.feintDuration3;
        attackScript.forward = activeMoveset.feintForward;
        attackScript.up = activeMoveset.feintUp;
        attackScript.forward2 = activeMoveset.feintForward2;
        attackScript.up2 = activeMoveset.feintUp2;
        attackScript.currentAttack = activeMoveset.feint;
        attackScript.startupFrames = activeMoveset.feintStartUp;
        attackScript.activeFrames = activeMoveset.feintActive;
        attackScript.recoveryFrames = activeMoveset.feintRecovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
        attackScript.setYVel = true;
        dive = true;
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
        attackScript.up3 = activeMoveset.air2Up3;
        attackScript.forward3 = activeMoveset.air2Forward3;
        attackScript.currentAttack = activeMoveset.air2;
        attackScript.startupFrames = activeMoveset.air2StartUp;
        attackScript.activeFrames = activeMoveset.air2Active;
        attackScript.recoveryFrames = activeMoveset.air2Recovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
        attackScript.setYVel = false;
        attackScript.willJump = true;
        print("Should jump");
    }

    void DragonPunch()
    {
        attackScript.attackID = activeMoveset.DPID;
        attackScript.attackStart = Time.time;
        attackScript.canAttack = false;
        attackScript.momentumDuration1 = activeMoveset.DPDuration1;
        attackScript.momentumDuration2 = activeMoveset.DPDuration2;
        attackScript.momentumDuration3 = activeMoveset.DPDuration3;
        attackScript.forward = activeMoveset.DPForward;
        attackScript.forward2 = activeMoveset.DPForward2;
        attackScript.up = activeMoveset.DPUp;
        attackScript.up2 = activeMoveset.DPUp2;
        attackScript.up3 = activeMoveset.DPUp3;
        attackScript.forward3 = activeMoveset.DPForward3;
        attackScript.currentAttack = activeMoveset.DP;
        attackScript.startupFrames = activeMoveset.DPStartUp;
        attackScript.activeFrames = activeMoveset.DPActive;
        attackScript.recoveryFrames = activeMoveset.DPRecovery;
        attackScript.startup = true;
        attackScript.startupMov = true;
        attackScript.setYVel = true;
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
    }

}
