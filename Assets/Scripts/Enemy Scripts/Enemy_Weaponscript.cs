using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Weaponscript : MonoBehaviour
{
    public int behaviorID = 1;
    public bool isBoss = false;

    AttackAI ai;
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

    bool targetAirborne;

    public List<int> attackQueue;
    public List<bool> homingQueue;



    void Awake()
    {
        movelist = GetComponent<Movelist>();
        enemyMov = GetComponentInParent<Enemy_Movement>();
        enemyScript = GetComponentInParent<EnemyScript>();
        attackScript = GetComponentInParent<Enemy_AttackScript>();
        ai = GetComponent<AttackAI>();
    }

    void Start()
    {


    }
    void OnEnable()
    {
      //  attackScript.keepVel = false;
        UpdateDash();
    }

    void RangeDetection()
    {
        if (ai.groundToGround.ranges.Count > 0)
            for (int i = 0; i < ai.groundToGround.ranges.Count; i++)
            {
                ai.groundToGround.withinRanges[i] = (transform.position - enemyMov.target.transform.position).magnitude < ai.groundToGround.ranges[i];
            }
        if (ai.groundToAir.ranges.Count > 0)
            for (int i = 0; i < ai.groundToAir.ranges.Count; i++)
            {
                ai.groundToAir.withinRanges[i] = (transform.position - enemyMov.target.transform.position).magnitude < ai.groundToAir.ranges[i];
            }
        if (ai.airToGround.ranges.Count > 0)
            for (int i = 0; i < ai.airToGround.ranges.Count; i++)
            {
                ai.airToGround.withinRanges[i] = (transform.position - enemyMov.target.transform.position).magnitude < ai.airToGround.ranges[i];
            }
        if (ai.airToAir.ranges.Count > 0)
            for (int i = 0; i < ai.airToAir.ranges.Count; i++)
            {
                ai.airToAir.withinRanges[i] = (transform.position - enemyMov.target.transform.position).magnitude < ai.airToAir.ranges[i];
            }
    }
    void FixedUpdate()
    {
        if (!HitStopScript.hitStop)
        {
            PhysicsTest();

            if (Input.GetKeyDown("t")) { enemyMov.direction = -attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.move8A, movelist.moveObject8A); }
            if (Input.GetKeyDown("g")) { enemyMov.direction = attackScript.trueDirection; attackDelayCounter = Random.Range(attackDelay, maxDelay); TestSlash(movelist.moveJ2A, movelist.moveObjectJ2A); }
            if (enemyScript.stun) attackQueue.Clear();
            // if (attackScript.recovery) attackDelayCounter = 0;

            RangeDetection();

            PerformAction();
            attackScript.resetCounter++;
            if (enemyMov.target != null)
            {
                if ((transform.position - enemyMov.target.transform.position).magnitude < distance) withinRange = true;
                else withinRange = false;
                if ((transform.position - enemyMov.target.transform.position).magnitude < distance2) withinRange2 = true;
                else withinRange2 = false;

                targetAirborne = enemyMov.target.transform.position.y > transform.position.y;
            }

            if (!targetAirborne && enemyMov.ground) ai.state = 0;
            else if (targetAirborne && enemyMov.ground) ai.state = 1;
            else if (!targetAirborne && !enemyMov.ground) ai.state = 2;
            else if (targetAirborne && !enemyMov.ground) ai.state = 3;
        }
    }

    void PerformAction()
    {
        if (attackScript.canAttack) attackDelayCounter--;
        if (enemyScript.detected && enemyScript.requiresTrigger || !enemyScript.requiresTrigger)
            if (isBoss)
            {
                if (behaviorID == 0) Boss();
                if (behaviorID == 1) BossGeese();
                if (behaviorID == 2) BossBamboo();
                if (behaviorID == 3) BossLilac();
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
            switch (attackQueue[0])
            {

                case 50: TestSlash(movelist.move5A, movelist.moveObject5A); return;
                case 51: TestSlash(movelist.move5AA, movelist.moveObject5AA); return;
                case 52: TestSlash(movelist.move5AAA, movelist.moveObject5AAA); return;
                case 53: TestSlash(movelist.move5AAAA, movelist.moveObject5AAA); return;

                case 20: TestSlash(movelist.move2A, movelist.moveObject2A); return;
                case 21: TestSlash(movelist.move2AA, movelist.moveObject2AA); return;

                case 80: TestSlash(movelist.move8A, movelist.moveObject8A); return;
                case 81: TestSlash(movelist.move8AA, movelist.moveObject8A); return;

                case 60: TestSlash(movelist.move6A, movelist.moveObject6A); return;


                case 150: TestSlash(movelist.moveJ5A, movelist.moveObjectJ5A); return;
                case 151: TestSlash(movelist.moveJ5AA, movelist.moveObjectJ5AA); return;
                case 152: TestSlash(movelist.moveJ5AAA, movelist.moveObjectJ5AAA); return;
                case 153: TestSlash(movelist.moveJ5AAAA, movelist.moveObjectJ5AAAA); return;

                case 120: TestSlash(movelist.moveJ2A, movelist.moveObjectJ2A); return;
                case 121: TestSlash(movelist.moveJ2AA, movelist.moveObjectJ2AA); return;

                case 180: TestSlash(movelist.moveJ8A, movelist.moveObjectJ8A); return;
                case 181: TestSlash(movelist.moveJ8AA, movelist.moveObjectJ8AA); return;

                case 160: TestSlash(movelist.moveJ5A, movelist.moveObjectJ5A); return;

                case 250: TestSlash(movelist.move5S, movelist.moveObject5S); return;
                case 251: TestSlash(movelist.move5S, movelist.moveObject5SS); return;
                case 252: TestSlash(movelist.move5SSS, movelist.moveObject5SSS); return;

                case 280: TestSlash(movelist.move8S, movelist.moveObject8S); return;
                case 220: TestSlash(movelist.move2S, movelist.moveObject2S); return;

                case 200: TestSlash(movelist.moveExtra, movelist.moveObjectExtra); return;
                case 201: TestSlash(movelist.moveExtra2, movelist.moveObjectExtra2); return;

                default: return;
            }

        }
    }



    void CheckMove(MoveProperties.Attack attack)
    {

        if (attack == MoveProperties.Attack.g5S)
        {
            attackQueue.Add(250);
        }
        else if (attack == MoveProperties.Attack.g5SS) { attackQueue.Add(251); }
        else if (attack == MoveProperties.Attack.g5SSS) { attackQueue.Add(252); }
        else if (attack == MoveProperties.Attack.g8S) { attackQueue.Add(280); }
        else if (attack == MoveProperties.Attack.g2S) { attackQueue.Add(220); }
        else if (attack == MoveProperties.Attack.extra1) { attackQueue.Add(200); }
        else if (attack == MoveProperties.Attack.extra2) { attackQueue.Add(201); }
        else if (attack == MoveProperties.Attack.g5A) { attackQueue.Add(50); }
        else if (attack == MoveProperties.Attack.g5AA) { attackQueue.Add(51); }
        else if (attack == MoveProperties.Attack.g5AAA) { attackQueue.Add(52); }
        else if (attack == MoveProperties.Attack.g5AAAA) { attackQueue.Add(53); }
        else if (attack == MoveProperties.Attack.g8A) { attackQueue.Add(80); }
        else if (attack == MoveProperties.Attack.g8AA) { attackQueue.Add(81); }
        else if (attack == MoveProperties.Attack.g2A) { attackQueue.Add(20); }
        else if (attack == MoveProperties.Attack.g2AA) { attackQueue.Add(21); }
        else if (attack == MoveProperties.Attack.g6A) { attackQueue.Add(60); }
        else if (attack == MoveProperties.Attack.j5A) { attackQueue.Add(150); }
        else if (attack == MoveProperties.Attack.j5AA) { attackQueue.Add(151); }
        else if (attack == MoveProperties.Attack.j5AAA) { attackQueue.Add(152); }
        else if (attack == MoveProperties.Attack.j5AAAA) { attackQueue.Add(153); }
        else if (attack == MoveProperties.Attack.j8A) { attackQueue.Add(180); }
        else if (attack == MoveProperties.Attack.j8AA) { attackQueue.Add(181); }
        else if (attack == MoveProperties.Attack.j2A) { attackQueue.Add(120); }
        else if (attack == MoveProperties.Attack.j2AA) { attackQueue.Add(121); }
        else if (attack == MoveProperties.Attack.j6A) { attackQueue.Add(160); }
        else if (attack == MoveProperties.Attack.jump) { enemyMov.Jump(); attackDelayCounter = 5;  print("Jump"); }
    }

    void ResetHoming()
    {
        homingQueue.RemoveAt(0);

    }

    void Boss()
    {

        if (attackScript.canAttack || attackScript.recovery)
        {
            if (!enemyScript.stun && attackDelayCounter <= 0 && attackQueue.Count == 0)
            {
                switch (ai.state)
                {

                    case 3: AirToAir(); return;
                    //case 2: GroundToGround(); return;
                    case 1: GroundToAir(); return;
                    case 0: GroundToGround(); return;
                    default: GroundToGround(); return;
                }

            }
        }
    }

    void GroundToGround()
    {
        int tempRangeCounter = 0;
        int tempRNGCounter = 0;

        //FOR EACH RANGE
        for (int i = 0; i < ai.groundToGround.withinRanges.Count; i++)
        {
            //CHECK IF WITHIN RANGE
            if (ai.groundToGround.withinRanges[i])
            {
                RNGCount = Random.Range(0, ai.groundToGroundRNG[i]);
                print(RNGCount);
                for (int j = 0; j < ai.groundToGround.moveSequences.Count; j++)
                {
                    if (ai.groundToGround.moveSequences[j].rangeID == i)
                    {
                        tempRangeCounter++;

                        if (RNGCount <= ai.groundToGround.moveSequences[j].RNGWeight + tempRNGCounter && RNGCount >= tempRNGCounter)
                        {
                            for (int k = 0; k < ai.groundToGround.moveSequences[j].moves.Count; k++)
                            {
                                attackDelayCounter = attackDelay;
                                CheckMove(ai.groundToGround.moveSequences[j].moves[k].attack);
                                if (ai.groundToGround.moveSequences[j].moves[k].isTracking) homingQueue.Add(true);
                                else homingQueue.Add(false);
                            }
                            return;
                        }
                        else { tempRNGCounter += ai.groundToGround.moveSequences[j].RNGWeight; }
                    }
                }
            }
        }
    }

    void GroundToAir()
    {
        int tempRangeCounter = 0;
        int tempRNGCounter = 0;

        //FOR EACH RANGE
        for (int i = 0; i < ai.groundToAir.withinRanges.Count; i++)
        {
            //CHECK IF WITHIN RANGE
            if (ai.groundToAir.withinRanges[i])
            {
                RNGCount = Random.Range(0, ai.groundToAirRNG[i]);
                //print(RNGCount);
                for (int j = 0; j < ai.groundToAir.moveSequences.Count; j++)
                {
                    if (ai.groundToAir.moveSequences[j].rangeID == i)
                    {
                        tempRangeCounter++;

                        if (RNGCount <= ai.groundToAir.moveSequences[j].RNGWeight + tempRNGCounter && RNGCount >= tempRNGCounter)
                        {
                            for (int k = 0; k < ai.groundToAir.moveSequences[j].moves.Count; k++)
                            {
                                attackDelayCounter = 60;
                                CheckMove(ai.groundToAir.moveSequences[j].moves[k].attack);
                                if (ai.groundToAir.moveSequences[j].moves[k].isTracking) homingQueue.Add(true);
                                else homingQueue.Add(false);
                            }
                            return;
                        }
                        else { tempRNGCounter += ai.groundToAir.moveSequences[j].RNGWeight; }
                    }
                }
            }
        }
    }

    void AirToGround()
    {
        int tempRangeCounter = 0;
        int tempRNGCounter = 0;

        //FOR EACH RANGE
        for (int i = 0; i < ai.airToGround.withinRanges.Count; i++)
        {
            //CHECK IF WITHIN RANGE
            if (ai.airToGround.withinRanges[i])
            {
                RNGCount = Random.Range(0, ai.airToGroundRNG[i]);
                print(RNGCount);
                for (int j = 0; j < ai.airToGround.moveSequences.Count; j++)
                {
                    if (ai.airToGround.moveSequences[j].rangeID == i)
                    {
                        tempRangeCounter++;

                        if (RNGCount <= ai.airToGround.moveSequences[j].RNGWeight + tempRNGCounter && RNGCount >= tempRNGCounter)
                        {
                            for (int k = 0; k < ai.airToGround.moveSequences[j].moves.Count; k++)
                            {
                                attackDelayCounter = 60;
                                CheckMove(ai.airToGround.moveSequences[j].moves[k].attack);
                                if (ai.airToGround.moveSequences[j].moves[k].isTracking) homingQueue.Add(true);
                                else homingQueue.Add(false);
                            }
                            return;
                        }
                        else { tempRNGCounter += ai.airToGround.moveSequences[j].RNGWeight; }
                    }
                }
            }
        }
    }

    void AirToAir()
    {
        int tempRangeCounter = 0;
        int tempRNGCounter = 0;

        //FOR EACH RANGE
        for (int i = 0; i < ai.airToAir.withinRanges.Count; i++)
        {
            //CHECK IF WITHIN RANGE
            if (ai.airToAir.withinRanges[i])
            {
                RNGCount = Random.Range(0, ai.airToAirRNG[i]);
                print(RNGCount);
                for (int j = 0; j < ai.airToAir.moveSequences.Count; j++)
                {
                    if (ai.airToAir.moveSequences[j].rangeID == i)
                    {
                        tempRangeCounter++;

                        if (RNGCount <= ai.airToAir.moveSequences[j].RNGWeight + tempRNGCounter && RNGCount >= tempRNGCounter)
                        {
                            for (int k = 0; k < ai.airToAir.moveSequences[j].moves.Count; k++)
                            {
                                print(ai.airToAir.moveSequences[j].moves[k].attack);
                                attackDelayCounter = 60;
                                CheckMove(ai.airToAir.moveSequences[j].moves[k].attack);
                                if (ai.airToAir.moveSequences[j].moves[k].isTracking) homingQueue.Add(true);
                                else homingQueue.Add(false);
                            }
                            return;
                        }
                        else { tempRNGCounter += ai.airToAir.moveSequences[j].RNGWeight; }
                    }
                }
            }
        }
    }


    void BossGeese()
    {

        if (attackScript.canAttack || attackScript.recovery)
        {
            if (!enemyScript.stun && attackDelayCounter <= 0 && attackQueue.Count == 0)
            {
                //GROUNDED
                if (enemyMov.ground)
                {
                    //TARGET IS AIRBORNE
                    if (targetAirborne)
                    {
                        //       if (enemyMov.ground) enemyMov.Jump();
                    }
                    //TARGET IS GROUDED
                    else
                    {
                        //RANGE CHECK
                        //SHORT RANGE
                        if (withinRange)
                        {
                            //ATTACK RNG
                            RNGCount = Random.Range(1, 8);
                            if (RNGCount == 1 && enemyMov.ground && !enemyScript.stun)
                            {
                                enemyMov.direction = attackScript.trueDirection;
                                attackDelayCounter = Random.Range(attackDelay, maxDelay);
                                attackQueue.Add(250);
                            }
                            //3 HIT COMBO
                            else if (RNGCount >= 2 && !enemyScript.stun)
                            {
                                if (attackScript.combo == 0)
                                {
                                    attackScript.tracking = true;
                                    attackQueue.Add(50);
                                }
                                else if (attackScript.combo == 1)
                                {
                                    attackQueue.Add(51);
                                }
                                else if (attackScript.combo == 2)
                                {
                                    attackDelayCounter = maxDelay;
                                    attackScript.tracking = true;
                                    attackQueue.Add(52);
                                }
                            }
                        }
                        //LONG RANGE
                        else if (withinRange2 && !withinRange)
                        {
                            attackScript.tracking = true;
                            attackDelayCounter = Random.Range(attackDelay, maxDelay);
                            attackQueue.Add(250);
                        }
                    }

                }
                //AIRBORNE
                else
                {
                    attackScript.tracking = true;
                    attackDelayCounter = Random.Range(attackDelay, maxDelay);
                    attackQueue.Add(250);
                }
            }

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


    public void ExtraMove()
    {
        enemyMov.direction = attackScript.trueDirection;
        TestSlash(movelist.moveExtra, movelist.moveObjectExtra);
    }

    void TestSlash(Move move, GameObject moveObject)
    {
        AttackCancel();
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
        attackQueue.RemoveAt(0);

        if (homingQueue.Count > 0)
        {
            if (homingQueue[0]) attackScript.tracking = true;
            else attackScript.tracking = false;
        }

        ResetHoming();
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
