﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [HeaderAttribute("Movement attributes")]
    public bool mov = true;
    public float m_vel;
    public float runSpeed;
    private float currentSpeed;
    public float airMultiplier = 1;
    public float airDrag = 1;
    public static float faceDirection;

    [HeaderAttribute("Jump attributes")]
    public GameObject positionObject;
    public GameObject jumpObject;
    public GameObject doubleJumpObject;
    [HideInInspector] public Vector3 actualPosition;
    public float m_jump;
    [HideInInspector] public int doubleJump = 1;
    [HideInInspector] public int doubleDash = 1;

    public int remainingJumps;
    public int remainingDash;
    public float fallMultiplier = 1.5F;
    public float lowJumpMultiplier = 1F;
    public float maxVelocity = 1000F;
    public bool jump = false;
    bool doubleJumped;
    public bool ground = true;
    [HideInInspector] public bool onPlatform;
    public Vector2 inheritedVelocity;
    public LayerMask platformMasks;
    public LayerMask enemyMasks;
    public bool running;
    [HideInInspector] public bool doubleTap;
    bool runEnd;
    public float runEndDuration;
    public float ray;
    public float raySpacingLeft;
    public float raySpacingRight;
    bool wasAirborne;
    public GameObject landParticle;
    public GameObject sprintStartParticle;
    public GameObject sprintParticle;
    public GameObject sprintEndParticle;
    public int sprintParticleInterval;
    int sprintCounter;
    [HeaderAttribute("Generic dash attributes")]
    public GameObject dashObject;
    [HideInInspector] public Vector2 dashVelocity = new Vector2(60F, 0F);
    [HideInInspector] public float dashDuration = 12F;
    [HideInInspector] public bool isDashing;
    [HideInInspector] public float dashRecovery = 16F;
    [HideInInspector] public bool dashing = false;
    [HideInInspector] public bool newDash;

    [HeaderAttribute("Backdash attributes")]
    public GameObject backdashObject;
    [HideInInspector] public Vector2 backdashVelocity = new Vector2(-30F, 0F);
    [HideInInspector] public float backdashDuration = 12F;
    [HideInInspector] public bool isBackdashing;
    [HideInInspector] public float backdashRecovery = 12F;
    [HideInInspector] public bool backdashing = false;
    [HideInInspector] public bool newBackdash;

    [HeaderAttribute("Airdash attributes")]
    public GameObject airdashObject;
    [HideInInspector] public Vector2 airdashVelocity = new Vector2(60F, 0F);
    [HideInInspector] public float airdashDuration = 12F;
    [HideInInspector] public float airdashRecovery = 16F;
    [HideInInspector] public bool airdashing = false;

    [HideInInspector] public bool inContact;
    public float push;
    GameObject target;

    bool hitStopped;
    [HideInInspector] public Vector3 oldVel;
    [HideInInspector] public float x;
    [HideInInspector] public float y;
    [HideInInspector] public bool recovery;
    [HideInInspector] public float currentDuration;
    [HideInInspector] public float currentRecovery;
    [HideInInspector] public Rigidbody2D rb;

    Player_Input playerInput;
    Player_AttackScript playerAttackScript;
    PlayerStatus playerStatus;

    void Awake()
    {
        playerInput = GetComponent<Player_Input>();
        playerAttackScript = GetComponent<Player_AttackScript>();
        playerStatus = GetComponent<PlayerStatus>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        currentDuration = dashDuration;
        UpdateDash();
        playerAttackScript.combo = 0; playerAttackScript.Recover();
        wasAirborne = false;
        remainingJumps = doubleJump;
        remainingDash = doubleDash;
        actualPosition = positionObject.transform.position;
    }

    void Update()
    {
        if (PauseMenu.gameIsPaused) { x = 0; rb.velocity = new Vector2(0, rb.velocity.y); }
        if (!PauseMenu.gameIsPaused)
        {
            actualPosition = positionObject.transform.position;
            x = playerInput.inputHorizontal;
            y = playerInput.inputVertical;

        }
        //Resets combo on jump
        if (Physics2D.Raycast(transform.position, -Vector2.up, ray, platformMasks)
            || Physics2D.Raycast(transform.position + Vector3.right * raySpacingRight * transform.localScale.x, -Vector2.up, ray, platformMasks)
            || Physics2D.Raycast(transform.position - Vector3.right * raySpacingLeft * transform.localScale.x, -Vector2.up, ray, platformMasks))
        { ground = true; doubleJumped = false; }
        else ground = false;
        if (!ground && !wasAirborne) { wasAirborne = true; playerAttackScript.combo = 0; }
        //Resets combo on landing
        if (wasAirborne && ground) { playerAttackScript.Recover(); wasAirborne = false; remainingJumps = doubleJump; Instantiate(landParticle, actualPosition, Quaternion.identity); }
        //Doublejump on heads
        if (doubleJumped)
        {

            if (Physics2D.Raycast(transform.position, -Vector2.up, ray, enemyMasks)
                || Physics2D.Raycast(transform.position + Vector3.right * raySpacingRight * transform.localScale.x, -Vector2.up, ray, enemyMasks)
                 || Physics2D.Raycast(transform.position - Vector3.right * raySpacingLeft * transform.localScale.x, -Vector2.up, ray, enemyMasks))
            {
                remainingJumps = 1;
            }
            else remainingJumps = 0;
        }

    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            if (other.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                onPlatform = true;
                inheritedVelocity = other.gameObject.GetComponent<Rigidbody2D>().velocity;
            }
            else { inheritedVelocity = Vector2.zero; onPlatform = false; }
    }

    void SprintStop() { StartCoroutine("RunEnd"); }

    IEnumerator RunEnd()
    {
        runEnd = true;
        if (transform.localScale.x > 0) { Instantiate(sprintEndParticle, actualPosition - Vector3.up, Quaternion.identity); }
        else Instantiate(sprintEndParticle, actualPosition - Vector3.up, Quaternion.Euler(0, 180, 0));
        yield return new WaitForSeconds(runEndDuration);
        runEnd = false;

    }

    void FixedUpdate()
    {
        PushAway();

        faceDirection = transform.localScale.x;
        if (ground) remainingDash = doubleDash;

        if (!doubleTap && running) { running = false; if (ground && mov) SprintStop(); }
        if (ground && doubleTap) { if (!running) Instantiate(sprintStartParticle, actualPosition - Vector3.up * 2, Quaternion.Euler(0, 0, 90)); running = true; }
        if (running)
        {
            currentSpeed = runSpeed;
            sprintCounter++;
            if (sprintCounter >= sprintParticleInterval && !dashing && !recovery)
            {
                sprintCounter = 0;
                if (ground)
                {
                    if (transform.localScale.x > 0) { Instantiate(sprintParticle, actualPosition - Vector3.up, Quaternion.identity); }
                    else Instantiate(sprintParticle, actualPosition - Vector3.up, Quaternion.Euler(0, 180, 0));
                }
            }
        }
        else { currentSpeed = m_vel; sprintCounter = 0; }

        Debug.DrawLine(transform.position, transform.position + Vector3.down * ray, Color.green);
        Debug.DrawLine(transform.position + Vector3.right * raySpacingRight * transform.localScale.x, transform.position + Vector3.right * raySpacingRight * transform.localScale.x + Vector3.down * ray, Color.green);
        Debug.DrawLine(transform.position - Vector3.right * raySpacingLeft * transform.localScale.x, transform.position - Vector3.right * raySpacingLeft * transform.localScale.x + Vector3.down * ray, Color.green);

        if (HitStopScript.hitStop)
        {
            if (!hitStopped)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                hitStopped = true;
                oldVel = rb.velocity;
            }
        }
        else if (!HitStopScript.hitStop)
        {
            if (hitStopped)
            {
                hitStopped = false;
                rb.velocity = oldVel;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            //MOVEMENT
            if (playerStatus.state == PlayerStatus.State.Neutral || playerStatus.state == PlayerStatus.State.HitRecovery)
            {
                if (mov && !PauseMenu.gameIsPaused)
                {
                    if (x == 0 && runEnd && ground) { rb.velocity = rb.velocity * 0.95F; }
                    else
                    {
                        if (ground)
                        {
                            rb.velocity = new Vector2(x * currentSpeed * Time.deltaTime, rb.velocity.y);

                        }
                        else
                        {
                            rb.AddForce(new Vector2(x * currentSpeed * airMultiplier * Time.deltaTime, 0), ForceMode2D.Force);
                            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -(currentSpeed * Time.deltaTime) * airDrag, (currentSpeed * Time.deltaTime) * airDrag), rb.velocity.y);
                        }
                    }

                    if (ground)
                    {
                        rb.velocity = rb.velocity + new Vector2(inheritedVelocity.x, 0);
                    }
                }

                if (jump)
                {
                    rb.velocity = new Vector2(rb.velocity.x + x * currentSpeed * Time.deltaTime, 0) + Vector2.up * m_jump; jump = false;
                    rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, 0, m_jump));
                }

            }

            if (rb.velocity.y < 0) { rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime; }
            else if (rb.velocity.y > 0 && !Input.GetButton("KB_Jump") && !Input.GetButton("XB_Jump") || rb.velocity.y > 0 && !mov) rb.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity), Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity));

            //Change direction
            if (x < 0 && mov) transform.localScale = new Vector3(-1, 1, 1);
            else if (x > 0 && mov) transform.localScale = new Vector3(1, 1, 1);


            //Dashing
            if (dashing) Dashing();
            else if (backdashing) Backdashing();
            else if (airdashing) Airdashing();

            if (recovery)
            {
                currentRecovery -= 1;
                if (ground && playerStatus.stuncounter <= 0)
                {
                    AddVelocity(Vector2.zero);
                    mov = false;
                }
                else if (!ground && playerStatus.stuncounter <= 0)
                {
                    mov = true;
                }
            }
            if (currentRecovery <= 0 && recovery) { DashRecovery(); }
        }
    }

    void Dashing()
    {
        if (currentDuration == dashDuration)
        {
            if (transform.localScale.x > 0) { Instantiate(dashObject, actualPosition, Quaternion.identity); }
            else Instantiate(dashObject, actualPosition, Quaternion.Euler(0, 180, 0));
        }
        if (currentDuration > 0)
        {
            currentDuration -= 1;
            AddVelocity(new Vector2(dashVelocity.x * transform.localScale.x, dashVelocity.y));
            gameObject.layer = LayerMask.NameToLayer("iFrames");
        }
        if (currentDuration <= 0 && dashing && !recovery)
        {
            dashing = false;
            currentDuration = dashDuration;
            gameObject.layer = LayerMask.NameToLayer("Player");
            recovery = true;
            if (ground)
            {
                if (transform.localScale.x > 0) { Instantiate(sprintEndParticle, actualPosition - Vector3.up - Vector3.right * transform.localScale.x * 2, Quaternion.identity); }
                else Instantiate(sprintEndParticle, actualPosition - Vector3.up - Vector3.right * transform.localScale.x * 2, Quaternion.Euler(0, 180, 0));
                //AddVelocity(Vector2.zero);
                mov = false;
            }
            else if (!ground)
            {
                mov = true;
            }
        }
    }
    void Airdashing()
    {
        if (currentDuration == airdashDuration)
        {
            if (transform.localScale.x > 0) { Instantiate(airdashObject, actualPosition, Quaternion.identity); }
            else Instantiate(airdashObject, actualPosition, Quaternion.Euler(0, 180, 0));
        }
        if (currentDuration > 0)
        {
            currentDuration -= 1;
            AddVelocity(new Vector2(airdashVelocity.x * transform.localScale.x, airdashVelocity.y));
            gameObject.layer = LayerMask.NameToLayer("iFrames");
        }
        if (currentDuration <= 0 && airdashing && !recovery)
        {
            airdashing = false;
            currentDuration = airdashDuration;
            gameObject.layer = LayerMask.NameToLayer("Player");
            recovery = true;
            //AddVelocity(Vector2.zero);
        }
    }
    void Backdashing()
    {
        if (currentDuration == backdashDuration)
        {
            if (transform.localScale.x < 0) { Instantiate(backdashObject, actualPosition, Quaternion.identity); }
            else Instantiate(backdashObject, actualPosition, Quaternion.Euler(0, 180, 0));
        }
        if (currentDuration > 0)
        {
            currentDuration -= 1;
            AddVelocity(new Vector2(backdashVelocity.x * transform.localScale.x, backdashVelocity.y));
            gameObject.layer = LayerMask.NameToLayer("iFrames");
        }
        if (currentDuration <= 0 && backdashing && !recovery)
        {
            backdashing = false;
            currentDuration = backdashDuration;
            gameObject.layer = LayerMask.NameToLayer("Player");
            recovery = true;
            //AddVelocity(Vector2.zero);
        }
    }

    public void Jump()
    {
        if (remainingJumps > 0 && !dashing && !ground)
        {
            doubleJumped = true;
            remainingJumps -= 1;
            jump = true;
            Instantiate(doubleJumpObject, actualPosition - Vector3.up * 2, Quaternion.Euler(0, 0, 90));
            playerAttackScript.combo = 0;
            DashRecovery();
        }

        if (remainingJumps > 0 && !dashing && ground)
        {
            ground = false;
            playerAttackScript.jumpDelay = true;
            Instantiate(jumpObject, actualPosition - Vector3.up * 2, Quaternion.Euler(0, 0, 90));
            jump = true;
            playerAttackScript.combo = 0;
            DashRecovery();
        }

        if (remainingJumps > 0 && !dashing && playerAttackScript.recovery && !ground ||
            Input.GetButtonDown("KB_Jump") && remainingJumps > 0 && !dashing && !ground ||
            Input.GetButtonDown("XB_Jump") && remainingJumps > 0 && !dashing && !ground)
        {
            doubleJumped = true;
            playerAttackScript.AttackCancel();
            remainingJumps -= 1;
            jump = true;
            Instantiate(doubleJumpObject, actualPosition - Vector3.up * 2, Quaternion.Euler(0, 0, 90));
            playerAttackScript.combo = 0;
            DashRecovery();
        }
        if (remainingJumps > 0 && !dashing && playerAttackScript.recovery && ground ||
            Input.GetButtonDown("KB_Jump") && remainingJumps > 0 && !dashing && ground ||
            Input.GetButtonDown("XB_Jump") && remainingJumps > 0 && !dashing && ground)
        {
            playerAttackScript.AttackCancel();
            playerAttackScript.jumpDelay = true;
            ground = false;
            Instantiate(jumpObject, actualPosition - Vector3.up * 2, Quaternion.Euler(0, 0, 90));
            jump = true;
            playerAttackScript.combo = 0;
            DashRecovery();
        }

    }

    public void AddVelocity(Vector2 addVel)
    {
        rb.velocity = addVel;
        if (ground) rb.velocity = rb.velocity + inheritedVelocity;
    }

    public void UpdateDash()
    {
        currentRecovery = dashRecovery;
    }

    public void DashRecovery()
    {
        currentDuration = dashDuration;
        isBackdashing = false;
        backdashing = false;
        newBackdash = false;

        isDashing = false;
        dashing = false;
        newDash = false;
        if (playerAttackScript.canAttack) mov = true;
        currentRecovery = dashRecovery;
        recovery = false;
    }

    public void Dash()
    {
        if (!backdashing && !dashing && !recovery && remainingDash > 0)
        {
            if (x < 0) transform.localScale = new Vector3(-1, 1, 1);
            else if (x > 0) transform.localScale = new Vector3(1, 1, 1);
            gameObject.layer = LayerMask.NameToLayer("iFrames");
            newDash = true;
            isDashing = true;
            remainingDash--;
            mov = false;
            currentRecovery = dashRecovery;
            dashing = true;
        }
    }

    public void Airdash()
    {
        if (!backdashing && !dashing && !recovery && remainingDash > 0)
        {
            if (x < 0) transform.localScale = new Vector3(-1, 1, 1);
            else if (x > 0) transform.localScale = new Vector3(1, 1, 1);
            gameObject.layer = LayerMask.NameToLayer("iFrames");
            newDash = true;
            isDashing = true;
            remainingDash--;
            mov = false;
            currentRecovery = dashRecovery;
            airdashing = true;
        }
    }

    public void Backdash()
    {
        if (!backdashing && !dashing && !recovery && remainingDash > 0)
        {
            gameObject.layer = LayerMask.NameToLayer("iFrames");
            // faceDirection
            newDash = true;
            newBackdash = true;
            isBackdashing = true;
            remainingDash--;
            currentRecovery = backdashRecovery;
            mov = false;
            backdashing = true;
        }
    }

    public void AddRecoil(float side, float up)
    {
        playerAttackScript.keepVel = true;
        playerAttackScript.activeFrames = 0;
        rb.velocity = new Vector2(side * transform.localScale.x + rb.velocity.x, up);

    }

    void PushAway()
    {
        if (!ground && inContact && rb.velocity.y <= 0)
        {
            print("push");
            transform.Translate(new Vector2(Mathf.Sign((transform.position.x - target.transform.position.x)) * push, 0));
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag("Head"))
        {
            target = other.gameObject;
            inContact = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Head"))
        {

            inContact = false;
        }
    }



}
