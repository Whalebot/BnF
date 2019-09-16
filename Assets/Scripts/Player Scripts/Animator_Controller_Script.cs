using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Controller_Script : MonoBehaviour
{
    Player_AttackScript playerAttackScript;
    public RuntimeAnimatorController hydrangeaController;
    public RuntimeAnimatorController snowCherryController;
    public RuntimeAnimatorController bambooController;
    public RuntimeAnimatorController sickleController;
    public RuntimeAnimatorController spearController;

    Animator anim;

    // Use this for initialization
    void Start()
    {
        playerAttackScript = GetComponent<Player_AttackScript>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAttackScript.currentMovesetObject == null) anim.runtimeAnimatorController = hydrangeaController as RuntimeAnimatorController;
        else if (playerAttackScript.currentMovesetObject.name.Contains("Hydrangea")) anim.runtimeAnimatorController = hydrangeaController as RuntimeAnimatorController;
        else if (playerAttackScript.currentMovesetObject.name.Contains("Bamboo")) anim.runtimeAnimatorController = bambooController as RuntimeAnimatorController;
        else if (playerAttackScript.currentMovesetObject.name.Contains("Cherry")) anim.runtimeAnimatorController = snowCherryController as RuntimeAnimatorController;
        else if (playerAttackScript.currentMovesetObject.name.Contains("Sickle")) anim.runtimeAnimatorController = sickleController as RuntimeAnimatorController;
        else if (playerAttackScript.currentMovesetObject.name.Contains("Spear")) anim.runtimeAnimatorController = spearController as RuntimeAnimatorController;
        else anim.runtimeAnimatorController = snowCherryController as RuntimeAnimatorController;
    }
}
