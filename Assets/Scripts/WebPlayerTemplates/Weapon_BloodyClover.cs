using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_BloodyClover : MonoBehaviour
{
    public Movelist movelist;
    [HeaderAttribute("Moveset attributes")]
    public Moveset_BloodyClover activeMoveset;
    Player_Movement playerMov;
    Player_AttackScript attackScript;
    PlayerStatus playerStatus;
    Player_Input inputManager;

    [HeaderAttribute("Attack attributes")]
    public GameObject currentAttack;
    public int attackID;
    public float attackSpeed;
    public bool canAttack = true;
    public float comboDelay;
    public bool comboStart;
    bool hasIFrames;

    [HeaderAttribute("Bloody clover")]
    public int BCCharge;
    public bool BCCharging;
    public int BCLvl1;
    public int BCLvl2;
    public int BCLvl3;

    public int UpBCCharge;
    public bool UpBCCharging;
    public int UpBCLvl1;
    public int UpBCLvl2;
    public int UpBCLvl3;

    [HeaderAttribute("Frame attributes")]

    bool startupMov;
    bool activeMov;
    bool recoveryMov;

    bool setStartVelocity;
    bool setEndVelocity;
    Vector3 startVelocity;

    float forward;
    float forward2;
    float up;
    float up2;
    float momentumDuration1;
    float momentumDuration2;
    float momentumDuration3;

    void Awake()
    {
        activeMoveset = GetComponent<Moveset_BloodyClover>();
        inputManager = GetComponentInParent<Player_Input>();
        playerStatus = gameObject.GetComponentInParent<PlayerStatus>();
        attackScript = GetComponentInParent<Player_AttackScript>();
        playerMov = GetComponentInParent<Player_Movement>();
    }

    void Start()
    {
    }
    void OnEnable()
    {
        attackScript.keepVel = false;
        UpdateDash();
    }

    void FixedUpdate()
    {
        if (!HitStopScript.hitStop)
        {
            if (inputManager.inputQueue.Count > 0 || inputManager.releaseQueue.Count > 0) PerformAction();
            attackScript.resetCounter++;
            if (comboDelay < attackScript.resetCounter) attackScript.combo = 0;
            if (BCCharging) BCCharge++;
            if (UpBCCharging) UpBCCharge++;
        }
    }

    void PerformAction()
    {
        if (inputManager.releaseQueue.Count > 0)
        {
            if (inputManager.releaseQueue[0] == 2 && BCCharging) { print("Attack ffs");  GenericAttack(); inputManager.releaseQueue.RemoveAt(0); }
        }

        if (inputManager.inputQueue.Count > 0)
        {
            if (attackScript.recovery && attackScript.specialCancelable || attackScript.canAttack && playerMov.mov)
            {
                if (inputManager.inputQueue[0] == 5 && inputManager.inputQueue.Count > 0 && playerStatus.stuncounter <= 0) { attackScript.AttackCancel(); SpecialAttack(); inputManager.inputQueue.RemoveAt(0); }
            }

            if (attackScript.combo > 0 && inputManager.inputQueue.Count > 0 && inputManager.inputQueue[0] == 2 && playerMov.ground && attackScript.canAttack && playerMov.mov
                ||
                attackScript.combo > 0 && inputManager.inputQueue.Count > 0 && inputManager.inputQueue[0] == 2 && playerMov.ground && attackScript.recovery && !attackScript.startup && !attackScript.active
                )
            {
                inputManager.inputQueue.RemoveAt(0);
                attackScript.resetCounter = 0;
                if (attackScript.combo == 2 && playerMov.ground) { AttackCancel(); NormalSlash3(activeMoveset.attack3StartUp, activeMoveset.attack3Active, activeMoveset.attack3Recovery); }
                else if (attackScript.combo == 1 && playerMov.ground) { AttackCancel(); NormalSlash2(activeMoveset.attack2StartUp, activeMoveset.attack2Active, activeMoveset.attack2Recovery); }

            }

            else if (attackScript.canAttack && !playerMov.dashing && inputManager.inputQueue.Count > 0 && playerStatus.stuncounter <= 0)
            {
                if (inputManager.inputQueue[0] == 8 && inputManager.inputQueue.Count > 0)
                {
                    inputManager.inputQueue.RemoveAt(0);
                    if (playerStatus.special >= movelist.electricCost) { Dash(); GenericAttack(); playerStatus.special -= movelist.electricCost; }
                }
                else if (inputManager.inputQueue[0] == 9 && playerMov.ground && inputManager.inputQueue.Count > 0) { inputManager.inputQueue.RemoveAt(0); if (playerStatus.special >= movelist.electricCost) { Dash(); UpSlash(activeMoveset.upAttackStartUp, activeMoveset.upAttackActive, activeMoveset.upAttackRecovery); playerStatus.special -= movelist.electricCost; } }
                else if (inputManager.inputQueue[0] == 10 && !playerMov.ground && inputManager.inputQueue.Count > 0) { inputManager.inputQueue.RemoveAt(0); if (playerStatus.special >= movelist.electricCost) { Dash(); DownSlash(activeMoveset.downAttackStartUp, activeMoveset.downAttackActive, activeMoveset.downAttackRecovery); playerStatus.special -= movelist.electricCost; } }

                else if (inputManager.inputQueue[0] == 2 && inputManager.inputQueue.Count > 0 && attackScript.combo == 0) { ChargeStart(); inputManager.inputQueue.RemoveAt(0); }

                else if (inputManager.inputQueue[0] == 3 && playerMov.ground && inputManager.inputQueue.Count > 0) { UpSlash(activeMoveset.upAttackStartUp, activeMoveset.upAttackActive, activeMoveset.upAttackRecovery); inputManager.inputQueue.RemoveAt(0); }
                else if (inputManager.inputQueue[0] == 4 && !playerMov.ground && inputManager.inputQueue.Count > 0) { DownSlash(activeMoveset.downAttackStartUp, activeMoveset.downAttackActive, activeMoveset.downAttackRecovery); inputManager.inputQueue.RemoveAt(0); }
            }
            if (attackScript.recovery && attackScript.jumpCancelable && inputManager.inputQueue.Count > 0
                || attackScript.canAttack && inputManager.inputQueue.Count > 0 && playerMov.mov
                || playerStatus.hitInvul && inputManager.inputQueue.Count > 0 && playerMov.mov)
            {
                if (inputManager.inputQueue[0] == 0 && inputManager.inputQueue.Count > 0) { attackScript.jumpCancelable = false; playerMov.Jump(); AttackCancel(); inputManager.inputQueue.RemoveAt(0); }
                else if (inputManager.inputQueue[0] == 1 && inputManager.inputQueue.Count > 0) { attackScript.jumpCancelable = false; Dash(); inputManager.inputQueue.RemoveAt(0); }
            }
        }

    }

    public void Dash() { if (!playerMov.dashing) playerMov.Dash(); }

    public void SpecialAttack()
    {
        if (playerStatus.special >= activeMoveset.specialCost)
        {
            playerStatus.special -= activeMoveset.specialCost;
            SpecialAttack(activeMoveset.specialStartUp, activeMoveset.specialActive, activeMoveset.specialRecovery);
        }
    }

    public void UpCharge()
    {
    }

    public void ChargeStart()
    {
        BCCharging = true;
    }
    public void GenericAttack()
    {
        attackScript.resetCounter = 0;

        if (playerMov.y == 0 && attackScript.combo == 0 && playerMov.ground)
        {
            if (BCCharge > BCLvl3)
                NormalSlash3(activeMoveset.attack1StartUp, activeMoveset.attack1Active, activeMoveset.attack1Recovery);
            else if (BCCharge > BCLvl2) NormalSlash2(activeMoveset.attack1StartUp, activeMoveset.attack1Active, activeMoveset.attack1Recovery);
            else if (BCCharge > BCLvl1) NormalSlash(activeMoveset.attack1StartUp, activeMoveset.attack1Active, activeMoveset.attack1Recovery);
        }


        else if (playerMov.y == 0 && attackScript.combo == 0 && !playerMov.ground) AirSlash(activeMoveset.air1StartUp, activeMoveset.air1Active, activeMoveset.air1Recovery);
        BCCharging = false;
        BCCharge = 0;
    }

    public void UpAttack()
    {
        attackScript.resetCounter = 0;

        if (playerMov.y == 0 && attackScript.combo == 0 && playerMov.ground)
        {
            if (BCCharge > BCLvl3)
                NormalSlash3(activeMoveset.attack1StartUp, activeMoveset.attack1Active, activeMoveset.attack1Recovery);
            else if (BCCharge > BCLvl2) NormalSlash2(activeMoveset.attack1StartUp, activeMoveset.attack1Active, activeMoveset.attack1Recovery);
            else if (BCCharge > BCLvl1) NormalSlash(activeMoveset.attack1StartUp, activeMoveset.attack1Active, activeMoveset.attack1Recovery);
        }
        BCCharging = false;
        BCCharge = 0;
    }

    void SpecialAttack(float startupF, float activeF, float recoveryF)
    {
        attackScript.attackID = activeMoveset.specialID;
        attackScript.hasIFrames = activeMoveset.hasiFrames;
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
        attackScript.startupFrames = startupF;
        attackScript.activeFrames = activeF;
        attackScript.recoveryFrames = recoveryF;
        attackScript.startup = true;
        attackScript.startupMov = true;
    }

    void NormalSlash(float startupF, float activeF, float recoveryF)
    {
        attackScript.jumpCancelable = true;
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
        attackScript.startupFrames = startupF;
        attackScript.activeFrames = activeF;
        attackScript.recoveryFrames = recoveryF;
        attackScript.startup = true;
        attackScript.startupMov = true;
    }
    void NormalSlash2(float startupF, float activeF, float recoveryF)
    {
        attackScript.attackID = activeMoveset.attack2ID;
        attackScript.combo = 2;
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
        attackScript.startupFrames = startupF;
        attackScript.activeFrames = activeF;
        attackScript.recoveryFrames = recoveryF;
        attackScript.startup = true;
        attackScript.startupMov = true;
    }
    void NormalSlash3(float startupF, float activeF, float recoveryF)
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
        attackScript.startupFrames = startupF;
        attackScript.activeFrames = activeF;
        attackScript.recoveryFrames = recoveryF;
        attackScript.specialCancelable = true;
        attackScript.startup = true;
        attackScript.startupMov = true;
    }

    void AirSlash(float startupF, float activeF, float recoveryF)

    {
        attackScript.jumpCancelable = true;
        attackScript.attackID = activeMoveset.air1ID;
        attackScript.canAttack = false;
        //         attackScript.combo = 1;
        attackScript.momentumDuration1 = activeMoveset.air1Duration1;
        attackScript.momentumDuration2 = activeMoveset.air1Duration2;
        attackScript.momentumDuration3 = activeMoveset.air1Duration3;
        attackScript.attackStart = Time.time;
        attackScript.forward = activeMoveset.air1Forward;
        attackScript.up = activeMoveset.air1Up;
        attackScript.forward2 = activeMoveset.air1Forward2;
        attackScript.up2 = activeMoveset.air1Up2;
        attackScript.currentAttack = activeMoveset.air1;
        attackScript.startupFrames = startupF;
        attackScript.activeFrames = activeF;
        attackScript.recoveryFrames = recoveryF;
        attackScript.startup = true;
        attackScript.startupMov = true;

    }

    void DownSlash(float startupF, float activeF, float recoveryF)
    {
        attackScript.jumpCancelable = false;
        attackScript.attackID = activeMoveset.downID;
        attackScript.attackStart = Time.time;
        attackScript.canAttack = false;
        attackScript.momentumDuration1 = activeMoveset.downAttackDuration1;
        attackScript.momentumDuration2 = activeMoveset.downAttackDuration2;
        attackScript.momentumDuration3 = activeMoveset.downAttackDuration3;
        attackScript.forward = activeMoveset.downAttackForward;
        attackScript.up = activeMoveset.downAttackUp;
        attackScript.forward2 = activeMoveset.downAttackForward2;
        attackScript.up2 = activeMoveset.downAttackUp2;
        attackScript.currentAttack = activeMoveset.downAttack;
        attackScript.startupFrames = startupF;
        attackScript.activeFrames = activeF;
        attackScript.recoveryFrames = recoveryF;
        attackScript.specialCancelable = true;
        attackScript.startup = true;
        attackScript.startupMov = true;
    }

    void UpSlash(float startupF, float activeF, float recoveryF)
    {
        attackScript.jumpCancelable = true;
        attackScript.attackID = activeMoveset.upID;
        attackScript.attackStart = Time.time;
        attackScript.canAttack = false;
        attackScript.momentumDuration1 = activeMoveset.upAttackDuration1;
        attackScript.momentumDuration2 = activeMoveset.upAttackDuration2;
        attackScript.momentumDuration3 = activeMoveset.upAttackDuration3;
        attackScript.forward = activeMoveset.upAttackForward;
        attackScript.up = activeMoveset.upAttackUp;
        attackScript.forward2 = activeMoveset.upAttackForward2;
        attackScript.up2 = activeMoveset.upAttackUp2;
        attackScript.currentAttack = activeMoveset.upAttack;
        attackScript.startupFrames = startupF;
        attackScript.activeFrames = activeF;
        attackScript.recoveryFrames = recoveryF;
        attackScript.specialCancelable = true;
        attackScript.startup = true;
        attackScript.startupMov = true;
        attackScript.keepVerticalVel = true;

    }


    public void Momentum(Vector2 vel)
    {
        playerMov.mov = false;
        playerMov.AddVelocity(vel);
    }

    public void AttackCancel()
    {
        DisableObjects();
        attackScript.startup = false;
        attackScript.active = false;
        attackScript.recovery = false;
        playerMov.mov = true;
        attackScript.canAttack = true;
    }

    void DisableObjects()
    {
        activeMoveset.attack1.SetActive(false);
        activeMoveset.attack2.SetActive(false);
        activeMoveset.attack3.SetActive(false);
        activeMoveset.air1.SetActive(false);
        activeMoveset.upAttack.SetActive(false);
        activeMoveset.downAttack.SetActive(false);
        activeMoveset.special.SetActive(false);
    }

    public void UpdateDash()
    {

        playerMov.dashDuration = activeMoveset.dashDuration;
        playerMov.dashSpeed = activeMoveset.dashSpeed;
        playerMov.dashRecovery = activeMoveset.dashRecovery;
        playerMov.currentRecovery = playerMov.dashRecovery;
        playerMov.currentDuration = playerMov.dashDuration;
    }
}
