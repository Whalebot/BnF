using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveset_Sickle : MonoBehaviour {

    [Header("Moves")]
    public GameObject attack1;
    public GameObject attack2;

    public GameObject air1;
    public GameObject air2;
    public GameObject upAttack;
    public GameObject up2Attack;
    public GameObject downAttack;
    public GameObject down2Attack;
    public GameObject special;

    [HeaderAttribute("Dash attributes")]
    [Space(10)]
    public float dashSpeed;
    public float dashDuration;
    public float dashRecovery;
    public bool canElectric;
    public float electricCost;
    [HeaderAttribute("Special cost")]
    public int specialCost;
    public bool hasiFrames;
    [Header("Special attack")]
    public int specialID;
    public float specialStartUp;
    public float specialActive;
    public float specialRecovery;
    [Space(5)]
    public float specialForward;
    public float specialUp;
    public float specialDuration1;
    [Space(5)]
    public float specialForward2;
    public float specialUp2;
    public float specialDuration2;
    [Space(5)]
    public float specialForward3;
    public float specialUp3;
    public float specialDuration3;
    [Space(10)]
    [Header("Attack 1")]
    public int attack1ID;
    public float attack1StartUp;
    public float attack1Active;
    public float attack1Recovery;
    [Space(5)]
    public float attack1Forward;
    public float attack1Up;
    public float attack1Duration1;
    [Space(5)]
    public float attack1Forward2;
    public float attack1Up2;
    public float attack1Duration2;
    [Space(5)]
    public float attack1Forward3;
    public float attack1Up3;
    public float attack1Duration3;
    [Space(10)]
    [Header("Attack 2")]
    public int attack2ID;
    public float attack2StartUp;
    public float attack2Active;
    public float attack2Recovery;
    [Space(5)]
    public float attack2Forward;
    public float attack2Up;
    public float attack2Duration1;
    [Space(5)]
    public float attack2Forward2;
    public float attack2Up2;
    public float attack2Duration2;
    [Space(5)]
    public float attack2Forward3;
    public float attack2Up3;
    public float attack2Duration3;
    [Space(10)]

    [Header("Air attack 1")]
    public int air1ID;
    public float air1StartUp;
    public float air1Active;
    public float air1Recovery;
    [Space(5)]
    public float air1Forward;
    public float air1Up;
    public float air1Duration1;
    [Space(5)]
    public float air1Forward2;
    public float air1Up2;
    public float air1Duration2;
    [Space(5)]
    public float air1Forward3;
    public float air1Up3;
    public float air1Duration3;
    [Space(10)]

    [Header("Air attack 2")]
    public int air2ID;
    public float air2StartUp;
    public float air2Active;
    public float air2Recovery;
    [Space(5)]
    public float air2Forward;
    public float air2Up;
    public float air2Duration1;
    [Space(5)]
    public float air2Forward2;
    public float air2Up2;
    public float air2Duration2;
    [Space(5)]
    public float air2Forward3;
    public float air2Up3;
    public float air2Duration3;
    [Space(10)]


    [Header("Up attack")]
    public int upID;
    public float upAttackStartUp;
    public float upAttackActive;
    public float upAttackRecovery;
    [Space(5)]
    public float upAttackForward;
    public float upAttackUp;
    public float upAttackDuration1;
    [Space(5)]
    public float upAttackForward2;
    public float upAttackUp2;
    public float upAttackDuration2;
    [Space(5)]
    public float upAttackForward3;
    public float upAttackUp3;
    public float upAttackDuration3;
    [Space(10)]
    [Header("Up attack")]
    public int up2ID;
    public float up2AttackStartUp;
    public float up2AttackActive;
    public float up2AttackRecovery;
    [Space(5)]
    public float up2AttackForward;
    public float up2AttackUp;
    public float up2AttackDuration1;
    [Space(5)]
    public float up2AttackForward2;
    public float up2AttackUp2;
    public float up2AttackDuration2;
    [Space(5)]
    public float up2AttackForward3;
    public float up2AttackUp3;
    public float up2AttackDuration3;
    [Space(10)]
    [Header("Down attack")]
    public int downID;
    public float downAttackStartUp;
    public float downAttackActive;
    public float downAttackRecovery;
    [Space(5)]
    public float downAttackForward;
    public float downAttackUp;
    public float downAttackDuration1;
    [Space(5)]
    public float downAttackForward2;
    public float downAttackUp2;
    public float downAttackDuration2;
    [Space(5)]
    public float downAttackForward3;
    public float downAttackUp3;
    public float downAttackDuration3;
    [Header("Down2 attack")]
    public int down2ID;
    public float down2AttackStartUp;
    public float down2AttackActive;
    public float down2AttackRecovery;
    [Space(5)]
    public float down2AttackForward;
    public float down2AttackUp;
    public float down2AttackDuration1;
    [Space(5)]
    public float down2AttackForward2;
    public float down2AttackUp2;
    public float down2AttackDuration2;
    [Space(5)]
    public float down2AttackForward3;
    public float down2AttackUp3;
    public float down2AttackDuration3;

}