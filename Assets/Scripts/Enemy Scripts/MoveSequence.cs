using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveSequence
{
    public int rangeID;
    public bool targetAirborne;
    public bool requiresAirborne;
    public int RNGWeight;
    public List<MoveProperties> moves;
}
