using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public GameObject blades;
    public List<int> selectedWeapons;
    public int currentNR;
    public int swordID = 1;
    public bool stageSelected;
    public float waitTime;
    public int waitCounter;
    public int waitTimeInt;
    bool wait;
    LoadingScreen loadingScreen;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        loadingScreen = GameObject.FindGameObjectWithTag("Manager").GetComponent<LoadingScreen>();
        anim = blades.GetComponent<Animator>();
        //  Blades = GameObject.Find("Blades");
        currentNR = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("KB_Vertical") < 0)
        {
            //      Blades.GetComponent<RotateSelect>().ScrollUp();
        }
        if (Input.GetAxis("KB_Vertical") > 0)
        {
            //     Blades.GetComponent<RotateSelect>().ScrollDown();
        }
        /* if (!stageSelected)
         {
             if (wait != true)
             {
                 if (Input.GetAxis("KB_Horizontal") < 0)
                 {
                     if (currentNR == 1)
                     {
                         currentNR = 12;
                         wait = true;
                         StartCoroutine(Wait());
                         return;
                     }
                     currentNR -= 1;
                     wait = true;
                     StartCoroutine(Wait());
                 }
                 if (Input.GetAxis("KB_Horizontal") > 0)
                 {
                     if (currentNR == 12)
                     {
                         currentNR = 1;
                         wait = true;
                         StartCoroutine(Wait());
                         return;
                     }
                     currentNR += 1;
                     wait = true;
                     StartCoroutine(Wait());
                 }
             }
             if (Input.GetButtonDown("Submit"))
             {
                 stageSelected = true;
                 //Blades.GetComponent<RotateSelect> ().Select ();
             }
         }
         else
         if (stageSelected)
         {*/
        if (wait != true)
            {
                if (Input.GetAxis("KB_Horizontal") < 0)
                {
                    if (swordID == 1)
                    {
                        swordID = 11;
                        wait = true;
                        StartCoroutine(Wait());
                        return;
                    }
                    BladeRotation(-1);
                    swordID -= 1;
                    wait = true;
                    StartCoroutine(Wait());
                }
                if (Input.GetAxis("KB_Horizontal") > 0)
                {
                    if (swordID == 11)
                    {
                        swordID = 1;
                        wait = true;
                        StartCoroutine(Wait());
                        return;
                    }
                    BladeRotation(1);
                    swordID += 1;
                    wait = true;
                    StartCoroutine(Wait());
                }
            }
            if (Input.GetButtonDown("Submit"))
            {
                if (selectedWeapons.Count < 3)
                {
                    if (!selectedWeapons.Contains(swordID)) selectedWeapons.Add(swordID);
                    else print("Can't select twice");
                }

                else
                {
                    print("Start");
                    SelectWeapons();
                }
                //Blades.GetComponent<RotateSelect> ().Select ();
            }
            if (Input.GetButtonDown("Cancel"))
            {
                if (selectedWeapons.Count > 0) selectedWeapons.RemoveAt(selectedWeapons.Count - 1);
                else { print("Can't remove"); stageSelected = true; }
                //Blades.GetComponent<RotateSelect> ().Select ();
            }
     //   }
    }

    void BladeRotation(int i)
    {
        //    blades.transform.Rotate(0, 0, i*30);
        StartCoroutine("RotateSword", i);
    }

    void SelectWeapons()
    {
        GameDataManager.weaponSlot1 = selectedWeapons[0];
        GameDataManager.weaponSlot2 = selectedWeapons[1];
        GameDataManager.weaponSlot3 = selectedWeapons[2];
        loadingScreen.StartCoroutine("LoadAsync", 2);
    }

    IEnumerator RotateSword(int i)
    {
        while (waitTimeInt > waitCounter) { waitCounter++; blades.transform.Rotate(0, 0, i * 30F/waitTimeInt); yield return new WaitForEndOfFrame(); }
        waitCounter = 0;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        wait = false;
    }
}
