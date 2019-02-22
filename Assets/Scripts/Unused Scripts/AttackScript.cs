using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public GameObject AttackObject1;
    public GameObject AttackObject2;
    public GameObject AttackObject3;
    public GameObject Launcher;
    public GameObject DownSlash;

    public float particleDuration;

    public float attackSpeed;
    public float attackStart;

    public bool attacking;
    public bool attack1;
    public bool attack2;
    public bool attack3;
    public bool launcher;
    public bool downslash;
    public bool canAttack = true;

    Player_Movement playerMov;

    public float activeFrames;

    private float lastAttacked;

    // Use this for initialization
    void Start()
    {
        AttackEnd();
        playerMov = GetComponent<Player_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack") && canAttack && lastAttacked + attackSpeed < Time.time) { Attack(); attacking = true; attackStart = Time.time; lastAttacked = Time.time; }
        else if (attacking && attackStart + activeFrames < Time.time) AttackEnd();
    }

    void Attack()
    {
        if (playerMov.y > 0)
        {
            if (playerMov.ground == true)
            {
                StartCoroutine("AttackAnimationLauncher", attackSpeed);
                Launcher.SetActive(true);
                StartCoroutine("DisableObjects", particleDuration);
                return;
            }
            return;
        }
        else if (playerMov.y < 0)
        {
            if (playerMov.ground != true)
            {
                StartCoroutine("AttackAnimationDownSlash", attackSpeed);
                DownSlash.SetActive(true);
                StartCoroutine("DisableObjects", particleDuration);
                return;
            }
            return;
        }
        else if (!attack1 && !attack2 && !attack3 && playerMov.y == 0)
        {
            float attack1Start = Time.time;
            AttackObject1.SetActive(true);
            attack1 = true;
            StartCoroutine("AttackAnimation", attackSpeed);
            StartCoroutine("DisableObjects", particleDuration);
        }

        else if (attack1)
        {
            attack1 = false;
            float attack2Start = Time.time;
            //AttackObject1.SetActive(false);
            AttackObject2.SetActive(true);
            attack2 = true; StartCoroutine("AttackAnimation", attackSpeed);
            StartCoroutine("DisableObjects", particleDuration);
        }

        else if (attack2)
        {
            attack2 = false;
            float attack3Start = Time.time;
            //AttackObject2.SetActive(false);
            AttackObject3.SetActive(true);
            attack3 = true;
            canAttack = false; StartCoroutine("AttackAnimation", attackSpeed);
            StartCoroutine("DisableObjects", particleDuration * 2);
        }

    }

    IEnumerator AttackAnimation(float dur)
    {
        playerMov.mov = false;
        if (playerMov.ground) playerMov.AddVelocity(new Vector2(transform.localScale.x, 0));
        if (!playerMov.ground) playerMov.AddVelocity(new Vector2(transform.localScale.x, 15));
        yield return new WaitForSeconds(dur);
        playerMov.mov = true;
    }

    IEnumerator AttackAnimationLauncher(float dur)
    {
        playerMov.mov = false;
        if (playerMov.ground) playerMov.AddVelocity(new Vector2(transform.localScale.x, 50));
        if (!playerMov.ground) playerMov.AddVelocity(new Vector2(transform.localScale.x, 15));
        yield return new WaitForSeconds(dur);
        playerMov.mov = true;
    }
    IEnumerator AttackAnimationDownSlash(float dur)
    {
        playerMov.mov = false;
        if (playerMov.ground) playerMov.AddVelocity(new Vector2(transform.localScale.x, 0));
        if (!playerMov.ground) playerMov.AddVelocity(new Vector2(transform.localScale.x, -50));
        yield return new WaitForSeconds(dur);
        playerMov.mov = true;
    }
    void AttackEnd()
    {
        AttackObject1.SetActive(false);
        AttackObject2.SetActive(false);
        AttackObject3.SetActive(false);
        Launcher.SetActive(false);
        DownSlash.SetActive(false);
        attack1 = false;
        attack2 = false;
        attack3 = false;
        launcher = false;
        downslash = false;
        attacking = false;
        canAttack = true;
    }
    IEnumerator DisableObjects(float dur)
    {
        yield return new WaitForSeconds(dur);
        AttackObject1.SetActive(false);
        AttackObject2.SetActive(false);
        AttackObject3.SetActive(false);
        Launcher.SetActive(false);
        DownSlash.SetActive(false);
    }

}
