using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Animation : MonoBehaviour
{

    Animator anim;
    Boss_AttackScript attackScript;
    Boss_Script enemyScript;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        attackScript = GetComponent<Boss_AttackScript>();
        enemyScript = GetComponent<Boss_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("AttackID", attackScript.attackID);

        if (enemyScript.dizzy) { anim.SetBool("Dizzy", true); }
        else anim.SetBool("Dizzy", false);

        if (enemyScript.stun) { anim.SetBool("Hitstun", true); }
        else { anim.SetBool("Hitstun", false); }


        if (attackScript.startup || attackScript.active) anim.SetBool("Attacking", true);
        else anim.SetBool("Attacking", false);

        if (attackScript.startup) anim.SetBool("Startup", true);
        else anim.SetBool("Startup", false);


        if (attackScript.active) anim.SetBool("Active", true);
        else anim.SetBool("Active", false);


        if (attackScript.recovery)
        {
            anim.SetBool("Active", false);
        }

    }
}
