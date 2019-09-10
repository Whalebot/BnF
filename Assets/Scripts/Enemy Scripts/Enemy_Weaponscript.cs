﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Weaponscript : MonoBehaviour
{
    public int behaviorID = 1;
    public bool isBoss = false;
    Movelist movelist;
    [HeaderAttribute("Moveset attributes")]
    Enemy_AttackScript attackScript;
    EnemyScript enemyScript;
    Enemy_Movement enemyMov;

    [HeaderAttribute("Attack attributes")]
    public GameObject currentAttack;
    public int attackID;

    public int attackDelay;

    public int maxDelay;
    int attackDelayCounter;

    public float comboDelay;
    public bool comboStart;

    public float distance;
    public float distance2;

    bool hasIFrames;
    public bool withinRange;
    public bool withinRange2;

    [HeaderAttribute("Frame attributes")]

    bool startupMov;
    bool activeMov;
    bool recoveryMov;

    bool setStartVelocity;
    bool setEndVelocity;
    Vector3 startVelocity;

    public int RNGCount;

    private float forward;
    private float forward2;
    float up;
    float up2;
    float momentumDuration1;
    float momentumDuration2;
    float momentumDuration3;
    bool phase2Trigger;

    public List<int> attackQueue;

    void Awake()
    {
        movelist = GetComponent<Movelist>();
        enemyMov = GetComponentInParent<Enemy_Movement>();
        enemyScript = GetComponentInParent<EnemyScript>();
        attackScript = GetComponentInParent<Enemy_AttackScript>();
    }

    void Start()
    {
        //   attackDelayCounter = attackDelay;

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
            if (Input.GetKeyDown("t")) { enemyMov.direction = -attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move8A, movelist.moveObject8A); }
            if (Input.GetKeyDown("g")) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.moveJ2A, movelist.moveObjectJ2A); }
            if (enemyScript.stun) attackQueue.Clear();
            // if (attackScript.recovery) attackDelayCounter = 0;

            PerformAction();
            attackScript.resetCounter++;
            if (enemyMov.target != null)
            {
                if ((transform.position - enemyMov.target.transform.position).magnitude < distance) withinRange = true;
                else withinRange = false;
                if ((transform.position - enemyMov.target.transform.position).magnitude < distance2) withinRange2 = true;
                else withinRange2 = false;
            }

            if (comboDelay < attackScript.resetCounter) attackScript.combo = 0;
        }
    }

    void PerformAction()
    {
        if (attackScript.canAttack) attackDelayCounter--;
        if (enemyScript.detected && enemyScript.requiresTrigger || !enemyScript.requiresTrigger)
            if (isBoss)
            {
                if (behaviorID == 1) BossGeese();
                if (behaviorID == 2) BossBamboo();
                if (behaviorID == 3) BossLilac();
                if (behaviorID == 10) BossHydrangea();
            }
            else
            {
                if (attackQueue.Count == 0)
                {
                    if (behaviorID == 1) SwordEnemy();
                    else if (behaviorID == 2) SpearEnemy();
                    else if (behaviorID == 3) TenguEnemy();
                    else if (behaviorID == 4) RangedTengu();
                    else if (behaviorID == 10) PhysicsTest();
                }
            }

        if (attackQueue.Count > 0 && attackScript.canAttack || attackScript.recovery && attackQueue.Count > 0)
        {
            if (attackQueue[0] == 1) { TestSlash(movelist.move5A, movelist.moveObject5A); attackQueue.RemoveAt(0); }
            else if (attackQueue[0] == 2) { TestSlash(movelist.move5AA, movelist.moveObject5AA); attackQueue.RemoveAt(0); }
            else if (attackQueue[0] == 3) { TestSlash(movelist.move5AAA, movelist.moveObject5AAA); attackQueue.RemoveAt(0); }
            else if (attackQueue[0] == 4) { TestSlash(movelist.move2A, movelist.moveObject2A); attackQueue.RemoveAt(0); }
            else if (attackQueue[0] == 5) { TestSlash(movelist.move8A, movelist.moveObject8A); attackQueue.RemoveAt(0); }
            else if (attackQueue[0] == 0) { TestSlash(movelist.move5S, movelist.moveObject5S); attackQueue.RemoveAt(0); }
        }
    }

    void BossGeese()
    {
        if (attackScript.canAttack || attackScript.recovery)
        {
            //GROUND
            if (enemyMov.ground)
            {
                //RANGE CHECK
                if (!enemyScript.stun && withinRange && attackDelayCounter <= 0 && attackQueue.Count == 0)
                {
                    //ATTACK RNG
                    RNGCount = Random.Range(1, 8);
                    if (RNGCount == 1 && enemyMov.ground && !enemyScript.stun)
                    {
                        enemyMov.direction = attackScript.trueDirection;
                        attackDelayCounter = Random.Range(attackDelay, maxDelay);
                        attackQueue.Add(0);
                    }
                    else if (RNGCount >= 2 && !enemyScript.stun)
                    {
                        if (attackScript.target.transform.position.y > transform.position.y && enemyMov.ground) enemyMov.Jump();

                        if (attackScript.combo == 0)
                        {
                            attackScript.tracking = true;
                            attackQueue.Add(1);
                        }
                        else if (attackScript.combo == 1)
                        {
                            attackQueue.Add(2);
                        }
                        else if (attackScript.combo == 2)
                        {
                            attackDelayCounter = maxDelay;
                            attackScript.tracking = true;
                            attackQueue.Add(3);
                        }
                    }
                }
                else if (!enemyScript.stun && withinRange2 && !withinRange && attackDelayCounter <= 0 && attackQueue.Count == 0)
                {
                    attackScript.tracking = true;
                    attackDelayCounter = Random.Range(attackDelay, maxDelay);
                    attackQueue.Add(0);
                }
            }
            //AIRBORNE
            else { }
        }
    }

    void BossLilac()
    {
        if (attackScript.canAttack && attackDelayCounter <= 0 && withinRange2 && !withinRange && !enemyScript.stun)
        {
            enemyMov.direction = attackScript.trueDirection;
            RNGCount = Random.Range(1, 6);
            if (RNGCount == 1)
            {
                attackQueue.Add(1);
                attackDelayCounter = attackDelay;
            }
            else if (RNGCount == 2)
            {
                attackQueue.Add(2);
                attackDelayCounter = attackDelay;
            }
            else if (RNGCount == 3)
            {
                attackQueue.Add(3);
                attackDelayCounter = attackDelay;
            }
            else if (RNGCount == 4)
            {
                attackQueue.Add(4);
                attackDelayCounter = attackDelay;
            }
            else if (RNGCount == 5)
            {
                attackQueue.Add(5);
                attackDelayCounter = attackDelay;
            }

        }
        else if (attackScript.canAttack && attackDelayCounter <= 0 && withinRange && !enemyScript.stun)
        {
            enemyMov.direction = attackScript.trueDirection;

            attackQueue.Add(0);
            attackQueue.Add(1);
            attackDelayCounter = maxDelay;

        }
    }

    void BossBamboo()
    {
        if (enemyScript.mode == 1)
        {
            if (!enemyScript.stun && withinRange && attackScript.canAttack && attackDelayCounter <= 0 && enemyMov.ground)
            {
                RNGCount = Random.Range(1, 16);
                if (RNGCount >= 1 && RNGCount <= 4 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move5S, movelist.moveObject5S); }
                if (RNGCount == 5 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move8A, movelist.moveObject8A); }
                if (RNGCount == 6 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move2A, movelist.moveObject2A); }
                if (RNGCount == 7 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move5S, movelist.moveObject5S); }
                if (RNGCount == 8 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move6A, movelist.moveObject6A); }
                if (RNGCount == 9 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.moveJ2A, movelist.moveObjectJ2A); }
                if (RNGCount >= 10 && !enemyScript.stun)
                {
                    if (attackScript.combo == 0)
                    {
                        enemyMov.direction = attackScript.trueDirection;
                        TestSlash(movelist.move5A, movelist.moveObject5A);
                    }
                    else if (attackScript.combo == 1)
                    {
                        enemyMov.direction = attackScript.trueDirection;
                        TestSlash(movelist.move5AA, movelist.moveObject5AA);
                    }
                    else if (attackScript.combo == 2)
                    {
                        attackDelayCounter = maxDelay;
                        enemyMov.direction = attackScript.trueDirection;
                        TestSlash(movelist.move5AAA, movelist.moveObject5AAA);
                    }
                }
            }
            else if (!enemyScript.stun && withinRange2 && !withinRange && attackScript.canAttack && attackDelayCounter <= 0)
            {
                RNGCount = Random.Range(1, 6);
                if (RNGCount == 1) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move6A, movelist.moveObject6A); }
                //      else if (RNGCount == 2) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move5SS, movelist.moveObject5SS); attackScript.tornado = true; }
                else { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(30, attackDelay); TestSlash(movelist.move2A, movelist.moveObject2A); }
            }
        }

        else if (enemyScript.mode == 2)
        {
            if (!phase2Trigger && enemyMov.ground && !enemyScript.stun)
            {
                phase2Trigger = true;
                enemyMov.direction = attackScript.trueDirection; attackDelayCounter = maxDelay; TestSlash(movelist.move5SS, movelist.moveObject5SS); attackScript.tornado = true;
            }
            else if (!enemyScript.stun && withinRange && attackScript.canAttack && attackDelayCounter <= 0 && enemyMov.ground)
            {
                RNGCount = Random.Range(1, 12);
                if (RNGCount == 1 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move5S, movelist.moveObject5S); }
                if (RNGCount == 2 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = -attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move8A, movelist.moveObject8A); }
                if (RNGCount == 3 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move2A, movelist.moveObject2A); }
                if (RNGCount == 4 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move5SS, movelist.moveObject5SS); attackScript.tornado = true; }
                if (RNGCount == 5 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move6A, movelist.moveObject6A); }
                if (RNGCount == 6 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.moveJ2A, movelist.moveObjectJ2A); }
                if (RNGCount == 7 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move8A, movelist.moveObject8A); }
                if (RNGCount >= 8 && !enemyScript.stun)
                {
                    //  if (attackScript.target.transform.position.y > transform.position.y && enemyMov.ground) enemyMov.Jump();
                    if (attackScript.combo == 0)
                    {
                        enemyMov.direction = attackScript.trueDirection;
                        TestSlash(movelist.move5A, movelist.moveObject5A);
                    }
                    else if (attackScript.combo == 1)
                    {
                        enemyMov.direction = attackScript.trueDirection;
                        TestSlash(movelist.move5AA, movelist.moveObject5AA);
                    }
                    else if (attackScript.combo == 2)
                    {
                        attackDelayCounter = maxDelay;
                        enemyMov.direction = attackScript.trueDirection;
                        TestSlash(movelist.move5AAA, movelist.moveObject5AAA);
                    }
                }
            }
            else if (!enemyScript.stun && withinRange2 && !withinRange && attackScript.canAttack && attackDelayCounter <= 0)
            {
                RNGCount = Random.Range(1, 6);
                if (RNGCount == 1) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move6A, movelist.moveObject6A); }
                else if (RNGCount == 2) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = maxDelay; TestSlash(movelist.move5SS, movelist.moveObject5SS); attackScript.tornado = true; }
                else { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(30, attackDelay); TestSlash(movelist.move2A, movelist.moveObject2A); }
            }
        }
    }

    void BossHydrangea()
    {
        if (enemyScript.mode == 1)
        {
            if (!enemyScript.stun && withinRange && attackScript.canAttack && attackDelayCounter <= 0 && enemyMov.ground)
            {
                RNGCount = Random.Range(1, 16);
                if (RNGCount >= 1 && RNGCount <= 2 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move5S, movelist.moveObject5S); }
                if (RNGCount == 3 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move8A, movelist.moveObject8A); }
                if (RNGCount == 4 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move5S, movelist.moveObject5S); }
                if (RNGCount == 5 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move5S, movelist.moveObject5S); }
                if (RNGCount == 6 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move6A, movelist.moveObject6A); }
                if (RNGCount == 7 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.moveJ2A, movelist.moveObjectJ2A); }
                if (RNGCount >= 8 && !enemyScript.stun)
                {
                    if (attackScript.combo == 0)
                    {
                        enemyMov.direction = attackScript.trueDirection;
                        TestSlash(movelist.move5A, movelist.moveObject5A);
                    }
                    else if (attackScript.combo == 1)
                    {
                        enemyMov.direction = attackScript.trueDirection;
                        TestSlash(movelist.move5AA, movelist.moveObject5AA);
                    }
                    else if (attackScript.combo == 2)
                    {
                        attackDelayCounter = maxDelay;
                        enemyMov.direction = attackScript.trueDirection;
                        TestSlash(movelist.move5AAA, movelist.moveObject5AAA);
                    }
                }
            }
            else if (!enemyScript.stun && withinRange2 && !withinRange && attackScript.canAttack && attackDelayCounter <= 0)
            {
                RNGCount = Random.Range(1, 6);
                //    if (RNGCount == 1) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move6A, movelist.moveObject6A); }
                //      else if (RNGCount == 2) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move5SS, movelist.moveObject5SS); attackScript.tornado = true; }
                //   else { enemyMov.direction = attackScript.trueDirection; TestSlash(movelist.move5S, movelist.moveObject5S); }
            }
        }

        else if (enemyScript.mode == 2)
        {
            if (!phase2Trigger && enemyMov.ground && !enemyScript.stun)
            {
                phase2Trigger = true;
                enemyMov.direction = attackScript.trueDirection; attackDelayCounter = maxDelay; TestSlash(movelist.move5SS, movelist.moveObject5SS); attackScript.tornado = true;
            }
            else if (!enemyScript.stun && withinRange && attackScript.canAttack && attackDelayCounter <= 0 && enemyMov.ground)
            {
                RNGCount = Random.Range(1, 12);
                if (RNGCount == 1 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move5S, movelist.moveObject5S); }
                if (RNGCount == 2 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = -attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move8A, movelist.moveObject8A); }
                if (RNGCount == 3 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move2A, movelist.moveObject2A); }
                if (RNGCount == 4 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move5SS, movelist.moveObject5SS); attackScript.tornado = true; }
                if (RNGCount == 5 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move6A, movelist.moveObject6A); }
                if (RNGCount == 6 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.moveJ2A, movelist.moveObjectJ2A); }
                if (RNGCount == 7 && enemyMov.ground && !enemyScript.stun) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move8A, movelist.moveObject8A); }
                if (RNGCount >= 8 && !enemyScript.stun)
                {
                    //  if (attackScript.target.transform.position.y > transform.position.y && enemyMov.ground) enemyMov.Jump();
                    if (attackScript.combo == 0)
                    {
                        enemyMov.direction = attackScript.trueDirection;
                        TestSlash(movelist.move5A, movelist.moveObject5A);
                    }
                    else if (attackScript.combo == 1)
                    {
                        enemyMov.direction = attackScript.trueDirection;
                        TestSlash(movelist.move5AA, movelist.moveObject5AA);
                    }
                    else if (attackScript.combo == 2)
                    {
                        attackDelayCounter = maxDelay;
                        enemyMov.direction = attackScript.trueDirection;
                        TestSlash(movelist.move5AAA, movelist.moveObject5AAA);
                    }
                }
            }
            else if (!enemyScript.stun && withinRange2 && !withinRange && attackScript.canAttack && attackDelayCounter <= 0)
            {
                RNGCount = Random.Range(1, 6);
                if (RNGCount == 1) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move6A, movelist.moveObject6A); }
                else if (RNGCount == 2) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = maxDelay; TestSlash(movelist.move5SS, movelist.moveObject5SS); attackScript.tornado = true; }
                else { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(30, attackDelay); TestSlash(movelist.move2A, movelist.moveObject2A); }
            }
        }
    }

    void SwordEnemy()
    {
        if (attackScript.canAttack && attackDelayCounter <= 0 && (transform.position - attackScript.target.transform.position).magnitude < distance && !enemyScript.stun)
        {

            enemyMov.direction = attackScript.trueDirection;
            attackDelayCounter = 0;
            if (attackScript.targetIsAirborne) enemyMov.Jump();
            attackQueue.Add(1);
        }
    }

    void SpearEnemy()
    {
        if (attackScript.target != null)
            if (attackScript.canAttack && attackDelayCounter <= 0 && (transform.position - attackScript.target.transform.position).magnitude < distance && !enemyScript.stun)
            {
                enemyMov.direction = attackScript.trueDirection;
                attackDelayCounter = 0;
                attackQueue.Add(1);
            }
    }

    void RangedTengu()
    {
        if (attackScript.canAttack && attackDelayCounter <= 0 && withinRange2 && !withinRange && !enemyScript.stun)
        {
            enemyMov.direction = attackScript.trueDirection;
            RNGCount = Random.Range(1, 3);
            if (RNGCount == 1)
            {
                attackQueue.Add(1);
                attackDelayCounter = attackDelay;
            }
            else if (RNGCount == 2)
            {
                attackQueue.Add(2);
                attackDelayCounter = attackDelay;
            }
        }
        else if (attackScript.canAttack && attackDelayCounter <= 0 && withinRange && !enemyScript.stun)
        {
            enemyMov.direction = attackScript.trueDirection;

            attackQueue.Add(0);
            attackQueue.Add(1);
            attackDelayCounter = maxDelay;

        }
    }

    void TenguEnemy()
    {
        if (attackScript.canAttack && attackDelayCounter <= 0 && (transform.position - attackScript.target.transform.position).magnitude < distance && !enemyScript.stun)
        {
            enemyMov.direction = attackScript.trueDirection;
            attackDelayCounter = 0;
            RNGCount = Random.Range(1, 3);
            if (RNGCount == 1)
            {
                attackQueue.Add(4);
                attackQueue.Add(5);
                attackDelayCounter = attackDelay;
            }
            else if (RNGCount == 2)
            {
                attackQueue.Add(0);
                attackQueue.Add(1);
                attackQueue.Add(2);
                attackDelayCounter = maxDelay;
            }

        }
    }


    void PhysicsTest()
    {
        if (Input.GetKeyDown("n"))
        {

            enemyMov.direction = attackScript.trueDirection;
            attackDelayCounter = 0;

            TestSlash(movelist.move5A, movelist.moveObject5A);
        }
        if (Input.GetKeyDown("m")) { enemyMov.Jump(); }
    }


    public void Dash() { if (!enemyMov.dashing) enemyMov.Dash(); }

    public void GenericAttack()
    {
        attackScript.resetCounter = 0;
        if (attackScript.combo == 0 && enemyMov.ground) TestSlash(movelist.move5A, movelist.moveObject5A);
        else if (attackScript.combo == 1 && enemyMov.ground) TestSlash(movelist.move5AA, movelist.moveObject5AA);
        else if (attackScript.combo == 2 && enemyMov.ground) TestSlash(movelist.move5AAA, movelist.moveObject5AAA);
        else if (attackScript.combo == 3 && enemyMov.ground) TestSlash(movelist.move5AAAA, movelist.moveObject5AAAA);

        else if (attackScript.combo == 0 && !enemyMov.ground) TestSlash(movelist.moveJ5A, movelist.moveObjectJ5A);
        else if (attackScript.combo == 1 && !enemyMov.ground) TestSlash(movelist.moveJ5AA, movelist.moveObjectJ5AA);
        else if (attackScript.combo == 2 && !enemyMov.ground) TestSlash(movelist.moveJ5AAA, movelist.moveObjectJ5AAA);
        else if (attackScript.combo == 3 && !enemyMov.ground) TestSlash(movelist.moveJ5AAAA, movelist.moveObjectJ5AAAA);
    }

    public void ExtraMove()
    {
        enemyMov.direction = attackScript.trueDirection;
        TestSlash(movelist.moveExtra, movelist.moveObjectExtra);
    }

    void TestSlash(Move move, GameObject moveObject)
    {
        AttackCancel();
        print(move);
        if (move.startupSound != null) Instantiate(move.startupSound);

        attackScript.attackID = move.ID;
        attackScript.canAttack = false;


        enemyScript.special -= move.SpCost;

        attackScript.resetCounter = 0;
        attackScript.combo = move.combo;

        attackScript.hasIFrames = move.iFrames;
        attackScript.hasInvul = move.invul;


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
        attackScript.keepHorizontalVel = move.keepHorizontalVel;
        attackScript.canMove = move.canMove;
        attackScript.keepVel = move.keepVel;
        attackScript.landCancel = move.landCancel;
        attackScript.landCancelRecovery = move.landCancelRecovery;
        attackScript.attackCancelable = move.attackCancelable;
        attackScript.interpolate = move.interpolate;
        attackScript.homing = move.isHoming;

        if (attackScript.interpolate)
        {
            attackScript.interpol1 = move.duration1;
            attackScript.interpol2 = move.duration2;
            attackScript.interpol3 = move.duration3;
        }

        attackScript.startup = true;
        attackScript.startupMov = true;
    }



    public void Momentum(Vector2 vel)
    {
        enemyMov.mov = false;
        enemyMov.AddVelocity(vel);
    }

    public void AttackCancel()
    {
        DisableObjects();
        attackScript.startup = false;
        attackScript.active = false;
        attackScript.recovery = false;
        enemyMov.mov = true;
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
        enemyMov.dashDuration = movelist.dashDuration;
        enemyMov.dashSpeed = movelist.dashSpeed;
        enemyMov.dashRecovery = movelist.dashRecovery;
        enemyMov.currentRecovery = movelist.dashRecovery;
        enemyMov.currentDuration = movelist.dashDuration;
    }
}
