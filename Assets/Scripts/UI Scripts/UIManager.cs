using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public GameObject statusBars;
    public GameObject Door1;
    public GameObject Door2;

    public GameObject HPCircle;
    public GameObject comboParticle;
    public GameObject comboCounter;
    public GameObject brokenScreen;

    public Text ComboCounter;
    public Text HITS;
    public Image stroke;

    public Sprite brokenScreenBlue;
    public Sprite brokenScreenPink;
    public Sprite brokenScreenGreen;
    Color col;
    public GameObject Player;

    public bool shouldShake = false;
    public float duration = 1f;
    public float power = 0.7f;
    public float slowDownAmount = 1f;
    public float UIMoveSpeed;
    public static int hits;
    public float comboReset;
    public float comboTimer;
    Vector3 startPosition;
    Vector3 startPosition2;
    Vector3 startPosition3;
    float initialDuration;
    public float rotateSpeed;

    float HPCircleRotation;
    PlayerStatus playerStatus;
    ComboCounter comboScript;
    // Use this for initialization
    void Start()
    {
        col = brokenScreen.GetComponent<Image>().color;
        col.a = 0f;

        Player = GameObject.FindGameObjectWithTag("Player");
        initialDuration = duration;
        StartCoroutine(LvlStart());
        playerStatus = Player.GetComponent<PlayerStatus>();
        comboScript = HITS.GetComponent<ComboCounter>();

        startPosition = statusBars.GetComponent<RectTransform>().position;
    }

    // Update is called once per frame
    void Update()
    {

        float rotateStep = rotateSpeed * Time.deltaTime;
        HPCircle.transform.rotation = Quaternion.Slerp(HPCircle.transform.rotation, Quaternion.Euler(HPCircle.transform.rotation.x, HPCircle.transform.rotation.y, HPCircleRotation), rotateStep);

        if (hits > 0)
        {
            comboCounter.GetComponent<RectTransform>().localScale = new Vector3(Mathf.Clamp(comboTimer, 1f, comboReset), Mathf.Clamp(comboTimer, 1f, comboReset), 1);
            ComboCounter.text = hits.ToString();
            HITS.text = "HITS";
            stroke.GetComponent<Image>().enabled = true;
            comboTimer -= Time.deltaTime * 0.25f;
            if (comboTimer <= 1)
            {
                hits = 0;
            }
        }
        else
        {
            ComboCounter.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            HITS.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            stroke.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            hits = 0;
            ComboCounter.text = " ";
            HITS.text = " ";
            stroke.GetComponent<Image>().enabled = false;
        }

        //startPosition = statusBars.GetComponent<RectTransform>().anchoredPosition;



        if (shouldShake)
        {
            if (duration > 0)
            {
                statusBars.GetComponent<RectTransform>().position = startPosition + Random.insideUnitSphere * power;
                //HP.transform.localPosition = startPositio n + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                statusBars.transform.position = startPosition;
            }
        }
    }

    void FixedUpdate()
    {
        Color temp = brokenScreen.GetComponent<Image>().color;
        temp.a = Mathf.Clamp(temp.a - 0.05f, 0f, 1f);
        brokenScreen.GetComponent<Image>().color = temp;
    }

    public void ComboUp()
    {
        hits += 1;
        comboTimer = comboReset;
        comboScript.ComboParticle();
    }

    public void SwordBreak(int activeWeapon)
    {
        if (activeWeapon == 1)
        {
            brokenScreen.GetComponent<Image>().sprite = brokenScreenBlue;
        }
        if (activeWeapon == 2)
        {
            brokenScreen.GetComponent<Image>().sprite = brokenScreenPink;
        }
        if (activeWeapon == 3)
        {
            brokenScreen.GetComponent<Image>().sprite = brokenScreenGreen;
        }

        Color temp = brokenScreen.GetComponent<Image>().color;
        temp.a = 1f;
        brokenScreen.GetComponent<Image>().color = temp;
    }

    public void UNGA()
    {
        StartCoroutine(LvlStart());
    }

    public IEnumerator LvlStart()
    {
        Door1.GetComponent<TransitionScript>().Close = true;
        Door2.GetComponent<TransitionScript>().Close = true;
        yield return new WaitForSeconds(1);
        Door1.GetComponent<TransitionScript>().Close = false;
        Door2.GetComponent<TransitionScript>().Close = false;
    }

    public void WeaponSwitch(int activeWeapon)
    {
        if (activeWeapon == 1)
        {
            HPCircleRotation = 0;
        }
        if (activeWeapon == 2)
        {
            HPCircleRotation = 240;
        }
        if (activeWeapon == 3)
        {
            HPCircleRotation = 120;
        }
    }

}
