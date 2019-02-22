using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatusBar : MonoBehaviour {
    public int weaponOrder;
    public Image HPImage;
    public Sprite HPSprite0;
    public Sprite HPSprite1;
    public Sprite HPSprite2;
    public Sprite HPSprite3;
    PlayerStatus playerStatus;
    Animator anim;
    // Use this for initialization
    void Start () {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playerStatus.activeWeapon == weaponOrder) anim.SetBool("Selected", true);
        else anim.SetBool("Selected", false);

        if (weaponOrder == 1)
        {
            if (playerStatus.health == 3) HPImage.sprite = HPSprite3;
            if (playerStatus.health == 2) HPImage.sprite = HPSprite2;
            if (playerStatus.health == 1) HPImage.sprite = HPSprite1;
            if (playerStatus.health == 0) HPImage.sprite = HPSprite0;
        }
        else if (weaponOrder == 2) {
            if (playerStatus.health2 == 3) HPImage.sprite = HPSprite3;
            if (playerStatus.health2 == 2) HPImage.sprite = HPSprite2;
            if (playerStatus.health2 == 1) HPImage.sprite = HPSprite1;
            if (playerStatus.health2 == 0) HPImage.sprite = HPSprite0;
        }
        else if (weaponOrder == 3)
        {
            if (playerStatus.health3 == 3) HPImage.sprite = HPSprite3;
            if (playerStatus.health3 == 2) HPImage.sprite = HPSprite2;
            if (playerStatus.health3 == 1) HPImage.sprite = HPSprite1;
            if (playerStatus.health3 == 0) HPImage.sprite = HPSprite0;
        }
    }
}
