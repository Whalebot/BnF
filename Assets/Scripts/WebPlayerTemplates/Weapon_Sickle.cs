using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Sickle : MonoBehaviour
{
    public Movelist movelist;
    [HeaderAttribute("Moveset attributes")]
    public Moveset_Sickle activeMoveset;
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
    public int attackDirection;
    bool hasIFrames;
    int resetCounter;

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
        activeMoveset = GetComponent<Moveset_Sickle>();
        inputManager = GetComponentInParent<Player_Input>();
        playerStatus = gameObject.GetComponentInParent<PlayerStatus>();
        attackScript = GetComponentInParent<Player_AttackScript>();
        playerMov = GetComponentInParent<Player_Movement>();
    }

    void Start()
    {
    }
    private void Update()
    {
    }

    void FixedUpdate()
    {
        if (!HitStopScript.hitStop)
        {
            if (inputManager.inputQueue.Count > 0) PerformAction();
            resetCounter++;
            if (comboDelay < resetCounter) attackScript.combo = 0;
        }
    }

    void PerformAction()
    {
        if (inputManager.inputQueue.Count > 0)
        {
            if (attackScript.recovery && attackScript.specialCancelable || attackScript.canAttack)
            {
                if (inputManager.inputQueue[0] == 5 && inputManager.inputQueue.Count > 0) { attackScript.AttackCancel(); SpecialAttack(); inputManager.inputQueue.RemoveAt(0); }

            }

            if (attackScript.combo == 1 && inputManager.inputQueue.Count > 0 && attackScript.recovery)
            {
                if (inputManager.inputQueue[0] == 2 && inputManager.inputQueue.Count > 0 || inputManager.inputQueue[0] == 3 && inputManager.inputQueue.Count > 0 || inputManager.inputQueue[0] == 4 && inputManager.inputQueue.Count > 0)
                {
                    if (attackDirection == 0)
                    { NormalSlash2(activeMoveset.attack2StartUp, activeMoveset.attack2Active, activeMoveset.attack2Recovery); inputManager.inputQueue.RemoveAt(0); }
                    else if (attackDirection == 1)
                    { UpSlash2(activeMoveset.up2AttackStartUp, activeMoveset.up2AttackActive, activeMoveset.up2AttackRecovery); inputManager.inputQueue.RemoveAt(0); }
                    else if (attackDirection == 2)
                    { DownSlash2(activeMoveset.downAttackStartUp, activeMoveset.downAttackActive, activeMoveset.downAttackRecovery); inputManager.inputQueue.RemoveAt(0); }
                }
                attackScript.combo = 0;
            }

            if (attackScript.canAttack && !playerMov.dashing && inputManager.inputQueue.Count > 0)
            {

                if (inputManager.inputQueue[0] == 8 && inputManager.inputQueue.Count > 0) { inputManager.inputQueue.RemoveAt(0); if (playerStatus.special >= movelist.electricCost) { Dash(); GenericAttack(); playerStatus.special -= movelist.electricCost; } }
                else if (inputManager.inputQueue[0] == 9 && playerMov.ground && inputManager.inputQueue.Count > 0)
                {
                    inputManager.inputQueue.RemoveAt(0); if (playerStatus.special >= movelist.electricCost)
                    {
                        Dash(); resetCounter = 0;
                        if (attackScript.combo == 0) UpSlash(activeMoveset.upAttackStartUp, activeMoveset.upAttackActive, activeMoveset.upAttackRecovery);
                        else if (attackScript.combo == 1) UpSlash2(activeMoveset.up2AttackStartUp, activeMoveset.up2AttackActive, activeMoveset.up2AttackRecovery);
                    }
                }
                else if (inputManager.inputQueue[0] == 10 && !playerMov.ground && inputManager.inputQueue.Count > 0)
                {
                    inputManager.inputQueue.RemoveAt(0); if (playerStatus.special >= movelist.electricCost)
                    {
                        Dash(); DownSlash(activeMoveset.downAttackStartUp, activeMoveset.downAttackActive, activeMoveset.downAttackRecovery); playerStatus.special -= movelist.electricCost;
                    }
                }
                //Non electrics
               
                else if (inputManager.inputQueue[0] == 2 && inputManager.inputQueue.Count > 0) {
                    resetCounter = 0;
                    NormalSlash(activeMoveset.attack1StartUp, activeMoveset.attack1Active, activeMoveset.attack1Recovery); inputManager.inputQueue.RemoveAt(0); attackDirection = 0; }
                else if (inputManager.inputQueue[0] == 3 && inputManager.inputQueue.Count > 0)
                {
                    resetCounter = 0;
                    if (attackScript.combo == 0) UpSlash(activeMoveset.upAttackStartUp, activeMoveset.upAttackActive, activeMoveset.upAttackRecovery);
                    attackDirection = 1;
                    inputManager.inputQueue.RemoveAt(0);
                }
                else if (inputManager.inputQueue[0] == 4 && !playerMov.ground && inputManager.inputQueue.Count > 0) {
                    resetCounter = 0;
                    DownSlash(activeMoveset.downAttackStartUp, activeMoveset.downAttackActive, activeMoveset.downAttackRecovery); inputManager.inputQueue.RemoveAt(0);
                    attackDirection = 2;
                }
            }
            //Jump and dash
            if (attackScript.recovery && attackScript.jumpCancelable && inputManager.inputQueue.Count > 0
              || attackScript.canAttack && inputManager.inputQueue.Count > 0 && playerMov.mov
              || playerStatus.hitInvul && inputManager.inputQueue.Count > 0 && playerMov.mov)
            {
                if (inputManager.inputQueue[0] == 0 && inputManager.inputQueue.Count > 0) { attackScript.jumpCancelable = false; playerMov.Jump(); AttackCancel(); inputManager.inputQueue.RemoveAt(0); }
                else if (inputManager.inputQueue[0] == 1 && inputManager.inputQueue.Count > 0) { attackScript.jumpCancelable = false; AttackCancel();Dash(); inputManager.inputQueue.RemoveAt(0); }

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


    public void GenericAttack()
    {
        resetCounter = 0;
        if (playerMov.y == 0  && playerMov.ground) {  NormalSlash(activeMoveset.attack1StartUp, activeMoveset.attack1Active, activeMoveset.attack1Recovery); }
     //   else if (playerMov.y == 0 && attackScript.combo == 1 && playerMov.ground) NormalSlash2(activeMoveset.attack2StartUp, activeMoveset.attack2Active, activeMoveset.attack2Recovery);

      //  else if (playerMov.y == 0 && attackScript.combo == 0 && !playerMov.ground) { AirSlash(activeMoveset.air1StartUp, activeMoveset.air1Active, activeMoveset.air1Recovery); }
        //else if (playerMov.y == 0 && attackScript.combo == 1 && !playerMov.ground) { AirSlash2(activeMoveset.air2StartUp, activeMoveset.air2Active, activeMoveset.air2Recovery); }
    }

    void SpecialAttack(float startupF, float activeF, float recoveryF)
    {
        attackScript.jumpCancelable = true;
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
        attackScript.combo = 0;
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
        attackScript.startupFrames = startupF;
        attackScript.activeFrames = activeF;
        attackScript.recoveryFrames = recoveryF;
        attackScript.specialCancelable = true;
        attackScript.startup = true;
        attackScript.startupMov = true;
    }

    void AirSlash(float startupF, float activeF, float recoveryF)
    {
        attackScript.attackID = activeMoveset.air1ID;
        attackScript.canAttack = false;
        attackScript.combo = 1;
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
    void AirSlash2(float startupF, float activeF, float recoveryF)
    {
        attackScript.attackID = activeMoveset.air2ID;
        attackScript.combo = 0;
        attackScript.attackStart = Time.time;
        attackScript.momentumDuration1 = activeMoveset.air2Duration1;
        attackScript.momentumDuration2 = activeMoveset.air2Duration2;
        attackScript.momentumDuration3 = activeMoveset.air2Duration3;
        attackScript.canAttack = false;
        attackScript.forward = activeMoveset.air2Forward;
        attackScript.up = activeMoveset.air2Up;
        attackScript.forward2 = activeMoveset.air2Forward2;
        attackScript.up2 = activeMoveset.air2Up2;
        attackScript.currentAttack = activeMoveset.air2;
        attackScript.startupFrames = startupF;
        attackScript.activeFrames = activeF;
        attackScript.recoveryFrames = recoveryF;
        attackScript.specialCancelable = true;
        attackScript.startup = true;
        attackScript.startupMov = true;
    }

    void DownSlash(float startupF, float activeF, float recoveryF)
    {
        attackScript.jumpCancelable = false;
        attackScript.attackID = activeMoveset.downID;
        attackScript.attackStart = Time.time;
        attackScript.combo = 1;
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
        attackScript.startup = true;
        attackScript.startupMov = true;
    }

    void DownSlash2(float startupF, float activeF, float recoveryF)
    {
        attackScript.jumpCancelable = false;
        attackScript.attackID = activeMoveset.down2ID;
        attackScript.attackStart = Time.time;
        attackScript.canAttack = false;
        attackScript.momentumDuration1 = activeMoveset.down2AttackDuration1;
        attackScript.momentumDuration2 = activeMoveset.down2AttackDuration2;
        attackScript.momentumDuration3 = activeMoveset.down2AttackDuration3;
        attackScript.forward = activeMoveset.down2AttackForward;
        attackScript.up = activeMoveset.down2AttackUp;
        attackScript.forward2 = activeMoveset.down2AttackForward2;
        attackScript.up2 = activeMoveset.down2AttackUp2;
        attackScript.currentAttack = activeMoveset.down2Attack;
        attackScript.startupFrames = startupF;
        attackScript.activeFrames = activeF;
        attackScript.recoveryFrames = recoveryF;
        attackScript.specialCancelable = true;
        attackScript.startup = true;
        attackScript.startupMov = true;
    }


    void UpSlash(float startupF, float activeF, float recoveryF)
    {
        attackScript.attackID = activeMoveset.upID;
        attackScript.combo = 1;
        print("First");
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
        attackScript.startup = true;
        attackScript.startupMov = true;

    }

    void UpSlash2(float startupF, float activeF, float recoveryF)
    {
        attackScript.attackID = activeMoveset.up2ID;
        attackScript.combo = 0;
        attackScript.attackStart = Time.time;
        attackScript.canAttack = false;
        attackScript.momentumDuration1 = activeMoveset.up2AttackDuration1;
        attackScript.momentumDuration2 = activeMoveset.up2AttackDuration2;
        attackScript.momentumDuration3 = activeMoveset.up2AttackDuration3;
        attackScript.forward = activeMoveset.up2AttackForward;
        attackScript.up = activeMoveset.up2AttackUp;
        attackScript.forward2 = activeMoveset.up2AttackForward2;
        attackScript.up2 = activeMoveset.up2AttackUp2;
        attackScript.currentAttack = activeMoveset.up2Attack;
        attackScript.startupFrames = startupF;
        attackScript.activeFrames = activeF;
        attackScript.recoveryFrames = recoveryF;
        attackScript.specialCancelable = true;
        attackScript.startup = true;
        attackScript.startupMov = true;

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
        attackScript.recoveryFrames = 0;
        playerMov.mov = true;
        attackScript.canAttack = true;
    }

    void DisableObjects()
    {
        activeMoveset.attack1.SetActive(false);
        activeMoveset.attack2.SetActive(false);
        activeMoveset.air1.SetActive(false);
        activeMoveset.air2.SetActive(false);
        activeMoveset.upAttack.SetActive(false);
        activeMoveset.downAttack.SetActive(false);
        activeMoveset.special.SetActive(false);
    }
}
