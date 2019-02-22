using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Move", menuName = "Attack moves")]
public class Move : ScriptableObject{
    [HeaderAttribute("General")]
    public int ID;
    public int SpCost;
    public int combo;

    [HeaderAttribute("Frame data")]
    public int startUp;
    public int active;
    public int recovery;

    [HeaderAttribute("Movement attributes")]

    public int forward1;
    public int up1;
    public int duration1;
    [Space(10)]
    public int forward2;
    public int up2;
    public int duration2;
    [Space(10)]
    public int forward3;
    public int up3;
    public int duration3;

    [HeaderAttribute("Move properties")]
    public GameObject startupSound;
    public bool jumpCancelable = true;
    public bool specialCancelable = true;
    public bool landCancel = true;
    public bool attackCancelable = true;
    public bool landCancelRecovery;
    public int landAttackFrames;
    public bool iFrames;
    public bool invul;
    public bool noClip;
    public bool isChargeAttack;
    public int justFrameTiming = 22;

    [HeaderAttribute("Movement type")]

    public bool canMove;
    public bool keepVel;
    public bool keepVerticalVel;
    public bool keepHorizontalVel;
    public bool interpolate;
    public bool isHoming;
}
