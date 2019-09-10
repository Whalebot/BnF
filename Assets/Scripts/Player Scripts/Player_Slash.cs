using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Slash : MonoBehaviour
{
    [SerializeField] private Transform center;
    public int dmg;
    public int poiseDamage;
    public float knockback;
    public float knockup;

    public float hitstun;
    public float chargeAmount = 1;
    public float hitPause = 0.1F;

    public float activeFrames = 0.1F;
    public bool singleHit = true;
    bool hitPauseOnce;
    public bool freeze;
    public int freezeFrames;

    public bool pulling;

    public GameObject hitParticle;
    public GameObject pullTarget;

    public bool leaveParticle;
    GameObject swordParticle;

    PlayerStatus playerStatus;
    Collider2D col;
    int RNGCount;
    [HeaderAttribute("Ranged")]
    public bool ranged = false;
    public GameObject fireball;

    [HeaderAttribute("Recoil attributes")]
    public bool hasRecoil = false;
    public float upRecoil;
    public float sideRecoil;

    [HeaderAttribute("Ground Bounce")]
    public bool hasGroundBounce;
    public float gBKnockback;
    public float gBKnockup;
    public float gBHitstun;

    [HeaderAttribute("Clash attributes")]
    public bool canClash = false;
    public bool clashActive = false;
    public int clashFrames;
    public float hitstopframes;
    int clashCounter;
    public GameObject clashFX;

    GameObject manager;
    GameObject player;
    //   Player_Movement playerMov;
    HitStopScript hitStopScript;
    UIManager uiManager;
    Weapon_Attackscript weaponScript;
    bool clashed;

    public List<GameObject> enemyList;

    // Use this for initialization
    void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        if (manager != null)
        {

            hitStopScript = manager.GetComponent<HitStopScript>();
            uiManager = manager.GetComponent<UIManager>();
        }
        enemyList = new List<GameObject>();
        //   playerMov = transform.parent.GetComponentInParent<Player_Movement>();
        weaponScript = transform.parent.GetComponent<Weapon_Attackscript>();
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void OnDisable()
    {
        ResetClash();

        if (leaveParticle)
        {
            swordParticle = transform.GetChild(0).gameObject;
            GameObject particleClone = Instantiate(swordParticle, transform.position, Quaternion.identity);
        }
        enemyList.Clear();

    }

    private void FixedUpdate()
    {
        if (clashed && !HitStopScript.hitStop) { DisableCollider();}

        clashCounter++;
        if (clashCounter >= clashFrames)
        { clashActive = false; }
    }

    void ResetClash()
    {
        clashCounter = 0;
        clashActive = true;
        clashed = false;
    }

    void Clash(Collider2D enemy)
    {
        if(center != null)
        Instantiate(clashFX, center.position, Quaternion.identity);
        else Instantiate(clashFX, transform.position, Quaternion.identity);
        hitStopScript.HitStop(hitstopframes);
        enemy.GetComponent<EnemyDmg>().clashed = true;
        print("Clash");
        GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManagerScript>().Zoom(transform.position);
        //weaponScript.AttackCancel();
    }

    void DisableCollider()
    {
        weaponScript.AttackCancel();
    }

    void OnEnable()
    {
        ResetClash();
        hitPauseOnce = false;
        StartCoroutine("AttackOnce", activeFrames);
        if (ranged) Instantiate(fireball, transform.position, Quaternion.identity);
    }
    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.CompareTag("EnemyAttack"))
        {
            if (clashActive)
            {
                clashed = true;
                Clash(enemy);
            }
        }
        else if (!clashed)
        {
            if (enemy.CompareTag("Enemy") || enemy.CompareTag("Boss"))
            {
                DoDmg(enemy.gameObject);
                RNGCount = Random.Range(-3, 4);

            }
        }

        if (enemy.CompareTag("EnemyProjectile"))
        {
            RNGCount = Random.Range(-3, 4);
            Instantiate(hitParticle, enemy.transform.position, Quaternion.Euler(0, 0, 15 * RNGCount));
        }
    }

    IEnumerator AttackOnce(float dur)
    {
        col.enabled = true;
        yield return new WaitForSeconds(dur * Time.deltaTime);
        col.enabled = false;
    }

    void DoDmg(GameObject enemy)
    {
        {
            if (singleHit)
            {
                if (!enemyList.Contains(enemy))
                {
                    Instantiate(hitParticle, enemy.transform.GetChild(0).transform.position, Quaternion.Euler(0, 0, 15 * RNGCount));
                    enemyList.Add(enemy);
                    playerStatus.special += chargeAmount;

                    if (manager != null)
                        if (enemy.GetComponent<EnemyScript>().stun && !HitStopScript.hitStop && !hitPauseOnce)
                        {
                            hitStopScript.HitStop(hitPause); hitPauseOnce = true;
                        }

                    if (hasRecoil) GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().AddRecoil(sideRecoil, upRecoil);
                    if (manager != null) uiManager.ComboUp();
                    enemy.GetComponent<EnemyScript>().Hitstun(hitstun, poiseDamage);
                    enemy.GetComponent<EnemyScript>().TakeDamage(dmg);


                    if (pulling && enemy.GetComponent<EnemyScript>().stun) enemy.GetComponent<EnemyScript>().Pull(pullTarget.transform.position, 0.3F);
                    else enemy.GetComponent<EnemyScript>().Knockback(transform.parent.parent.position, knockback, knockup);

                    if (hasGroundBounce) { enemy.GetComponent<EnemyScript>().GroundBounce(transform.parent.parent.position, gBKnockback, gBKnockup, gBHitstun); print("poi"); }

                    if (freeze) { enemy.GetComponent<EnemyScript>().Freeze(freezeFrames); }
                }
            }
            /*
            else
            {
                Instantiate(hitParticle, enemy.transform.position, Quaternion.Euler(0, 0, 15 * RNGCount));
                enemyList.Add(enemy);
                playerStatus.special += chargeAmount;
                //  if (hasRecoil) GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().AddRecoil(sideRecoil, upRecoil);
                uiManager.ComboUp();
                enemy.GetComponent<EnemyScript>().Hitstun(hitstun, poiseDamage);
                enemy.GetComponent<EnemyScript>().TakeDamage(dmg);

                if (pulling && enemy.GetComponent<EnemyScript>().stun) enemy.GetComponent<EnemyScript>().Pull(pullTarget.transform.position, 0.3F);
                else enemy.GetComponent<EnemyScript>().Knockback(transform.parent.parent.position, knockback, knockup);

                if (hasGroundBounce) { enemy.GetComponent<EnemyScript>().GroundBounce(transform.parent.parent.position, gBKnockback, gBKnockup, gBHitstun); print("poi"); }

                if (enemy.GetComponent<EnemyScript>().stun && !HitStopScript.hitStop && !hitPauseOnce)
                { hitStopScript.HitStop(hitPause); hitPauseOnce = true; }
            }*/
        }
    }
}
