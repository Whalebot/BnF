using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Player_Movement playerMov;
    Player_AttackScript player_AttackScript;
    PlayerStatus playerStatus;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        playerMov = GetComponent<Player_Movement>();
        player_AttackScript = GetComponent<Player_AttackScript>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Descending") || anim.GetCurrentAnimatorStateInfo(0).IsName("Landing")) anim.SetBool("FromAir", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Descending") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Landing"))
            anim.SetBool("FromAir", false);

        if (playerMov.x != 0 && playerMov.mov) anim.SetBool("Walking", true);
        else anim.SetBool("Walking", false);

        if (playerMov.running) anim.SetBool("Running", true);
        else anim.SetBool("Running", false);

        if (playerStatus.hitAnimation) anim.SetBool("Hitstun", true);
        else anim.SetBool("Hitstun", false);

        if (playerMov.isDashing) anim.SetBool("Dashing", true);
        else { anim.SetBool("Dashing", false); }


        if (playerMov.isBackdashing) anim.SetBool("Backdashing", true);
        else { anim.SetBool("Backdashing", false); }
        if (playerMov.newDash) { DashClick(); }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dash") || anim.GetCurrentAnimatorStateInfo(0).IsName("Backdash")) { anim.SetBool("FromDash", true); }
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Dash") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Backdash"))
            anim.SetBool("FromDash", false);


        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run")) { anim.SetBool("FromRun", true); }
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Run") && !anim.GetCurrentAnimatorStateInfo(0).IsName("RunStop"))
            anim.SetBool("FromRun", false);

        anim.SetInteger("AttackID", player_AttackScript.attackID);

        if (playerMov.rb.velocity.y < 0) anim.SetInteger("Ascending", -1);
        else if (playerMov.rb.velocity.y > 1)
            anim.SetInteger("Ascending", 1);
        else anim.SetInteger("Ascending", 0);

        if (playerMov.ground) anim.SetBool("Grounded", true);
        else anim.SetBool("Grounded", false);

        if (player_AttackScript.startup) { anim.SetBool("Startup", true); }
        else anim.SetBool("Startup", false);

        if (player_AttackScript.active) anim.SetBool("Active", true);
        else anim.SetBool("Active", false);

        if (player_AttackScript.recovery) anim.SetBool("Recovery", true);
        else anim.SetBool("Recovery", false);


        if (!player_AttackScript.startup && !player_AttackScript.active) anim.SetBool("Attacking", false);
        else anim.SetBool("Attacking", true);

    }
    public void DashClick()
    {
    
        anim.SetTrigger("DashClick");
        playerMov.newDash = false;
    }
}
