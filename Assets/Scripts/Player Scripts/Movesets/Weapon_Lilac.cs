using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Lilac : MonoBehaviour
{

    public Movelist movelist;
    [HeaderAttribute("Moveset attributes")]
    public Moveset_Lilac activeMoveset;
    Player_Movement playerMov;
    Player_AttackScript attackScript;
    PlayerStatus playerStatus;
    Player_Input inputManager;

    [HeaderAttribute("Attack attributes")]
    public GameObject currentAttack;
    public int attackID;
    public float attackSpeed;
    public bool canAttack = true;
    float attackStart;
    public float comboDelay;
    public bool comboStart;
    int resetCounter;
    bool hasIFrames;

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
        activeMoveset = GetComponent<Moveset_Lilac>();
        inputManager = GetComponentInParent<Player_Input>();
        playerStatus = gameObject.GetComponentInParent<PlayerStatus>();
        attackScript = GetComponentInParent<Player_AttackScript>();
        playerMov = GetComponentInParent<Player_Movement>();
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
            if (attackScript.recovery && attackScript.specialCancelable || attackScript.canAttack && inputManager.inputQueue.Count > 0 || playerMov.mov && inputManager.inputQueue.Count > 0)
            {
                if (inputManager.inputQueue[0] == 5 && inputManager.inputQueue.Count > 0) { SpecialAttack(); inputManager.inputQueue.RemoveAt(0); }
            }

            if (attackScript.recovery && attackScript.jumpCancelable || attackScript.canAttack)
            {
                if (inputManager.inputQueue[0] == 0) { AttackCancel(); playerMov.Jump(); inputManager.inputQueue.RemoveAt(0); }
            }
            if (attackScript.canAttack && !playerMov.dashing && inputManager.inputQueue.Count > 0)
            {

                if (inputManager.inputQueue[0] == 8 && inputManager.inputQueue.Count > 0) { inputManager.inputQueue.RemoveAt(0); if (playerStatus.special >= movelist.electricCost) { Dash(); GenericAttack(); playerStatus.special -= movelist.electricCost; } }
                else if (inputManager.inputQueue[0] == 9 && playerMov.ground && inputManager.inputQueue.Count > 0) { inputManager.inputQueue.RemoveAt(0); }
                else if (inputManager.inputQueue[0] == 10 && !playerMov.ground && inputManager.inputQueue.Count > 0) { inputManager.inputQueue.RemoveAt(0); }
                else if (inputManager.inputQueue[0] == 1 && inputManager.inputQueue.Count > 0) { Dash(); inputManager.inputQueue.RemoveAt(0); }
                else if (inputManager.inputQueue[0] == 2 && inputManager.inputQueue.Count > 0) { GenericAttack(); inputManager.inputQueue.RemoveAt(0); }
                else if (inputManager.inputQueue[0] == 3 && playerMov.ground && inputManager.inputQueue.Count > 0) { inputManager.inputQueue.RemoveAt(0); }
                else if (inputManager.inputQueue[0] == 4 && !playerMov.ground && inputManager.inputQueue.Count > 0) { inputManager.inputQueue.RemoveAt(0); }
            }
        }
    }

    public void Dash() { if (playerMov.mov && !playerMov.dashing && attackScript.canAttack) playerMov.Dash(); }

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
        if (playerMov.y == 0 && attackScript.combo == 0 && playerMov.ground) NormalSlash(activeMoveset.attack1StartUp, activeMoveset.attack1Active, activeMoveset.attack1Recovery);

        else if (playerMov.y == 0 && attackScript.combo == 0 && !playerMov.ground) AirSlash(activeMoveset.air1StartUp, activeMoveset.air1Active, activeMoveset.air1Recovery);
        //     else if (playerMov.y == 0 && attackScript.combo == 1 && !playerMov.ground) AirSlash2(activeMoveset.air2StartUp, activeMoveset.air2Active, activeMoveset.air2Recovery);
        //     else if (playerMov.y == 0 && attackScript.combo == 2 && !playerMov.ground) AirSlash3(activeMoveset.air3StartUp, activeMoveset.air3Active, activeMoveset.air3Recovery);
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


    void AirSlash(float startupF, float activeF, float recoveryF)
    {
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
        activeMoveset.air1.SetActive(false);
        activeMoveset.special.SetActive(false);
    }
}
