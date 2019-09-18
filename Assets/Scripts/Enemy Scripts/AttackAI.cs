using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAI : MonoBehaviour
{
    public int state;

    public bool isGroundToGround;
    public bool isGroundToAir;
    public bool isAirToGround;
    public bool isAirToAir;

    [HideInInspector]
    public int[] groundToGroundRNG;
    [HideInInspector]
    public int[] groundToAirRNG;
    [HideInInspector]
    public int[] airToGroundRNG;
    [HideInInspector]
    public int[] airToAirRNG;

    [HideInInspector]
    public int[] groundToGroundRanges;
    [HideInInspector]
    public int[] groundToAirRanges;
    [HideInInspector]
    public int[] airToGroundRanges;
    [HideInInspector]
    public int[] airToAirRanges;

    public AttackState groundToGround;
    public AttackState groundToAir;
    public AttackState airToGround;
    public AttackState airToAir;

    private void Start()
    {
        GroundToGroundSetup();
        GroundToAirSetup();
        AirToGroundSetup();
        AirToAirSetup();
    }

    void GroundToGroundSetup()
    {
        groundToGroundRanges = new int[groundToGround.ranges.Count];
        groundToGroundRNG = new int[groundToGround.ranges.Count];
        groundToGround.withinRanges.Clear();

        for (int h = 0; h < groundToGround.ranges.Count; h++)
        {
            groundToGround.withinRanges.Add(false);
            for (int i = 0; i < groundToGround.moveSequences.Count; i++)
            {
                if (h == groundToGround.moveSequences[i].rangeID)
                {
                    groundToGroundRNG[h] += groundToGround.moveSequences[i].RNGWeight;
                    groundToGroundRanges[h]++;
                }
            }
        }
    }
    void GroundToAirSetup()
    {
        groundToAirRanges = new int[groundToAir.ranges.Count];
        groundToAirRNG = new int[groundToAir.ranges.Count];
        groundToAir.withinRanges.Clear();

        for (int h = 0; h < groundToAir.ranges.Count; h++)
        {
            groundToAir.withinRanges.Add(false);
            for (int i = 0; i < groundToAir.moveSequences.Count; i++)
            {
                if (h == groundToAir.moveSequences[i].rangeID)
                {
                    groundToAirRNG[h] += groundToAir.moveSequences[i].RNGWeight;
                    groundToAirRanges[h]++;
                }
            }
        }
    }
    void AirToGroundSetup()
    {
        airToGroundRanges = new int[airToGround.ranges.Count];
        airToGroundRNG = new int[airToGround.ranges.Count];
        airToGround.withinRanges.Clear();

        for (int h = 0; h < airToGround.ranges.Count; h++)
        {
            airToGround.withinRanges.Add(false);
            for (int i = 0; i < airToGround.moveSequences.Count; i++)
            {
                if (h == airToGround.moveSequences[i].rangeID)
                {
                    airToGroundRNG[h] += airToGround.moveSequences[i].RNGWeight;
                    airToGroundRanges[h]++;
                }
            }
        }
    }
    void AirToAirSetup()
    {
        airToAirRanges = new int[airToAir.ranges.Count];
        airToAirRNG = new int[airToAir.ranges.Count];
        airToAir.withinRanges.Clear();

        for (int h = 0; h < airToAir.ranges.Count; h++)
        {
            airToAir.withinRanges.Add(false);
            for (int i = 0; i < airToAir.moveSequences.Count; i++)
            {
                if (h == airToAir.moveSequences[i].rangeID)
                {
                    airToAirRNG[h] += airToAir.moveSequences[i].RNGWeight;
                    airToAirRanges[h]++;
                }
            }
        }
    }

}