using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AttackScript : MonoBehaviour
{
    [HeaderAttribute("Moveset attributes")]
    //   public Weapon_MovesetScript activeMoveset;
    public int currentMoveset = 1;
    public GameObject moveset1;
    public GameObject moveset2;
    public GameObject moveset3;
    public GameObject currentMovesetObject;
    Player_Movement playerMov;
    PlayerStatus playerStatus;

    [HeaderAttribute("Attack attributes")]
    public GameObject currentAttack;
    public int attackID;
    public float attackSpeed;
    public bool canAttack = true;
    public float attackStart;
    public int jumpAttackDelay;
    public int jumpDelayCounter;
    public bool jumpDelay;

    public int combo;
    public bool comboStart;
    public int resetCounter;
    public bool hasIFrames;
    public bool hasInvul;
    public bool noClip = false;
    public bool jumpCancelable = true;
    public bool specialCancelable = true;
    public bool attackCancelable = true;
    public int j8ALimit;

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

    public float forward;
    public float forward2;
    public float forward3;
    public float up;
    public float up2;
    public float up3;
    public float momentumDuration1;
    public float momentumDuration2;
    public float momentumDuration3;
    public bool keepVel;
    public bool canMove;
    public bool keepVerticalVel;
    public bool keepHorizontalVel;
    public bool landCancel;
    public bool landCancelRecovery;
    public int landAttackFrames;
    public bool interpolate;
    public int interpol1;
    public int interpol2;
    public int interpol3;

    void Awake()
    {
        playerStatus = GetComponent<PlayerStatus>();
        playerMov = GetComponent<Player_Movement>();
        //    MovesetChange(1);
    }
    void Start()
    {
        MovesetChange(1);
    }

    void FixedUpdate()
    {
        if (playerMov.ground) j8ALimit = 1;
        if (HitStopScript.hitStop)
        {
          //  print("Recovery " + startup + "Active " + active + "Recovery " + recovery + "" + playerMov.rb.velocity);
        }
        if (!HitStopScript.hitStop)
        {
            if (jumpDelayCounter < jumpAttackDelay && jumpDelay) { canAttack = false; jumpDelayCounter++; }
            if (jumpDelayCounter >= jumpAttackDelay && jumpDelay) { canAttack = true; jumpDelayCounter = 0; jumpDelay = false; }

            Startup();
            Active();
            Recovery();
        }
    }

    void Startup()
    {
        if (startup)
        {
            if (hasIFrames) gameObject.layer = LayerMask.NameToLayer("iFrames");
            else if (hasInvul) gameObject.layer = LayerMask.NameToLayer("Invul");
            else if (noClip) gameObject.layer = LayerMask.NameToLayer("NoClip");
            startupFrames -= 1;
            if (startupMov && !canMove)
            {
                if (interpolate)
                    Momentum(new Vector2(transform.localScale.x * forward * ((momentumDuration1) / interpol1), up * ((momentumDuration1) / interpol1)));

                else
                    Momentum(new Vector2(transform.localScale.x * forward, up));
                momentumDuration1 -= 1;
            }
            else if (startupMov && canMove) Momentum(new Vector2(playerMov.x * playerMov.m_vel * Time.deltaTime, playerMov.rb.velocity.y));
        }

        if (startupFrames <= 0 && startup)
        {
            startup = false;
            active = true;
            canAttack = false;
            currentAttack.SetActive(true);
            startupFrames = 1;
            activeMov = true;
        }
        if (momentumDuration1 <= 0 && startupMov && !keepVel)
        {
            Momentum(new Vector2(0, playerMov.rb.velocity.y));
            startupMov = false;
        }
        else if (momentumDuration1 <= 0 && startupMov && canMove)
        {
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
            activeFrames -= 1;

            if (activeMov && !canMove)
            {
                momentumDuration2 -= 1;
                if (interpolate) Momentum(new Vector2(transform.localScale.x * forward2 * (1 - (momentumDuration2) / interpol2), up2 * (1 - (momentumDuration2) / interpol2)));
                else Momentum(new Vector2(transform.localScale.x * forward2, up2));
            }
            else if (activeMov && canMove) Momentum(new Vector2(playerMov.x * playerMov.m_vel * Time.deltaTime, playerMov.rb.velocity.y));

            if (momentumDuration2 <= 0 && activeMov && !canMove)
            {
                if(!keepHorizontalVel && !keepVerticalVel)
                Momentum(new Vector2(0, playerMov.rb.velocity.y));
                activeMov = false;
            }

            if (momentumDuration2 <= 0 && !activeMov && !canMove && playerMov.rb.velocity.x != 0) {
                if (!keepHorizontalVel && !keepVerticalVel)
                    Momentum(new Vector2(0, playerMov.rb.velocity.y));
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
            else gameObject.layer = LayerMask.NameToLayer("Player");
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
            playerMov.rb.mass = 1;
            recoveryFrames -= 1;

            playerMov.DashRecovery();
            if (recoveryMov)
            {

                momentumDuration3 -= 1;
                if (!keepVerticalVel && !keepHorizontalVel && !canMove)
                {

                    Momentum(new Vector2(transform.localScale.x * forward3, up3));
                }

                else if (keepVerticalVel && !keepHorizontalVel) Momentum(new Vector2(transform.localScale.x * forward3, playerMov.rb.velocity.y));
                else if (!keepVerticalVel && keepHorizontalVel) { Momentum(new Vector2(playerMov.rb.velocity.x, up3)); }
                else if (keepVerticalVel && keepHorizontalVel)
                {
                    Momentum(new Vector2(playerMov.rb.velocity.x, playerMov.rb.velocity.y));
                }
            }    
        }
        if (momentumDuration3 <= 0 && recoveryMov && !canMove)
        {
            if (!keepHorizontalVel && !keepVerticalVel)
                Momentum(new Vector2(0, playerMov.rb.velocity.y));
            recoveryMov = false;
        }
        else if (momentumDuration3 <= 0 && recoveryMov && canMove)
        {
            recoveryMov = false;
        }
        if (recoveryFrames <= 0 && recovery)
        {
            specialCancelable = false;
            gameObject.layer = LayerMask.NameToLayer("Player");
            canAttack = true;

            recovery = false;
            keepVel = false;
            keepVerticalVel = false;
            landCancelRecovery = false;
            attackCancelable = false;
            playerMov.dashing = false;
            playerMov.mov = true;
            DisableObjects();
        }
    }

    public void Momentum(Vector2 vel)
    {
        playerMov.mov = false;
            playerMov.AddVelocity(vel);
      //  else if (playerMov.ground && !canAttack) playerMov.AddVelocity(vel + playerMov.inheritedVelocity);
    }

    public void MovesetChange(int weaponNumber)
    {
        AttackCancel();
        if (moveset1 != null)
        {
            switch (weaponNumber)
            {
                case 1:
                    moveset1.SetActive(true);
                    if (moveset2 != null) moveset2.SetActive(false);
                    if (moveset3 != null) moveset3.SetActive(false);
                    if (moveset1 != null) currentMovesetObject = moveset1;
                    currentMoveset = 1; return;
                case 2:
                    if (moveset2 != null) moveset2.SetActive(true);
                    if (moveset1 != null) moveset1.SetActive(false);
                    if (moveset3 != null) moveset3.SetActive(false);
                    if (moveset2 != null) currentMovesetObject = moveset2;
                    currentMoveset = 2; return;
                case 3:
                    if (moveset3 != null) moveset3.SetActive(true);
                    if (moveset2 != null) moveset2.SetActive(false);
                    if (moveset1 != null) moveset1.SetActive(false);
                    if (moveset3 != null) currentMovesetObject = moveset3;
                    currentMoveset = 3; return;
                default: return;
            }
        }
    }

    public void Recover()
    {
        combo = 0;
        if (active || startup)
        {
            if (landCancel)
            {
                momentumDuration2 = 0;
                startup = false;

                if (landCancelRecovery) { active = true; activeFrames = landAttackFrames; }
                else { active = false; recovery = true; recoveryFrames = 0; }
            }
        }
    }

    public void AttackCancel()
    {
        DisableObjects();

        momentumDuration1 = 0;
        momentumDuration2 = 0;
        momentumDuration3 = 0;
        startupMov = false;
        activeMov = false;
        recoveryMov = false;
        DisableObjects();
        startup = false;
        active = false;
        recovery = false;
        if (playerStatus.canTakeDmg)
        {
            playerMov.mov = true;
            canAttack = true;
        }
    }

    void DisableObjects()
    {
        foreach (Transform child in transform)
        {
            foreach (Transform child2 in child) { child2.gameObject.SetActive(false); }
        }
    }
}
