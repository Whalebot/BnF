using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    public float bufferWindow;
    public float releaseWindow;
    public float switchWindow;
    public float directionalWindow;

    public float lastInput;
    public List<int> inputQueue;
    public List<int> releaseQueue;
    public List<int> weaponSwitchQueue;
    public List<int> directionalQueue;
    public int lastDirectional;

    public Player_AttackScript playerAttackScript;
    public Player_Movement playerMovement;
    bool L2Switch;
    bool R1Switch;
    public bool KB = true;
    public bool XBOX = true;
    public bool PS4;
    public float inputHorizontal;
    public float inputVertical;



    void Update()
    {

        //  if(Input.GetJoystickNames()[0].Contains("XBOX")) print(Input.GetJoystickNames()[0]);
        //  else if (Input.GetJoystickNames()[0].Contains("Wireless")) print(Input.GetJoystickNames()[0]);

        if (!PauseMenu.gameIsPaused)
        {
            if (Mathf.Abs(Input.GetAxis("KB_Horizontal")) > Mathf.Abs(Input.GetAxis("XB_Horizontal"))) { inputHorizontal = Input.GetAxis("KB_Horizontal"); }
            else inputHorizontal = Input.GetAxis("XB_Horizontal");
            if (Mathf.Abs(Input.GetAxis("KB_Vertical")) > Mathf.Abs(Input.GetAxis("XB_Vertical"))) { inputVertical = Input.GetAxis("KB_Vertical"); }
            else inputVertical = Input.GetAxis("XB_Vertical");


            if (inputHorizontal == 1 && lastDirectional != 6)
            {
                StartCoroutine("DirectionalReset", 6);
                lastDirectional = 6;
            }
            if (inputHorizontal == 0 && lastDirectional != 5)
            {
                StartCoroutine("DirectionalReset", 5);
                lastDirectional = 5;
            }
            if (inputHorizontal == -1 && lastDirectional != 4)
            {
                StartCoroutine("DirectionalReset", 4);
                lastDirectional = 4;
            }
            if (directionalQueue.Count > 2)
                if (directionalQueue[0] == 6 && directionalQueue[1] == 5 && directionalQueue[2] == 6) playerMovement.doubleTap = true;
                else if (directionalQueue[0] == 4 && directionalQueue[1] == 5 && directionalQueue[2] == 4) playerMovement.doubleTap = true;
            if (playerMovement.running && directionalQueue.Count > 0) { if (directionalQueue[0] == 5 && lastDirectional == 5) playerMovement.doubleTap = false;
            }


            if (KB)
            {
                if (Input.GetButtonDown("KB_Dash") && !Input.GetButtonDown("KB_Attack") && !Input.GetButtonDown("KB_Special") && inputHorizontal == 0) BackDash();
                if (Input.GetButtonDown("KB_Dash") && !Input.GetButtonDown("KB_Attack") && !Input.GetButtonDown("KB_Special") && inputHorizontal != 0) Dash();

                if (Input.GetButtonDown("KB_Jump")) Jump();

                //    if (Input.GetAxis("KB_Trigger") > 0 && !L2Switch) { SwitchUp(); L2Switch = true; }
                //    if (Input.GetAxis("KB_Trigger") < 0 && !L2Switch) { SwitchDown(); L2Switch = true; }

                if (Input.GetButtonDown("KB_R1") && !R1Switch) { SwitchUp(); R1Switch = true; }
                if (Input.GetButtonDown("KB_L1") && !L2Switch) { SwitchDown(); L2Switch = true; }
                if (Input.GetButtonUp("KB_R1")) R1Switch = false;
                if (Input.GetButtonUp("KB_L1")) L2Switch = false;

                if (Input.GetButtonDown("KB_Special") && Input.GetAxis("KB_Horizontal") == 0 && Input.GetAxis("KB_Vertical") == 0) NSpecial();
                if (Input.GetButtonDown("KB_Special") && Input.GetAxis("KB_Horizontal") != 0 && Input.GetAxis("KB_Vertical") == 0) NSpecial();
                if (Input.GetButtonDown("KB_Special") && Input.GetAxis("KB_Vertical") > 0) USpecial();


                if (Input.GetButtonDown("KB_Special") && Input.GetAxis("KB_Vertical") < 0) DSpecial();

                if (Input.GetButtonDown("KB_Attack") && Input.GetAxis("KB_Horizontal") == 0 && Input.GetAxis("KB_Vertical") == 0 && !Input.GetButtonDown("KB_Dash")) NAttack();
                if (Input.GetButtonDown("KB_Attack") && Input.GetAxis("KB_Horizontal") != 0 && Input.GetAxis("KB_Vertical") == 0 && !Input.GetButtonDown("KB_Dash")) SAttack();
                if (Input.GetButtonDown("KB_Attack") && Input.GetAxis("KB_Vertical") > 0 && !Input.GetButtonDown("KB_Dash")) UAttack();
                if (Input.GetButtonDown("KB_Attack") && Input.GetAxis("KB_Vertical") < 0 && !Input.GetButtonDown("KB_Dash")) DAttack();
               
                if (Input.GetButtonDown("KB_Attack") && Input.GetAxis("KB_Horizontal") == 0 && Input.GetAxis("KB_Vertical") == 0 && Input.GetButtonDown("KB_Dash")) Dash();
                if (Input.GetButtonDown("KB_Attack") && Input.GetAxis("KB_Horizontal") != 0 && Input.GetAxis("KB_Vertical") == 0 && Input.GetButtonDown("KB_Dash")) Dash();
                if (Input.GetButtonDown("KB_Attack") && Input.GetAxis("KB_Vertical") > 0 && Input.GetButtonDown("KB_Dash")) Dash();
                if (Input.GetButtonDown("KB_Attack") && Input.GetAxis("KB_Vertical") < 0 && Input.GetButtonDown("KB_Dash")) Dash();

                if (Input.GetButtonUp("KB_Attack") && Input.GetAxis("KB_Vertical") == 0 && !Input.GetButtonDown("KB_Dash")) ReleaseAttack();
            }
            if (XBOX)
            {
                if (Input.GetButtonDown("XB_Dash") && !Input.GetButtonDown("XB_Attack") && !Input.GetButtonDown("XB_Special") && inputHorizontal == 0) BackDash();
                if (Input.GetButtonDown("XB_Dash") && !Input.GetButtonDown("XB_Attack") && !Input.GetButtonDown("XB_Special") && inputHorizontal != 0) Dash();

                if (Input.GetButtonDown("XB_Jump")) Jump();

                //    if (Input.GetAxis("XB_Trigger") > 0 && !L2Switch) { SwitchUp(); L2Switch = true; }
                //    if (Input.GetAxis("XB_Trigger") < 0 && !L2Switch) { SwitchDown(); L2Switch = true; }

                if (Input.GetButtonDown("XB_R1") && !R1Switch) { SwitchUp(); R1Switch = true; }
                if (Input.GetButtonDown("XB_L1") && !L2Switch) { SwitchDown(); L2Switch = true; }
                if (Input.GetButtonUp("XB_R1")) R1Switch = false;
                if (Input.GetButtonUp("XB_L1")) L2Switch = false;

                if (Input.GetButtonDown("XB_Special") && Input.GetAxis("XB_Horizontal") == 0 && Input.GetAxis("XB_Vertical") == 0) NSpecial();
                if (Input.GetButtonDown("XB_Special") && Input.GetAxis("XB_Horizontal") != 0 && Input.GetAxis("XB_Vertical") == 0) NSpecial();
                if (Input.GetButtonDown("XB_Special") && Input.GetAxis("XB_Vertical") > 0) USpecial();


                if (Input.GetButtonDown("XB_Special") && Input.GetAxis("XB_Vertical") < 0) DSpecial();

                if (Input.GetButtonDown("XB_Attack") && Input.GetAxis("XB_Horizontal") == 0 && Input.GetAxis("XB_Vertical") == 0 && !Input.GetButtonDown("XB_Dash")) NAttack();
                if (Input.GetButtonDown("XB_Attack") && Input.GetAxis("XB_Horizontal") != 0 && Input.GetAxis("XB_Vertical") == 0 && !Input.GetButtonDown("XB_Dash")) SAttack();
                if (Input.GetButtonDown("XB_Attack") && Input.GetAxis("XB_Vertical") > 0 && !Input.GetButtonDown("XB_Dash")) UAttack();
                if (Input.GetButtonDown("XB_Attack") && Input.GetAxis("XB_Vertical") < 0 && !Input.GetButtonDown("XB_Dash")) DAttack();

                if (Input.GetButtonDown("XB_Attack") && Input.GetAxis("XB_Horizontal") == 0 && Input.GetAxis("XB_Vertical") == 0 && Input.GetButtonDown("XB_Dash")) Dash();
                if (Input.GetButtonDown("XB_Attack") && Input.GetAxis("XB_Horizontal") != 0 && Input.GetAxis("XB_Vertical") == 0 && Input.GetButtonDown("XB_Dash")) Dash();
                if (Input.GetButtonDown("XB_Attack") && Input.GetAxis("XB_Vertical") > 0 && Input.GetButtonDown("XB_Dash")) Dash();
                if (Input.GetButtonDown("XB_Attack") && Input.GetAxis("XB_Vertical") < 0 && Input.GetButtonDown("XB_Dash")) Dash();

                if (Input.GetButtonUp("XB_Attack") && Input.GetAxis("XB_Vertical") == 0 && !Input.GetButtonDown("XB_Dash")) ReleaseAttack();
            }
            else if (PS4)
            {
                if (Input.GetButtonDown("Dash") && !Input.GetButtonDown("Attack") && !Input.GetButtonDown("Special")) Dash();
                if (Input.GetButtonDown("Jump")) Jump();

                if (Input.GetAxis("L2") > 0 && !L2Switch) { SwitchUp(); L2Switch = true; }
                if (Input.GetAxis("L2") < 0 && !L2Switch) { SwitchDown(); L2Switch = true; }
                if (Input.GetAxis("L2") == 0) L2Switch = false;

                if (Input.GetButtonDown("Special") && Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) NSpecial();
                if (Input.GetButtonDown("Special") && Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") == 0) SSpecial();
                if (Input.GetButtonDown("Special") && Input.GetAxis("Vertical") > 0) USpecial();


                if (Input.GetButtonDown("Special") && Input.GetAxis("Vertical") < 0) DSpecial();

                if (Input.GetButtonDown("Attack") && Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && !Input.GetButtonDown("Dash")) NAttack();
                if (Input.GetButtonDown("Attack") && Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") == 0 && !Input.GetButtonDown("Dash")) SAttack();
                if (Input.GetButtonDown("Attack") && Input.GetAxis("Vertical") > 0 && !Input.GetButtonDown("Dash")) UAttack();
                if (Input.GetButtonDown("Attack") && Input.GetAxis("Vertical") < 0 && !Input.GetButtonDown("Dash")) DAttack();

                if (Input.GetButtonDown("Attack") && Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && Input.GetButtonDown("Dash")) ENAttack();
                if (Input.GetButtonDown("Attack") && Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") == 0 && Input.GetButtonDown("Dash")) ESAttack();
                if (Input.GetButtonDown("Attack") && Input.GetAxis("Vertical") > 0 && Input.GetButtonDown("Dash")) EUAttack();
                if (Input.GetButtonDown("Attack") && Input.GetAxis("Vertical") < 0 && Input.GetButtonDown("Dash")) EDAttack();
            }
        }
    }

    public void ResetBufferQueue() {
        StopAllCoroutines();
        inputQueue.Clear();
    }

    IEnumerator BufferReset(int inputID)
    {
        inputQueue.Add(inputID);
        for (int i = 0; i < bufferWindow; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        if (inputQueue.Count > 0)
            inputQueue.RemoveAt(0);
    }

    IEnumerator ReleaseReset(int inputID)
    {
        releaseQueue.Add(inputID);
        for (int i = 0; i < releaseWindow; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        if (releaseQueue.Count > 0)
            releaseQueue.RemoveAt(0);
    }

    IEnumerator DirectionalReset(int inputID)
    {
        directionalQueue.Add(inputID);
        for (int i = 0; i < directionalWindow; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        if (directionalQueue.Count > 0)
            directionalQueue.RemoveAt(0);
    }

    IEnumerator WeaponSwitchReset(int inputID)
    {
        weaponSwitchQueue.Add(inputID);
        for (int i = 0; i < switchWindow; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        if (weaponSwitchQueue.Count > 0)
            weaponSwitchQueue.RemoveAt(0);
    }

    void Jump() { StartCoroutine("BufferReset", 0); }
    void Dash() { StartCoroutine("BufferReset", 1); }
    void BackDash() { StartCoroutine("BufferReset", 2); }


    void NAttack() { StartCoroutine("BufferReset", 3); }
    void SAttack() { StartCoroutine("BufferReset", 9); }
    void UAttack() { StartCoroutine("BufferReset", 4); }
    void DAttack() { StartCoroutine("BufferReset", 5); }

    void NSpecial() { StartCoroutine("BufferReset", 6); }
    void USpecial() { StartCoroutine("BufferReset", 7); }
    void DSpecial() { StartCoroutine("BufferReset", 8); }


    void SwitchUp() { StartCoroutine("WeaponSwitchReset", 1); }
    void SwitchDown() { StartCoroutine("WeaponSwitchReset", 2); }



    void SpecialHold() { StartCoroutine("BufferReset", 9); }
    void SpecialRelease() { StartCoroutine("BufferReset", 9); }
    void SSpecial() { StartCoroutine("BufferReset", 9); }

    void ReleaseAttack() { StartCoroutine("ReleaseReset", 1); }
    void ReleaseUAttack() { StartCoroutine("ReleaseReset", 2); print("released"); }
    void ReleaseDAttack() { StartCoroutine("ReleaseReset", 3); print("released"); }


    void ENAttack() { StartCoroutine("BufferReset", 9); }
    void ESAttack() { StartCoroutine("BufferReset", 9); }
    void EUAttack() { print("Neutral attack"); StartCoroutine("BufferReset", 9); }
    void EDAttack() { StartCoroutine("BufferReset", 10); }



}
