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
    [HideInInspector] public GameObject currentAttack;
    public int attackID;

    public int attackDelay;

    public int maxDelay;
    int attackDelayCounter;

    public float distance;
    public float distance2;

    bool hasIFrames;

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

    public float currentDistance;
    int walkCounter;
   bool walkForward;

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
        if (enemyMov.target != null)
        {

            currentDistance = Mathf.Abs(transform.position.x - enemyMov.target.transform.position.x);
            targetAirborne = enemyMov.target.transform.position.y > transform.position.y;



            if (ai.groundToGround.numberOfRanges > 0)
                for (int i = 0; i < ai.groundToGround.numberOfRanges; i++)
                {
                    ai.groundToGround.withinRanges[i] = Mathf.Abs(transform.position.x - enemyMov.target.transform.position.x) < ai.groundToGround.temp2Ranges[i];
                }
            if (ai.groundToAir.numberOfRanges > 0)
                for (int i = 0; i < ai.groundToAir.numberOfRanges; i++)
                {
                    ai.groundToAir.withinRanges[i] = Mathf.Abs(transform.position.x - enemyMov.target.transform.position.x) < ai.groundToAir.temp2Ranges[i];
                }
            if (ai.airToGround.numberOfRanges > 0)
                for (int i = 0; i < ai.airToGround.numberOfRanges; i++)
                {
                    ai.airToGround.withinRanges[i] = Mathf.Abs(transform.position.x - enemyMov.target.transform.position.x) < ai.airToGround.temp2Ranges[i];
                }
            if (ai.airToAir.numberOfRanges > 0)
                for (int i = 0; i < ai.airToAir.numberOfRanges; i++)
                {
                    ai.airToAir.withinRanges[i] = Mathf.Abs(transform.position.x - enemyMov.target.transform.position.x) < ai.airToAir.temp2Ranges[i];
                }
        }
    }
    void FixedUpdate()
    {
        if (!HitStopScript.hitStop)
        {
            PhysicsTest();

            if (enemyScript.stun) attackQueue.Clear();
            // if (attackScript.recovery) attackDelayCounter = 0;

            RangeDetection();

            PerformAction();
  
            if (walkForward ) {
                walkCounter--;
                attackScript.canAttack = false;
                if (walkCounter <= 0 || currentDistance <= ai.walkDistance)
                {
                    walkForward = false;
                    attackScript.canAttack = true;
                    walkCounter = 0;
                }
            }

            if (!targetAirborne && enemyMov.ground) ai.state = 0;
            else if (targetAirborne && enemyMov.ground) ai.state = 1;
            else if (!targetAirborne && !enemyMov.ground) ai.state = 2;
            else if (targetAirborne && !enemyMov.ground) ai.state = 3;
        }
    }

    void PerformAction()
    {
        if (attackScript.canAttack) { attackDelayCounter--;
            if (attackDelayCounter <= 0) enemyMov.mov = true;
            else { enemyMov.mov = false; }
        }


        if (enemyScript.detected && enemyScript.requiresTrigger || !enemyScript.requiresTrigger)
            if (isBoss)
            {
                if (behaviorID == 0) Boss();
            }
        if (attackQueue.Count > 0 && attackScript.canAttack || attackScript.recovery && attackQueue.Count > 0)
        {
            switch (attackQueue[0])
            {
                case 6: WalkForward(); return;

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
                case 251: TestSlash(movelist.move5SS, movelist.moveObject5SS); return;
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
        else if (attack == MoveProperties.Attack.walk) { attackQueue.Add(6); }
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
                    case 3: AttackAlgorhythm(ai.airToAir); return;
                    case 2: AttackAlgorhythm(ai.airToGround); return;
                    case 1: AttackAlgorhythm(ai.groundToAir); return;
                    case 0: AttackAlgorhythm(ai.groundToGround); return;
                    default: AttackAlgorhythm(ai.groundToGround); return;
                }

            }
        }
    }

    void WalkForward() {
        walkForward = true;
        walkCounter = ai.walkDuration;
        attackQueue.RemoveAt(0);
        homingQueue.RemoveAt(0);
    }

    void AttackAlgorhythm(AttackState attackState)
    {
        int tempRangeCounter = 0;
        int tempRNGCounter = 0;

        //FOR EACH RANGE
        for (int i = 0; i < attackState.withinRanges.Count; i++)
        {
            //CHECK IF WITHIN RANGE
            if (attackState.withinRanges[i])
            {
                RNGCount = Random.Range(0, attackState.RNGlevels[i] + 1);
                homingQueue.Clear();
                for (int j = 0; j < attackState.moveSequences.Count; j++)
                {
                    if (attackState.moveSequences[j].range == attackState.temp2Ranges[i])
                    {
                        tempRangeCounter++;

                        if (RNGCount <= attackState.moveSequences[j].RNGWeight + tempRNGCounter && RNGCount >= tempRNGCounter)
                        {
                            for (int k = 0; k < attackState.moveSequences[j].moves.Count; k++)
                            {
                                attackDelayCounter = attackDelay;
                                CheckMove(attackState.moveSequences[j].moves[k].attack);
                                homingQueue.Add(attackState.moveSequences[j].moves[k].tracking);

                            }
                            return;
                        }
                        else { tempRNGCounter += attackState.moveSequences[j].RNGWeight; }
                    }
                }
            }
        }
    }
    /*
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
    }*/
  
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
    /*
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
    */

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

        //attackScript.combo = move.combo;

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
        attackScript.tornado = move.spin;

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
            attackScript.tracking = homingQueue[0];
        
            ResetHoming();

        }


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
         enemyMov.dashRecovery = movelist.dashRecovery;
        enemyMov.currentRecovery = movelist.dashRecovery;
        enemyMov.currentDuration = movelist.dashDuration;
    }
}
