using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Hydrangea : MonoBehaviour
{
    public Movelist movelist;
    [HeaderAttribute("Moveset attributes")]
    public Moveset_Hydrangea activeMoveset;
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
        activeMoveset = GetComponent<Moveset_Hydrangea>();
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
            if (inputManager.inputQueue.Count > 0) PerformAction();
            attackScript.resetCounter++;
            if (comboDelay < attackScript.resetCounter) attackScript.combo = 0;
        }
    }

    void PerformAction()
    {
        if (inputManager.inputQueue.Count > 0)
        {
            //SPECIAL
            if (attackScript.recovery && attackScript.specialCancelable || attackScript.canAttack && playerMov.mov)
            {
                if (inputManager.inputQueue[0] == 5 && inputManager.inputQueue.Count > 0 && playerStatus.stuncounter <= 0)
                {
                    if (playerStatus.special >= movelist.move5S.SpCost)
                    { TestSlash(movelist.move5S, movelist.moveObject5S); }
                    inputManager.inputQueue.RemoveAt(0);
                }
            }
            if (inputManager.inputQueue.Count > 0)
            {
                //COMBO
                if (attackScript.combo > 0 && inputManager.inputQueue.Count > 0 && inputManager.inputQueue[0] == 2 && playerMov.ground && attackScript.canAttack && playerMov.mov
                ||
                attackScript.combo > 0 && inputManager.inputQueue.Count > 0 && inputManager.inputQueue[0] == 2 && attackScript.recovery && !attackScript.startup && !attackScript.active
                )
                {
                    inputManager.inputQueue.RemoveAt(0);
                    attackScript.resetCounter = 0;
                    AttackCancel();
                    GenericAttack();
                }
                else if (attackScript.attackCancelable && inputManager.inputQueue[0] == 3 && inputManager.inputQueue.Count > 0 && attackScript.recovery && !attackScript.startup && !attackScript.active)
                {
                    AttackCancel();
                    if (playerMov.ground)
                    {
                        if (movelist.move8A == null) { GenericAttack(); }
                        else TestSlash(movelist.move8A, movelist.moveObject8A);
                    }
                    else
                    {

                        if (movelist.moveJ8A == null) { GenericAttack(); }
                        else TestSlash(movelist.moveJ8A, movelist.moveObjectJ8A);
                    }
                    inputManager.inputQueue.RemoveAt(0);
                }
                else if (attackScript.attackCancelable && inputManager.inputQueue[0] == 4 && inputManager.inputQueue.Count > 0 && attackScript.recovery && !attackScript.startup && !attackScript.active)
                {
                    AttackCancel();
                    if (playerMov.ground)
                    {
                        if (movelist.move2A == null) { GenericAttack(); }
                        else TestSlash(movelist.move2A, movelist.moveObject2A);
                    }
                    else
                    {
                        if (movelist.moveJ2A == null) { GenericAttack(); }
                        else TestSlash(movelist.moveJ2A, movelist.moveObjectJ2A);
                    }
                    inputManager.inputQueue.RemoveAt(0);

                }

                else if (attackScript.canAttack && !playerMov.dashing && inputManager.inputQueue.Count > 0 && playerStatus.stuncounter <= 0)
                {
                    if (inputManager.inputQueue[0] == 8 && inputManager.inputQueue.Count > 0)
                    {
                        inputManager.inputQueue.RemoveAt(0);
                        if (playerStatus.special >= movelist.electricCost) { Dash(); GenericAttack(); playerStatus.special -= movelist.electricCost; }
                    }
                    //ELECTRIC
                    else if (inputManager.inputQueue[0] == 9 && inputManager.inputQueue.Count > 0)
                    {
                        inputManager.inputQueue.RemoveAt(0);
                        if (playerStatus.special >= movelist.electricCost)
                        {
                            Dash();
                            if (playerMov.ground)
                            {
                                if (movelist.move8A == null) { GenericAttack(); }
                                else TestSlash(movelist.move8A, movelist.moveObject8A);
                            }
                            else
                            {

                                if (movelist.moveJ8A == null) { GenericAttack(); }
                                else TestSlash(movelist.moveJ8A, movelist.moveObjectJ8A);
                            }
                            playerStatus.special -= movelist.electricCost;
                        }
                    }
                    else if (inputManager.inputQueue[0] == 10 && inputManager.inputQueue.Count > 0)
                    {
                        inputManager.inputQueue.RemoveAt(0); if (playerStatus.special >= movelist.electricCost)
                        {
                            Dash();
                            AttackCancel();
                            if (playerMov.ground)
                            {
                                if (movelist.move2A == null) { GenericAttack(); }
                                else TestSlash(movelist.move2A, movelist.moveObject2A);
                            }
                            else
                            {

                                if (movelist.moveJ2A == null) { GenericAttack(); }
                                else TestSlash(movelist.moveJ2A, movelist.moveObjectJ2A);
                            }
                            playerStatus.special -= movelist.electricCost;
                        }
                    }
                    //NORMAL ATTACKS
                    //GROUND
                    else if (inputManager.inputQueue[0] == 2 && inputManager.inputQueue.Count > 0 && attackScript.combo == 0)
                    {
                        AttackCancel();
                        GenericAttack();//TestSlash(movelist.move5A, movelist.moveObject5A);
                        inputManager.inputQueue.RemoveAt(0);
                    }
                    else if (inputManager.inputQueue[0] == 3 && inputManager.inputQueue.Count > 0)
                    {
                        AttackCancel();
                        if (playerMov.ground)
                        {
                            if (movelist.move8A == null) { GenericAttack(); }
                            else TestSlash(movelist.move8A, movelist.moveObject8A);
                        }
                        else
                        {

                            if (movelist.moveJ8A == null) { GenericAttack(); }
                            else TestSlash(movelist.moveJ8A, movelist.moveObjectJ8A);
                        }
                        inputManager.inputQueue.RemoveAt(0);
                    }
                    //AIR
                    else if (inputManager.inputQueue[0] == 4 && inputManager.inputQueue.Count > 0)
                    {
                        AttackCancel();
                        if (playerMov.ground)
                        {
                            if (movelist.move2A == null) { GenericAttack(); }
                            else TestSlash(movelist.move2A, movelist.moveObject2A);
                        }
                        else
                        {

                            if (movelist.moveJ2A == null) { GenericAttack(); }
                            else TestSlash(movelist.moveJ2A, movelist.moveObjectJ2A);
                        }
                        inputManager.inputQueue.RemoveAt(0);
                    }
                }
                if (attackScript.recovery && attackScript.jumpCancelable && inputManager.inputQueue.Count > 0
                    || attackScript.canAttack && inputManager.inputQueue.Count > 0 && playerMov.mov
                    || playerStatus.hitInvul && inputManager.inputQueue.Count > 0 && playerMov.mov)
                {
                    if (inputManager.inputQueue[0] == 0 && inputManager.inputQueue.Count > 0) { attackScript.jumpCancelable = false; AttackCancel(); playerMov.Jump(); inputManager.inputQueue.RemoveAt(0); }
                    else if (inputManager.inputQueue[0] == 1 && inputManager.inputQueue.Count > 0) { attackScript.jumpCancelable = false; AttackCancel(); Dash(); inputManager.inputQueue.RemoveAt(0); }
                }
            }
        }
    }



    public void Dash() { if (!playerMov.dashing) playerMov.Dash(); }

    public void GenericAttack()
    {
        attackScript.resetCounter = 0;
        if (attackScript.combo == 0 && playerMov.ground) TestSlash(movelist.move5A, movelist.moveObject5A);
        else if (attackScript.combo == 1 && playerMov.ground) TestSlash(movelist.move5AA, movelist.moveObject5AA);
        else if (attackScript.combo == 2 && playerMov.ground) TestSlash(movelist.move5AAA, movelist.moveObject5AAA);
        else if (attackScript.combo == 3 && playerMov.ground) TestSlash(movelist.move5AAAA, movelist.moveObject5AAAA);

        else if (attackScript.combo == 0 && !playerMov.ground) TestSlash(movelist.moveJ5A, movelist.moveObjectJ5A);
        else if (attackScript.combo == 1 && !playerMov.ground) TestSlash(movelist.moveJ5AA, movelist.moveObjectJ5AA);
        else if (attackScript.combo == 2 && !playerMov.ground) TestSlash(movelist.moveJ5AAA, movelist.moveObjectJ5AAA);
        else if (attackScript.combo == 3 && !playerMov.ground) TestSlash(movelist.moveJ5AAAA, movelist.moveObjectJ5AAAA);
    }

    void TestSlash(Move move, GameObject moveObject)
    {
        print(move);
        attackScript.canAttack = false;
        attackScript.attackStart = Time.time;

        attackScript.attackID = move.ID;
        playerStatus.special -= move.SpCost;

        attackScript.resetCounter = 0;
        attackScript.combo = move.combo;
        attackScript.hasIFrames = move.invul;

        attackScript.momentumDuration1 = move.duration1;
        attackScript.momentumDuration2 = move.duration2;
        attackScript.momentumDuration3 = move.duration3;
        attackScript.forward = move.forward1;
        attackScript.up = move.up1;
        attackScript.forward2 = move.forward2;
        attackScript.up2 = move.up2;
        attackScript.forward3 = move.forward3;
        attackScript.up3 = move.up3;
        attackScript.currentAttack = moveObject;
        attackScript.startupFrames = move.startUp;
        attackScript.activeFrames = move.active;
        attackScript.recoveryFrames = move.recovery;

        attackScript.specialCancelable = move.specialCancelable;
        attackScript.jumpCancelable = move.jumpCancelable;
        attackScript.keepVerticalVel = move.keepVerticalVel;
        attackScript.landCancel = move.landCancel;
        attackScript.landCancelRecovery = move.landCancelRecovery;
        attackScript.attackCancelable = move.attackCancelable;

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
        if (movelist.moveObject5S != null) movelist.moveObject5S.SetActive(false);
        if (movelist.moveObject5SS != null) movelist.moveObject5SS.SetActive(false);
        if (movelist.moveObject5SSS != null) movelist.moveObject5SSS.SetActive(false);
        if (movelist.moveObjectExtra != null) movelist.moveObjectExtra.SetActive(false);

        if (movelist.moveObject5A != null) movelist.moveObject5A.SetActive(false);
        if (movelist.moveObject5AA != null) movelist.moveObject5AA.SetActive(false);
        if (movelist.moveObject5AAA != null) movelist.moveObject5AAA.SetActive(false);
        if (movelist.moveObject5AAAA != null) movelist.moveObject5AAAA.SetActive(false);
        if (movelist.moveObject2A != null) movelist.moveObject2A.SetActive(false);
        if (movelist.moveObject8A != null) movelist.moveObject8A.SetActive(false);

        if (movelist.moveObjectJ5A != null) movelist.moveObjectJ5A.SetActive(false);
        if (movelist.moveObjectJ5AA != null) movelist.moveObjectJ5AA.SetActive(false);
        if (movelist.moveObjectJ5AAA != null) movelist.moveObjectJ5AAA.SetActive(false);
        if (movelist.moveObjectJ5AAAA != null) movelist.moveObjectJ5AAAA.SetActive(false);
        if (movelist.moveObjectJ2A != null) movelist.moveObjectJ2A.SetActive(false);
        if (movelist.moveObjectJ8A != null) movelist.moveObjectJ8A.SetActive(false);
    }


    public void UpdateDash()
    {

        playerMov.dashDuration = movelist.dashDuration;
        playerMov.dashSpeed = movelist.dashSpeed;
        playerMov.dashRecovery = movelist.dashRecovery;
        playerMov.currentRecovery = movelist.dashRecovery;
        playerMov.currentDuration = movelist.dashDuration;
    }
}
