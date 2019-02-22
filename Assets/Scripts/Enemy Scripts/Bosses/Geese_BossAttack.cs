using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geese_BossAttack : MonoBehaviour
{

    public GameObject moveset1;
    Geese_Moveset activeMoveset;
    Boss_Script Boss_Script;
    Boss_AttackScript attackScript;
    GameObject target;


    int RNGCount;

    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        activeMoveset = moveset1.GetComponent<Geese_Moveset>();
        Boss_Script = GetComponent<Boss_Script>();
        attackScript = GetComponent<Boss_AttackScript>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        SwanBoss();
		
	}


    void SwanBoss()
    {
        if ((transform.position - target.transform.position).magnitude < attackScript.distance2 && attackScript.canAttack && attackScript.attackStart + attackScript.attackSpeed < Time.time)
        {
            RNGCount = Random.Range(1, 8);
            if (RNGCount == 1 && Boss_Script.ground && !Boss_Script.stun) { Boss_Script.direction = attackScript.trueDirection; attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); SpecialAttack(); }
            if (RNGCount >= 2 && !Boss_Script.stun)
            {
                attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay) * 0.5F; if (target.transform.position.y > transform.position.y && Boss_Script.ground) Boss_Script.Jump();
                if (attackScript.combo == 0) { Boss_Script.direction = attackScript.trueDirection; NormalSlash(); attackScript.comboCounter = 0; }
                else if (attackScript.combo == 1) { Boss_Script.direction = attackScript.trueDirection; NormalSlash2(); attackScript.comboCounter = 0; }
                else if (attackScript.combo == 2)
                {
                    attackScript.attackSpeed = 5;
                    Boss_Script.direction = attackScript.trueDirection; NormalSlash3();
                }
            }
        }
        else if (!Boss_Script.stun && (transform.position - target.transform.position).magnitude > attackScript.distance2 && (transform.position - target.transform.position).magnitude < attackScript.distance && attackScript.canAttack && attackScript.attackStart + attackScript.attackSpeed < Time.time)
        { attackScript.attackSpeed = Random.Range(attackScript.minDelay, attackScript.maxDelay); Boss_Script.direction = attackScript.trueDirection; SpecialAttack(); }
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
    }

}
