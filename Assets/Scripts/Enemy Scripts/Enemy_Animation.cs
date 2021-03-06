﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animation : MonoBehaviour
{

    Animator anim;
    Enemy_AttackScript attackScript;
    EnemyScript enemyScript;
    Enemy_Movement enemyMov;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        attackScript = GetComponent<Enemy_AttackScript>();
        enemyScript = GetComponent<EnemyScript>();
        enemyMov = GetComponent<Enemy_Movement>();
    }
   
    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("AttackID", attackScript.attackID);

        if (enemyScript.dizzy) { anim.SetBool("Dizzy", true); }
        else anim.SetBool("Dizzy", false);


        if (enemyScript.knockout) { anim.SetBool("Knockout", true); }
        else anim.SetBool("Knockout", false);

        if (enemyScript.knockdown) { anim.SetBool("Knockdown", true); }
        else anim.SetBool("Knockdown", false);

        if (enemyScript.retreatJump) { anim.SetBool("Retreat", true); }
        else anim.SetBool("Retreat", false);


        if (enemyMov.mov && enemyMov.direction != 0) { anim.SetBool("Walking", true); }
        else { anim.SetBool("Walking", false); }

        if (enemyScript.stun) { anim.SetBool("Hitstun", true); }
        else { anim.SetBool("Hitstun", false); }


        if (enemyMov.rb.velocity.y < 0) anim.SetInteger("Ascending", -1);
        else if (enemyMov.rb.velocity.y > 1)
            anim.SetInteger("Ascending", 1);
        else anim.SetInteger("Ascending", 0);

        if (enemyMov.ground) anim.SetBool("Grounded", true);
        else anim.SetBool("Grounded", false);


        if (attackScript.state != Enemy_AttackScript.State.Neutral) anim.SetBool("Attacking", true);
        else anim.SetBool("Attacking", false);

        switch (attackScript.state)
        {
            case Enemy_AttackScript.State.Startup:
                anim.SetBool("Startup", true);
                anim.SetBool("Active", false);
                anim.SetBool("Recovery", false);
                break;
            case Enemy_AttackScript.State.Active:
                anim.SetBool("Active", true);
                anim.SetBool("Startup", false);
                anim.SetBool("Recovery", false);
                break;
            case Enemy_AttackScript.State.Recovery:
                anim.SetBool("Startup", false);
                anim.SetBool("Active", false);
                anim.SetBool("Recovery", true);
                break;
            case Enemy_AttackScript.State.Neutral:
                anim.SetBool("Startup", false);
                anim.SetBool("Active", false);
                anim.SetBool("Recovery", false);
                break;
            default: break;
        }



        if (enemyMov.rb.velocity.y < 0) anim.SetInteger("Ascending", -1);
        else if (enemyMov.rb.velocity.y > 1)
            anim.SetInteger("Ascending", 1);
        else anim.SetInteger("Ascending", 0);

    }
}
