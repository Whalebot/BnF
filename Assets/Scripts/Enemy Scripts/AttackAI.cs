using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAI : MonoBehaviour
{
    public bool isGroundToGround;
    public bool isGroundToAir;
    public bool isAirToGround;
    public bool isAirToAir;

    public int groundToGroundRNG;
    public int groundToAirRNG;
    public int airToGroundRNG;
    public int airToAirRNG;

    public AttackState groundToGround;
    public AttackState groundToAir;
    public AttackState airToGround;
    public AttackState airToAir;


}