using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAssigner : MonoBehaviour
{
    public GameObject moveset1;
    public GameObject moveset2;
    public GameObject moveset3;
    public GameObject moveset4;
    public GameObject moveset5;
    public GameObject moveset6;
    public GameObject moveset7;
    public GameObject moveset8;
    public GameObject moveset9;
    public GameObject moveset10;
    public GameObject moveset11;
    public GameObject moveset12;

    public int weapon1;
    public int weapon2;
    public int weapon3;
    public bool levelSelect = true;

    public GameObject player;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (levelSelect)
        {
            weapon1 = GameDataManager.weaponSlot1;
            weapon2 = GameDataManager.weaponSlot2;
            weapon3 = GameDataManager.weaponSlot3;
        }
        else
        {
            GameDataManager.weaponSlot1 = weapon1;
            GameDataManager.weaponSlot2 = weapon2;
            GameDataManager.weaponSlot3 = weapon3;
        }
        
        if (weapon1 == 1) player.GetComponent<Player_AttackScript>().moveset1 = moveset1;
        if (weapon1 == 2) player.GetComponent<Player_AttackScript>().moveset1 = moveset2;
        if (weapon1 == 3) player.GetComponent<Player_AttackScript>().moveset1 = moveset3;
        if (weapon1 == 4) player.GetComponent<Player_AttackScript>().moveset1 = moveset4;
        if (weapon1 == 5) player.GetComponent<Player_AttackScript>().moveset1 = moveset5;
        if (weapon1 == 6) player.GetComponent<Player_AttackScript>().moveset1 = moveset6;
        if (weapon1 == 7) player.GetComponent<Player_AttackScript>().moveset1 = moveset7;
        if (weapon1 == 8) player.GetComponent<Player_AttackScript>().moveset1 = moveset8;
        if (weapon1 == 9) player.GetComponent<Player_AttackScript>().moveset1 = moveset9;
        if (weapon1 == 10) player.GetComponent<Player_AttackScript>().moveset1 = moveset10;
        if (weapon1 == 11) player.GetComponent<Player_AttackScript>().moveset1 = moveset11;
        if (weapon1 == 12) player.GetComponent<Player_AttackScript>().moveset1 = moveset12;

        if (weapon2 == 1) player.GetComponent<Player_AttackScript>().moveset2 = moveset1;
        if (weapon2 == 2) player.GetComponent<Player_AttackScript>().moveset2 = moveset2;
        if (weapon2 == 3) player.GetComponent<Player_AttackScript>().moveset2 = moveset3;
        if (weapon2 == 4) player.GetComponent<Player_AttackScript>().moveset2 = moveset4;
        if (weapon2 == 5) player.GetComponent<Player_AttackScript>().moveset2 = moveset5;
        if (weapon2 == 6) player.GetComponent<Player_AttackScript>().moveset2 = moveset6;
        if (weapon2 == 7) player.GetComponent<Player_AttackScript>().moveset2 = moveset7;
        if (weapon2 == 8) player.GetComponent<Player_AttackScript>().moveset2 = moveset8;
        if (weapon2 == 9) player.GetComponent<Player_AttackScript>().moveset2 = moveset9;
        if (weapon2 == 10) player.GetComponent<Player_AttackScript>().moveset2 = moveset10;
        if (weapon2 == 11) player.GetComponent<Player_AttackScript>().moveset2 = moveset11;
        if (weapon2 == 12) player.GetComponent<Player_AttackScript>().moveset2 = moveset12;

        if (weapon3 == 1) player.GetComponent<Player_AttackScript>().moveset3 = moveset1;
        if (weapon3 == 2) player.GetComponent<Player_AttackScript>().moveset3 = moveset2;
        if (weapon3 == 3) player.GetComponent<Player_AttackScript>().moveset3 = moveset3;
        if (weapon3 == 4) player.GetComponent<Player_AttackScript>().moveset3 = moveset4;
        if (weapon3 == 5) player.GetComponent<Player_AttackScript>().moveset3 = moveset5;
        if (weapon3 == 6) player.GetComponent<Player_AttackScript>().moveset3 = moveset6;
        if (weapon3 == 7) player.GetComponent<Player_AttackScript>().moveset3 = moveset7;
        if (weapon3 == 8) player.GetComponent<Player_AttackScript>().moveset3 = moveset8;
        if (weapon3 == 9) player.GetComponent<Player_AttackScript>().moveset3 = moveset9;
        if (weapon3 == 10) player.GetComponent<Player_AttackScript>().moveset3 = moveset10;
        if (weapon3 == 11) player.GetComponent<Player_AttackScript>().moveset3 = moveset11;
        if (weapon3 == 12) player.GetComponent<Player_AttackScript>().moveset3 = moveset12;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
