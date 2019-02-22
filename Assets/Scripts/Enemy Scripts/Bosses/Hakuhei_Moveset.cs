using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hakuhei_Moveset : MonoBehaviour {

    [Header("Moves")]
    public GameObject attack1;
    public GameObject attack2;
    public GameObject attack3;
    public GameObject air1;
    public GameObject air2;
    public GameObject DP;
    public GameObject upDash;
    public GameObject downDash;
    public GameObject downSlash;
    public GameObject dashDownSlash;
    public GameObject special;
    public GameObject feint;

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
    public float specialStartUp;     public float specialActive;     public float specialRecovery;
    [Space(5)]
    public float specialForward;    public float specialUp;    public float specialDuration1;
    [Space(5)]
    public float specialForward2;     public float specialUp2;    public float specialDuration2;
    [Space(5)]
    public float specialForward3;    public float specialUp3;    public float specialDuration3;
    [Space(10)]
    [Header("Feint attack")]
    public int feintID;
    public float feintStartUp; public float feintActive; public float feintRecovery;
    [Space(5)]
    public float feintForward; public float feintUp; public float feintDuration1;
    [Space(5)]
    public float feintForward2; public float feintUp2; public float feintDuration2;
    [Space(5)]
    public float feintForward3; public float feintUp3; public float feintDuration3;
    [Space(10)]
    [Header("Attack 1")]
    public int attack1ID;    public float attack1StartUp;    public float attack1Active;    public float attack1Recovery;    [Space(5)]    public float attack1Forward;    public float attack1Up;    public float attack1Duration1;
    [Space(5)]
    public float attack1Forward2;    public float attack1Up2;    public float attack1Duration2;
    [Space(5)]
    public float attack1Forward3;    public float attack1Up3;    public float attack1Duration3;
    [Space(10)]
    [Header("Attack 2")]
    public int attack2ID;    public float attack2StartUp;    public float attack2Active;    public float attack2Recovery;
    [Space(5)]
    public float attack2Forward;    public float attack2Up;    public float attack2Duration1;
    [Space(5)]
    public float attack2Forward2;    public float attack2Up2;    public float attack2Duration2;
    [Space(5)]
    public float attack2Forward3;    public float attack2Up3;    public float attack2Duration3;
    [Space(10)]
    [Header("Attack 3")]
    public int attack3ID;    public float attack3StartUp;    public float attack3Active;    public float attack3Recovery;
    [Space(5)]
    public float attack3Forward;    public float attack3Up;    public float attack3Duration1;
    [Space(5)]
    public float attack3Forward2;    public float attack3Up2;    public float attack3Duration2;
    [Space(5)]
    public float attack3Forward3;    public float attack3Up3;    public float attack3Duration3;
    [Space(10)]
    [Header("Air attack")]
    public int air1ID;    public float air1StartUp;    public float air1Active;    public float air1Recovery;
    [Space(5)]
    public float air1Forward;    public float air1Up;    public float air1Duration1;
    [Space(5)]
    public float air1Forward2;    public float air1Up2;    public float air1Duration2;
    [Space(5)]
    public float air1Forward3;    public float air1Up3;    public float air1Duration3;
    [Space(10)]
    [Header("Air attack 2")]
    public int air2ID;    public float air2StartUp;    public float air2Active;    public float air2Recovery;
    [Space(5)]
    public float air2Forward;    public float air2Up;    public float air2Duration1;
    [Space(5)]
    public float air2Forward2;    public float air2Up2;    public float air2Duration2;
    [Space(5)]
    public float air2Forward3;    public float air2Up3;    public float air2Duration3;
    [Space(10)]
    [Header("Dragon punch")]
    public int DPID;    public float DPStartUp;    public float DPActive;    public float DPRecovery;
    [Space(5)]
    public float DPForward;    public float DPUp;    public float DPDuration1;
    [Space(5)]
    public float DPForward2;    public float DPUp2;    public float DPDuration2;
    [Space(5)]
    public float DPForward3;    public float DPUp3;    public float DPDuration3;
    [Space(10)]
    [Header("Up dash")]
    public int upID;    public float upDashStartUp;    public float upDashActive;    public float upDashRecovery;
    [Space(5)]
    public float upDashForward;    public float upDashUp;    public float upDashDuration1;
    [Space(5)]
    public float upDashForward2;    public float upDashUp2;    public float upDashDuration2;
    [Space(5)]
    public float upDashForward3;    public float upDashUp3;    public float upDashDuration3;
    [Space(10)]
    [Header("Down dash")]
    public int downID;    public float downDashStartUp;    public float downDashActive;    public float downDashRecovery;
    [Space(5)]
    public float downDashForward;    public float downDashUp;    public float downDashDuration1;
    [Space(5)]
    public float downDashForward2;    public float downDashUp2;    public float downDashDuration2;
    [Space(5)]
    public float downDashForward3;    public float downDashUp3;    public float downDashDuration3;
    [Space(10)]
    [Header("Down slash")]
    public int downSlashID; public float downSlashStartUp; public float downSlashActive; public float downSlashRecovery;
    [Space(5)]
    public float downSlashForward; public float downSlashUp; public float downSlashDuration1;
    [Space(5)]
    public float downSlashForward2; public float downSlashUp2; public float downSlashDuration2;
    [Space(5)]
    public float downSlashForward3; public float downSlashUp3; public float downSlashDuration3;
    [Header("Dash down slash")]
    public int dashDownSlashID; public float dashDownSlashStartUp; public float dashDownSlashActive; public float dashDownSlashRecovery;
    [Space(5)]
    public float dashDownSlashForward; public float dashDownSlashUp; public float dashDownSlashDuration1;
    [Space(5)]
    public float dashDownSlashForward2; public float dashDownSlashUp2; public float dashDownSlashDuration2;
    [Space(5)]
    public float dashDownSlashForward3; public float dashDownSlashUp3; public float dashDownSlashDuration3;
}
