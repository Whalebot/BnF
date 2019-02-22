using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Assigner : MonoBehaviour
{
    public GameObject statusBar1;
    public GameObject statusBar2;
    public GameObject statusBar3;
    public GameObject statusBar4;
    public GameObject statusBar5;
    public GameObject statusBar6;
    public GameObject statusBar7;
    public GameObject statusBar8;
    public GameObject statusBar9;
    public GameObject statusBar10;
    public GameObject statusBar11;
    public GameObject statusBar12;



    public Image circleSR1;
    public Image circleSR2;
    public Image circleSR3;
    public Sprite circle1;
    public Sprite circle2;
    public Sprite circle3;
    public Sprite circle4;
    public Sprite circle5;
    public Sprite circle6;
    public Sprite circle7;
    public Sprite circle8;
    public Sprite circle9;
    public Sprite circle10;
    public Sprite circle11;
    public Sprite circle12;


    // Use this for initialization
    void Start()
    {

        if (GameDataManager.weaponSlot1 == 1) { circleSR1.sprite = circle1; statusBar1.SetActive(true); statusBar1.GetComponent<UI_StatusBar>().weaponOrder = 1; }
        if (GameDataManager.weaponSlot1 == 2) { circleSR1.sprite = circle2; statusBar2.SetActive(true); statusBar2.GetComponent<UI_StatusBar>().weaponOrder = 1; }
        if (GameDataManager.weaponSlot1 == 3) { circleSR1.sprite = circle3; statusBar3.SetActive(true); statusBar3.GetComponent<UI_StatusBar>().weaponOrder = 1; }
        if (GameDataManager.weaponSlot1 == 4) { circleSR1.sprite = circle4; statusBar4.SetActive(true); statusBar4.GetComponent<UI_StatusBar>().weaponOrder = 1; }
        if (GameDataManager.weaponSlot1 == 5) { circleSR1.sprite = circle5; statusBar5.SetActive(true); statusBar5.GetComponent<UI_StatusBar>().weaponOrder = 1; }
        if (GameDataManager.weaponSlot1 == 6) { circleSR1.sprite = circle6; statusBar6.SetActive(true); statusBar6.GetComponent<UI_StatusBar>().weaponOrder = 1; }
        if (GameDataManager.weaponSlot1 == 7) { circleSR1.sprite = circle7; statusBar7.SetActive(true); statusBar7.GetComponent<UI_StatusBar>().weaponOrder = 1; }
        if (GameDataManager.weaponSlot1 == 8) { circleSR1.sprite = circle8; statusBar8.SetActive(true); statusBar8.GetComponent<UI_StatusBar>().weaponOrder = 1; }
        if (GameDataManager.weaponSlot1 == 9) { circleSR1.sprite = circle9; statusBar9.SetActive(true); statusBar9.GetComponent<UI_StatusBar>().weaponOrder = 1; }
        if (GameDataManager.weaponSlot1 == 10) { circleSR1.sprite = circle10; statusBar10.SetActive(true); statusBar10.GetComponent<UI_StatusBar>().weaponOrder = 1; }
        if (GameDataManager.weaponSlot1 == 11) { circleSR1.sprite = circle11; statusBar11.SetActive(true); statusBar11.GetComponent<UI_StatusBar>().weaponOrder = 1; }
        if (GameDataManager.weaponSlot1 == 12) { circleSR1.sprite = circle12; statusBar12.SetActive(true); statusBar12.GetComponent<UI_StatusBar>().weaponOrder = 1; }


        if (GameDataManager.weaponSlot2 == 1) { circleSR2.sprite = circle1; statusBar1.SetActive(true); statusBar1.GetComponent<UI_StatusBar>().weaponOrder = 2; }
        if (GameDataManager.weaponSlot2 == 2) { circleSR2.sprite = circle2; statusBar2.SetActive(true); statusBar2.GetComponent<UI_StatusBar>().weaponOrder = 2; }
        if (GameDataManager.weaponSlot2 == 3) { circleSR2.sprite = circle3; statusBar3.SetActive(true); statusBar3.GetComponent<UI_StatusBar>().weaponOrder = 2; }
        if (GameDataManager.weaponSlot2 == 4) { circleSR2.sprite = circle4; statusBar4.SetActive(true); statusBar4.GetComponent<UI_StatusBar>().weaponOrder = 2; }
        if (GameDataManager.weaponSlot2 == 5) { circleSR2.sprite = circle5; statusBar5.SetActive(true); statusBar5.GetComponent<UI_StatusBar>().weaponOrder = 2; }
        if (GameDataManager.weaponSlot2 == 6) { circleSR2.sprite = circle6; statusBar6.SetActive(true); statusBar6.GetComponent<UI_StatusBar>().weaponOrder = 2; }
        if (GameDataManager.weaponSlot2 == 7) { circleSR2.sprite = circle7; statusBar7.SetActive(true); statusBar7.GetComponent<UI_StatusBar>().weaponOrder = 2; }
        if (GameDataManager.weaponSlot2 == 8) { circleSR2.sprite = circle8; statusBar8.SetActive(true); statusBar8.GetComponent<UI_StatusBar>().weaponOrder = 2; }
        if (GameDataManager.weaponSlot2 == 9) { circleSR2.sprite = circle9; statusBar9.SetActive(true); statusBar9.GetComponent<UI_StatusBar>().weaponOrder = 2; }
        if (GameDataManager.weaponSlot2 == 10) { circleSR2.sprite = circle10; statusBar10.SetActive(true); statusBar10.GetComponent<UI_StatusBar>().weaponOrder = 2; }
        if (GameDataManager.weaponSlot2 == 11) { circleSR2.sprite = circle11; statusBar11.SetActive(true); statusBar11.GetComponent<UI_StatusBar>().weaponOrder = 2; }
        if (GameDataManager.weaponSlot2 == 12) { circleSR2.sprite = circle12; statusBar12.SetActive(true); statusBar12.GetComponent<UI_StatusBar>().weaponOrder = 2; }



        if (GameDataManager.weaponSlot3 == 1) { circleSR3.sprite = circle1; statusBar1.SetActive(true); statusBar1.GetComponent<UI_StatusBar>().weaponOrder = 3; }
        if (GameDataManager.weaponSlot3 == 2) { circleSR3.sprite = circle2; statusBar2.SetActive(true); statusBar2.GetComponent<UI_StatusBar>().weaponOrder = 3; }
        if (GameDataManager.weaponSlot3 == 3) { circleSR3.sprite = circle3; statusBar3.SetActive(true); statusBar3.GetComponent<UI_StatusBar>().weaponOrder = 3; }
        if (GameDataManager.weaponSlot3 == 4) { circleSR3.sprite = circle4; statusBar4.SetActive(true); statusBar4.GetComponent<UI_StatusBar>().weaponOrder = 3; }
        if (GameDataManager.weaponSlot3 == 5) { circleSR3.sprite = circle5; statusBar5.SetActive(true); statusBar5.GetComponent<UI_StatusBar>().weaponOrder = 3; }
        if (GameDataManager.weaponSlot3 == 6) { circleSR3.sprite = circle6; statusBar6.SetActive(true); statusBar6.GetComponent<UI_StatusBar>().weaponOrder = 3; }
        if (GameDataManager.weaponSlot3 == 7) { circleSR3.sprite = circle7; statusBar7.SetActive(true); statusBar7.GetComponent<UI_StatusBar>().weaponOrder = 3; }
        if (GameDataManager.weaponSlot3 == 8) { circleSR3.sprite = circle8; statusBar8.SetActive(true); statusBar8.GetComponent<UI_StatusBar>().weaponOrder = 3; }
        if (GameDataManager.weaponSlot3 == 9) { circleSR3.sprite = circle9; statusBar9.SetActive(true); statusBar9.GetComponent<UI_StatusBar>().weaponOrder = 3; }
        if (GameDataManager.weaponSlot3 == 10) { circleSR3.sprite = circle10; statusBar10.SetActive(true); statusBar10.GetComponent<UI_StatusBar>().weaponOrder = 3; }
        if (GameDataManager.weaponSlot3 == 11) { circleSR3.sprite = circle11; statusBar11.SetActive(true); statusBar11.GetComponent<UI_StatusBar>().weaponOrder = 3; }
        if (GameDataManager.weaponSlot3 == 12) { circleSR3.sprite = circle12; statusBar12.SetActive(true); statusBar12.GetComponent<UI_StatusBar>().weaponOrder = 3; }

     
    }
}
