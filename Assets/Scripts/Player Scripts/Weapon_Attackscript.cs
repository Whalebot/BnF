using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Attackscript : MonoBehaviour
{

    public Movelist movelist;
    [HeaderAttribute("Moveset attributes")]
    Player_Movement playerMov;
    Player_AttackScript attackScript;
    PlayerStatus playerStatus;
    Player_Input inputManager;

    [HeaderAttribute("Attack attributes")]
    public GameObject currentAttack;
    public int attackID;
    public float attackSpeed;
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

    public int chargeCounter;
    public int chargeLevel;
    public bool chargeStart;
    bool isChargeAttack;
    int justFrame;
    public GameObject justFrameSignal;

    void Awake()
    {
        movelist = GetComponent<Movelist>();
        inputManager = GetComponentInParent<Player_Input>();
        playerStatus = gameObject.GetComponentInParent<PlayerStatus>();
        attackScript = GetComponentInParent<Player_AttackScript>();
        playerMov = GetComponentInParent<Player_Movement>();
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
            PerformAction();
            if (attackScript.startup && chargeStart)
            {
                chargeCounter++;
                if (chargeCounter == 22 - inputManager.releaseWindow && movelist.justFrameWeapon) Instantiate(justFrameSignal, transform.position, Quaternion.identity);
            }
            chargeLevel = chargeCounter / 25;
            attackScript.resetCounter++;
            if (comboDelay < attackScript.resetCounter) attackScript.combo = 0;
        }
    }
    void ChargeWeapon()
    {
        {
            if (inputManager.releaseQueue.Count > 0)
            {
                chargeCounter = 0;
                chargeStart = false;
            }

            if (attackScript.combo == 1)
            {
                if (chargeCounter >= movelist.move5A.startUp - 1 && attackScript.startup && chargeStart && chargeLevel == 1)
                {
                    TestSlash(movelist.moveC5A, movelist.moveObjectC5A);

                }
                if (chargeCounter >= movelist.moveC5A.startUp - 1 && attackScript.startup && chargeStart && chargeLevel == 2)
                {
                    chargeStart = false;
                    TestSlash(movelist.moveCC5A, movelist.moveObjectCC5A);
                }
            }
            else if (attackScript.combo == 2)
            {
                if (chargeCounter >= movelist.move5AA.startUp - 1 && attackScript.startup && chargeStart && chargeLevel == 1)
                {
                    TestSlash(movelist.moveC5AA, movelist.moveObjectC5AA);

                }
                if (chargeCounter >= movelist.moveC5AA.startUp - 1 && attackScript.startup && chargeStart && chargeLevel == 2)
                {
                    chargeStart = false;
                    TestSlash(movelist.moveCC5AA, movelist.moveObjectCC5AA);
                }
            }
            else if (attackScript.combo == 0)
            {
                if (chargeCounter >= movelist.move5AAA.startUp - 1 && attackScript.startup && chargeStart && chargeLevel == 1)
                {
                    TestSlash(movelist.moveC5AAA, movelist.moveObjectC5AAA);

                }
                if (chargeCounter >= movelist.moveC5AAA.startUp - 1 && attackScript.startup && chargeStart && chargeLevel == 2)
                {
                    chargeStart = false;
                    TestSlash(movelist.moveCC5AAA, movelist.moveObjectCC5AAA);
                }
            }
        }
    }

    void PerformAction()
    {
        if (movelist.chargeWeapon && isChargeAttack)
            ChargeWeapon();

        if (inputManager.releaseQueue.Count > 0 && movelist.justFrameWeapon)
        {
            if (inputManager.releaseQueue[0] == 1)
            {
                if (attackScript.startup && chargeCounter == justFrame)
                {
                    if (attackScript.combo == 1)
                        TestSlash(movelist.moveC5A, movelist.moveObjectC5A);
                    else if (attackScript.combo == 2) TestSlash(movelist.moveC5AA, movelist.moveObjectC5AA);
                    else if (attackScript.combo == 0) TestSlash(movelist.moveC5AAA, movelist.moveObjectC5AAA);
                    inputManager.releaseQueue.RemoveAt(0);
                    chargeCounter = 0;
                    chargeStart = false;
                }
            }
        }
        if (inputManager.inputQueue.Count > 0)
        {
            chargeCounter = 0;
            chargeStart = true;
            ////////
            //JUMP//
            ////////
            if (inputManager.inputQueue[0] == 0)
            {
                if (playerMov.mov && !playerMov.recovery || attackScript.recovery && attackScript.jumpCancelable)
                {
                    if (playerMov.ground)
                    {
                        attackScript.jumpCancelable = false;
                        AttackCancel();
                        playerMov.Jump();
                        inputManager.inputQueue.RemoveAt(0);
                    }
                    else if (!playerMov.ground && playerMov.remainingJumps > 0)
                    {
                        attackScript.jumpCancelable = false;
                        AttackCancel(); playerMov.Jump();
                        inputManager.inputQueue.RemoveAt(0);
                    }
                }
            }
            ////////
            //DASH//
            ////////
            else if (inputManager.inputQueue[0] == 1)
            {
                if (playerMov.mov && !playerMov.recovery || attackScript.recovery && attackScript.jumpCancelable)
                {
                    if (playerMov.ground)
                    {
                        AttackCancel();
                       // attackScript.
                        playerMov.Dash();
                        inputManager.inputQueue.RemoveAt(0);
                    }
                    else if (!playerMov.ground && playerMov.remainingDash > 0)
                    {
                        AttackCancel(); playerMov.Dash();
                        inputManager.inputQueue.RemoveAt(0);
                    }
                }
            }
            ////////////
            //BACKDASH//
            ////////////
            else if (inputManager.inputQueue[0] == 2)
            {
                if (playerMov.mov && !playerMov.recovery || attackScript.recovery && attackScript.jumpCancelable)
                {
                    if (playerMov.ground) { AttackCancel(); playerMov.Backdash(); inputManager.inputQueue.RemoveAt(0); }
                    else if (!playerMov.ground && playerMov.remainingDash > 0) { AttackCancel(); playerMov.Dash(); inputManager.inputQueue.RemoveAt(0); }
                }
            }

            //////////////////
            //NEUTRAL ATTACK//
            //////////////////
            else if (inputManager.inputQueue[0] == 3)
            {
                if (movelist.move5A != null)
                    //COMBO
                    if (attackScript.combo > 0 && attackScript.recovery && !attackScript.startup && !attackScript.active)
                    {
                        inputManager.inputQueue.RemoveAt(0);
                        GenericAttack();
                    }
                    //CAN ATTACK
                    else if (attackScript.canAttack && !playerMov.dashing && playerStatus.stuncounter <= 0 && !playerMov.backdashing)
                    {
                        GenericAttack();
                        inputManager.inputQueue.RemoveAt(0);
                    }
            }
            /////////////
            //UP ATTACK//
            /////////////
            else if (inputManager.inputQueue[0] == 4)
            {
                if (movelist.move8A != null || movelist.move5A != null)
                    //COMBO
                    if (attackScript.combo > 0 && attackScript.canAttack && playerMov.mov ||
                   attackScript.combo > 0 && attackScript.recovery && !attackScript.startup && !attackScript.active)
                    {
                        inputManager.inputQueue.RemoveAt(0);
                        if (playerMov.ground)
                        {
                            if (movelist.move8AA != null) { TestSlash(movelist.move8AA, movelist.moveObject8AA); }
                            else if (movelist.move8A != null) TestSlash(movelist.move8A, movelist.moveObject8A);
                            else GenericAttack();
                        }
                        else
                        {
                            if (movelist.moveJ8AA != null) { TestSlash(movelist.moveJ8AA, movelist.moveObjectJ8AA); }
                            else if (movelist.moveJ8A != null && !movelist.limitJ8A) TestSlash(movelist.moveJ8A, movelist.moveObjectJ8A);
                            else if (movelist.moveJ8A != null && movelist.limitJ8A && attackScript.j8ALimit > 0)
                            {

                                print("8 combo");
                                attackScript.j8ALimit--; TestSlash(movelist.moveJ8A, movelist.moveObjectJ8A);

                            }
                            else
                            {
                                print("Combo");
                                GenericAttack();
                            }
                        }
                    }
                    //ATTACK CANCEL
                    else if (attackScript.attackCancelable && attackScript.recovery && !attackScript.startup && !attackScript.active)
                    {
                        if (playerMov.ground)
                        {
                            if (movelist.move8A == null)
                            {
                                print("Cancel");
                                GenericAttack();
                                inputManager.inputQueue.RemoveAt(0);
                            }
                            else { TestSlash(movelist.move8A, movelist.moveObject8A); inputManager.inputQueue.RemoveAt(0); }
                        }
                        else
                        {
                            if (movelist.moveJ8A == null)
                            {
                                // GenericAttack();
                            }
                            else if (movelist.moveJ8A != null && !movelist.limitJ8A) { TestSlash(movelist.moveJ8A, movelist.moveObjectJ8A); inputManager.inputQueue.RemoveAt(0); }
                            else if (movelist.moveJ8A != null && movelist.limitJ8A && attackScript.j8ALimit > 0)
                            {
                                print("8 cancel");
                                attackScript.j8ALimit--; TestSlash(movelist.moveJ8A, movelist.moveObjectJ8A);
                                inputManager.inputQueue.RemoveAt(0);
                            }
                        }
                    }
                    //CAN ATTACK
                    else if (attackScript.canAttack && playerMov.mov || playerMov.recovery && attackScript.canAttack)
                    {
                        inputManager.inputQueue.RemoveAt(0);
                        if (playerMov.ground)
                        {
                            if (movelist.move8A == null)
                            {

                                GenericAttack();
                            }
                            else TestSlash(movelist.move8A, movelist.moveObject8A);
                        }
                        else
                        {
                            if (movelist.moveJ8A != null && !movelist.limitJ8A) TestSlash(movelist.moveJ8A, movelist.moveObjectJ8A);
                            else if (movelist.moveJ8A != null && movelist.limitJ8A && attackScript.j8ALimit > 0)
                            {

                                print("8 Can attack");
                                attackScript.j8ALimit--; TestSlash(movelist.moveJ8A, movelist.moveObjectJ8A);
                            }
                            else
                            {
                                print("Can attack");
                                GenericAttack();
                            }
                        }
                    }

            }
            ///////////////
            //DOWN ATTACK//
            ///////////////           
            else if (inputManager.inputQueue[0] == 5)
            {
                if (movelist.move2A != null || movelist.move5A != null)
                    //COMBO
                    if (attackScript.combo > 0 && attackScript.canAttack && playerMov.mov ||
                 attackScript.combo > 0 && attackScript.recovery && !attackScript.startup && !attackScript.active)
                    {
                        inputManager.inputQueue.RemoveAt(0);
                        if (playerMov.ground)
                        {
                            if (movelist.move2AA != null) { TestSlash(movelist.move2AA, movelist.moveObject2AA); }
                            else if (movelist.move2A != null) TestSlash(movelist.move2A, movelist.moveObject2A);
                            else GenericAttack();
                        }
                        else
                        {
                            if (movelist.moveJ2AA != null) { TestSlash(movelist.moveJ2AA, movelist.moveObjectJ2AA); }
                            else if (movelist.moveJ2A != null) TestSlash(movelist.moveJ2A, movelist.moveObjectJ2A);
                            else GenericAttack();
                        }
                    }
                    //ATTACK CANCELABLE
                    else if (attackScript.attackCancelable && attackScript.recovery && !attackScript.startup && !attackScript.active)
                    {
                        if (playerMov.ground)
                        {
                            if (movelist.move2A == null) { }
                            else TestSlash(movelist.move2A, movelist.moveObject2A);
                        }
                        else
                        {
                            if (movelist.moveJ2A == null) { GenericAttack(); }
                            else TestSlash(movelist.moveJ2A, movelist.moveObjectJ2A);
                        }
                        inputManager.inputQueue.RemoveAt(0);
                    }
                    //CAN ATTACK
                    else if (attackScript.canAttack && playerMov.mov && !playerMov.dashing || playerMov.recovery && attackScript.canAttack)
                    {
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
            //////////////////
            //SPECIAL ATTACK//
            //////////////////
            else if (inputManager.inputQueue[0] == 6)
            {
                if (movelist.move5S != null)
                    if (attackScript.recovery && attackScript.specialCancelable || attackScript.canAttack && playerMov.mov || attackScript.canAttack && playerMov.recovery)
                    {
                        if (playerStatus.special >= movelist.move5S.SpCost)
                        {
                            TestSlash(movelist.move5S, movelist.moveObject5S);
                        }
                        inputManager.inputQueue.RemoveAt(0);
                    }
            }
            //////////////////
            //SPECIAL ATTACK//
            //////////////////
            else if (inputManager.inputQueue[0] == 7)
            {

                if (movelist.move8S != null)
                    if (attackScript.recovery && attackScript.specialCancelable || attackScript.canAttack && playerMov.mov || attackScript.canAttack && playerMov.recovery)
                    {
                        if (playerStatus.special >= movelist.move5S.SpCost)
                        {
                            TestSlash(movelist.move8S, movelist.moveObject8S);
                        }

                    }
                inputManager.inputQueue.RemoveAt(0);
            }
            //////////////////
            //SPECIAL ATTACK//
            //////////////////
            else if (inputManager.inputQueue[0] == 8)
            {
                if (movelist.move2S != null)
                    if (attackScript.recovery && attackScript.specialCancelable || attackScript.canAttack && playerMov.mov || attackScript.canAttack && playerMov.recovery)
                    {
                        if (playerStatus.special >= movelist.move2S.SpCost)
                        {
                            TestSlash(movelist.move2S, movelist.moveObject2S);
                        }

                    }
                inputManager.inputQueue.RemoveAt(0);
            }
            //////////////////
            //RUNNING ATTACK//
            //////////////////
            else if (inputManager.inputQueue[0] == 9)
            {
                if (movelist.move6A != null)
                {
                    //RUNNING
                    if (playerMov.running && attackScript.canAttack && playerMov.ground && !playerMov.dashing|| playerMov.recovery && playerMov.ground && attackScript.canAttack)
                    {
                        playerMov.DashRecovery();
                        inputManager.inputQueue.RemoveAt(0);
                        TestSlash(movelist.move6A, movelist.moveObject6A);
                    }


                    //COMBO
                    else if (attackScript.combo > 0 && attackScript.recovery && !attackScript.startup && !attackScript.active)
                    {
                        inputManager.inputQueue.RemoveAt(0);
                        GenericAttack();
                    }
                    //CAN ATTACK
                    else if (attackScript.canAttack && !playerMov.dashing && playerStatus.stuncounter <= 0 && !playerMov.backdashing)
                    {
                        GenericAttack();
                        inputManager.inputQueue.RemoveAt(0);
                    }
                }
            }

            else if (inputManager.inputQueue[0] == 9) { }
            else if (inputManager.inputQueue[0] == 10) { }
            else if (inputManager.inputQueue[0] == 11) { }
            else if (inputManager.inputQueue[0] == 12) { }

        }
    }

    public void GenericAttack()
    {
        if (attackScript.combo == 0 && playerMov.ground && movelist.move5A != null) TestSlash(movelist.move5A, movelist.moveObject5A);
        else if (attackScript.combo == 1 && playerMov.ground && movelist.move5AA != null) TestSlash(movelist.move5AA, movelist.moveObject5AA);
        else if (attackScript.combo == 2 && playerMov.ground && movelist.move5AAA != null) TestSlash(movelist.move5AAA, movelist.moveObject5AAA);
        else if (attackScript.combo == 3 && playerMov.ground && movelist.move5AAAA != null) TestSlash(movelist.move5AAAA, movelist.moveObject5AAAA);
        else if (attackScript.combo == 1 && playerMov.ground && movelist.move5AA == null) TestSlash(movelist.move5A, movelist.moveObject5A);
        else if (attackScript.combo == 2 && playerMov.ground && movelist.move5AAA == null) TestSlash(movelist.move5A, movelist.moveObject5A);
        else if (attackScript.combo == 3 && playerMov.ground && movelist.move5AAAA == null) TestSlash(movelist.move5A, movelist.moveObject5A);

        else if (attackScript.combo == 0 && !playerMov.ground && movelist.moveJ5A != null) TestSlash(movelist.moveJ5A, movelist.moveObjectJ5A);
        else if (attackScript.combo == 1 && !playerMov.ground && movelist.moveJ5AA != null) TestSlash(movelist.moveJ5AA, movelist.moveObjectJ5AA);
        else if (attackScript.combo == 2 && !playerMov.ground && movelist.moveJ5AAA != null) TestSlash(movelist.moveJ5AAA, movelist.moveObjectJ5AAA);
        else if (attackScript.combo == 3 && !playerMov.ground && movelist.moveJ5AAAA != null) TestSlash(movelist.moveJ5AAAA, movelist.moveObjectJ5AAAA);
        else if (attackScript.combo == 1 && !playerMov.ground && movelist.moveJ5AA == null) TestSlash(movelist.moveJ5A, movelist.moveObjectJ5A);
        else if (attackScript.combo == 2 && !playerMov.ground && movelist.moveJ5AAA == null) TestSlash(movelist.moveJ5A, movelist.moveObjectJ5A);
        else if (attackScript.combo == 3 && !playerMov.ground && movelist.moveJ5AAAA == null) TestSlash(movelist.moveJ5A, movelist.moveObjectJ5A);
    }

    public void ExtraMove(int extraID)
    {
        if (extraID == 1) TestSlash(movelist.moveExtra, movelist.moveObjectExtra);
        if (extraID == 2) TestSlash(movelist.moveExtra2, movelist.moveObjectExtra2);
    }

    void TestSlash(Move move, GameObject moveObject)
    {
        if (inputManager.inputHorizontal < 0) transform.parent.localScale = new Vector3(-1, 1, 1);
        else if (inputManager.inputHorizontal > 0) transform.parent.localScale = new Vector3(1, 1, 1);

        playerStatus.Recover();
        isChargeAttack = move.isChargeAttack;
        playerMov.doubleTap = false;
        AttackCancel();
        print(move);
        attackScript.resetCounter = 0;
        attackScript.attackID = move.ID;
        attackScript.canAttack = false;
        attackScript.attackStart = Time.time;

        playerStatus.special -= move.SpCost;

        attackScript.combo = move.combo;
        attackScript.hasIFrames = move.iFrames;
        attackScript.hasInvul = move.invul;
        attackScript.noClip = move.noClip;

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
        attackScript.landAttackFrames = move.landAttackFrames;
        attackScript.specialCancelable = move.specialCancelable;
        attackScript.jumpCancelable = move.jumpCancelable;
        attackScript.keepVerticalVel = move.keepVerticalVel;
        attackScript.keepHorizontalVel = move.keepHorizontalVel;
        attackScript.canMove = move.canMove;
        attackScript.landCancel = move.landCancel;
        attackScript.landCancelRecovery = move.landCancelRecovery;
        attackScript.attackCancelable = move.attackCancelable;
        attackScript.interpolate = move.interpolate;
        if (attackScript.interpolate)
        {
            attackScript.interpol1 = move.duration1;
            attackScript.interpol2 = move.duration2;
            attackScript.interpol3 = move.duration3;
        }

        if (movelist.justFrameWeapon) { justFrame = move.justFrameTiming; }
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

        playerMov.backdashDuration = movelist.backdashDuration;
        playerMov.backdashSpeed = movelist.backdashSpeed;
        playerMov.backdashRecovery = movelist.backdashRecovery;
    }
}
