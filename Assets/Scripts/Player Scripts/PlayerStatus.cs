using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    [Header("Status")]
    public int health;
    public int health2;
    public int health3;
    public bool sword1Alive;
    public bool sword2Alive;
    public bool sword3Alive;
    public float special = 60;

    public float ray;
    public float maxSpeed;

    [Header("Weapon")]
    public int activeWeapon;
    public int weapons;
    public float weaponSwitchDelay;
    bool flash;
    bool weaponBroke;
    public GameObject weaponBreakSound;

    [Header("Getting hit attributes")]
    public bool canTakeDmg = true;
    public float stuncounter;
    public bool hitInvul;
    public int invulDur;
    public bool hitAnimation;
    bool recovered;
    float invulcounter;

    [Header("Parry attributes")]
    public bool isParrying;
    public int parryWindow;
    public int parryRecovery;

    float weaponSwitchCounter;
    bool weaponSwitch;
    bool dying;


    public GameObject[] dmgSound;

    private SpriteRenderer myRenderer;
    Rigidbody2D rb;
    Player_Movement playerMovement;
    Player_AttackScript playerAttack;
    Player_Input inputManager;
    GameObject manager;
    UIManager uiManager;

    void Start()
    {
        invulcounter = invulDur;
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        activeWeapon = 1;
        playerMovement = GetComponent<Player_Movement>();
        playerAttack = GetComponent<Player_AttackScript>();
        rb = GetComponent<Rigidbody2D>();
        inputManager = GetComponent<Player_Input>();
        manager = GameObject.FindGameObjectWithTag("Manager");
        if (manager != null)
            uiManager = manager.GetComponent<UIManager>();
        if (playerAttack.moveset2 == null) { sword2Alive = false; health2 = 0; }
        if (playerAttack.moveset3 == null) { sword3Alive = false; health3 = 0; }
    }

    void Update()
    {
        if (health + health2 + health3 <= 0 && !dying)
        {
            dying = true;
            StartCoroutine(Death());
        }

        special = Mathf.Clamp(special, 0, 60);
        if (health + health2 + health3 < 0)
        {
            //SceneManager.LoadScene("Level and UI test");
        }


    }

    void FixedUpdate()
    {

        if (inputManager.weaponSwitchQueue.Count > 0 && !playerMovement.dashing && !playerMovement.backdashing && !playerMovement.recovery)
        {
            if (inputManager.weaponSwitchQueue[0] == 1)
            {
                if (playerAttack.recovery && !weaponSwitch || playerAttack.canAttack && !weaponSwitch || hitInvul && !weaponSwitch)
                {
                    //     if ()
                    weaponBroke = false;
                    weaponSwitch = true;
                    inputManager.weaponSwitchQueue.RemoveAt(0);
                    if (activeWeapon == 1)
                    {
                        activeWeapon = 3;
                    }
                    else
                    {
                        activeWeapon -= 1;
                    }
                    if (!sword3Alive && activeWeapon == 3) activeWeapon--;
                    if (!sword2Alive && activeWeapon == 2) activeWeapon--;
                    if (!sword1Alive && activeWeapon == 1) activeWeapon = 3;
                    if (!sword3Alive && activeWeapon == 3) activeWeapon--;
                    if (manager != null) { uiManager.WeaponSwitch(activeWeapon); }

                    playerAttack.MovesetChange(activeWeapon);
                }
            }

            else if (inputManager.weaponSwitchQueue[0] == 2)
            {
                if (playerAttack.recovery && !weaponSwitch || playerAttack.canAttack && !weaponSwitch || hitInvul && !weaponSwitch)
                {
                    weaponSwitch = true;
                    inputManager.weaponSwitchQueue.RemoveAt(0);
                    if (activeWeapon == 3)
                    {
                        activeWeapon = 1;
                    }
                    else
                    {
                        activeWeapon += 1;
                    }
                    if (!sword1Alive && activeWeapon == 1) activeWeapon++;
                    if (!sword2Alive && activeWeapon == 2) activeWeapon++;
                    if (!sword3Alive && activeWeapon == 3) activeWeapon = 1;
                    if (!sword1Alive && activeWeapon == 1) activeWeapon++;
                    if (manager != null) { uiManager.WeaponSwitch(activeWeapon); }
                    playerAttack.MovesetChange(activeWeapon);
                }
            }
        }

        if (weaponBroke)
        {
            weaponBroke = false;
            weaponSwitch = true;
            if (activeWeapon == 1)
            {
                activeWeapon = 3;
            }
            else
            {
                activeWeapon -= 1;
            }
            if (!sword3Alive && activeWeapon == 3) activeWeapon--;
            if (!sword2Alive && activeWeapon == 2) activeWeapon--;
            if (!sword1Alive && activeWeapon == 1) activeWeapon = 3;
            if (!sword3Alive && activeWeapon == 3) activeWeapon--;
            if (manager != null) { uiManager.WeaponSwitch(activeWeapon); }
            playerAttack.MovesetChange(activeWeapon);
            Instantiate(weaponBreakSound);
            playerMovement.UpdateDash();
        }

        // if (!sword1Alive && !sword2Alive && !sword3Alive) StartCoroutine("Death");
        if (health <= 0 && sword1Alive) { sword1Alive = false; weaponBroke = true; }
        if (health2 <= 0 && sword2Alive) { sword2Alive = false; weaponBroke = true; }
        if (health3 <= 0 && sword3Alive) { sword3Alive = false; weaponBroke = true; }

        if (weaponSwitch) weaponSwitchCounter -= 1;
        if (weaponSwitchCounter <= 0) { weaponSwitch = false; weaponSwitchCounter = weaponSwitchDelay; }



        if (isParrying) { canTakeDmg = false; }
        if (!canTakeDmg && stuncounter > 0) { stuncounter -= 1; hitAnimation = true; gameObject.layer = LayerMask.NameToLayer("iFrames"); playerAttack.canAttack = false; playerMovement.mov = false; }
        if (stuncounter <= 0 && !playerAttack.canAttack && hitAnimation && !recovered) { playerAttack.canAttack = true; recovered = true; playerMovement.mov = true; gameObject.layer = LayerMask.NameToLayer("Invul"); }
        if (stuncounter <= 0 && !canTakeDmg && !hitInvul) { hitInvul = true; }
        if (hitInvul)
        {
            if (!playerMovement.dashing) gameObject.layer = LayerMask.NameToLayer("Invul");
            invulcounter -= 1; hitAnimation = false; flash = !flash; playerMovement.mov = true;
            if (flash) { myRenderer.enabled = !myRenderer.enabled; }
            if (invulcounter <= 5) { playerAttack.canAttack = true; }
            if (invulcounter <= 0) { Recover(); }
        }
    }

    public void Recover() {
        playerAttack.canAttack = true; canTakeDmg = true; hitInvul = false; myRenderer.enabled = true; invulcounter = invulDur; recovered = false; gameObject.layer = LayerMask.NameToLayer("Player");
    }

    void Parry() { }

    public void TakeDamage(int dmg)
    {
        if (canTakeDmg)
        {
            if (activeWeapon == 1)
            {
                if (health == 1)
                {
                    if (manager != null)
                    { uiManager.SwordBreak(activeWeapon); }
                }
                health -= dmg;
            }
            if (activeWeapon == 2)
            {
                if (health2 == 1)
                {
                    if (manager != null)
                    { uiManager.SwordBreak(activeWeapon); }
                }
                health2 -= dmg;
            }
            if (activeWeapon == 3)
            {
                if (health3 == 1)
                {
                    if (manager != null)
                    { uiManager.SwordBreak(activeWeapon); }
                }
                health3 -= dmg;
            }
            if (manager != null)
            {
                uiManager.shouldShake = true;
            }
            Instantiate(dmgSound[Random.Range(0, 4)]);

        }
        else return;
        UIManager.hits = 0;
    }
    public void Hitstun(float dur)
    {
        if (canTakeDmg)
        {
            playerAttack.AttackCancel();
            stuncounter = dur;
          
        }
        else if (isParrying) { Parry(); }
        else return;
    }

    public void Knockback(float dir, float force, float knockup)
    {
        if (canTakeDmg)
        {
            playerAttack.Recover();
            playerMovement.mov = false;
            playerAttack.canAttack = false;
            //   rb.velocity = Vector2.right * force + Vector2.up * knockup + Vector2.right * knockup;
            rb.velocity = new Vector2(dir, 0) * force + Vector2.up * knockup;
            canTakeDmg = false;
        }
        else return;
    }


    IEnumerator Death()
    {
      
        playerMovement.mov = false;
        playerAttack.canAttack = false;
        canTakeDmg = false;
        yield return new WaitForSeconds(1);
        manager.GetComponent<LoadingScreen>().StartCoroutine("LoadSame");

    }

}