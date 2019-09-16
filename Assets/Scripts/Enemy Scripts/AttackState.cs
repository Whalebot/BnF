using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackState
{
    public List<float> ranges;
    public List<bool> withinRanges;
    public List<MoveSequence> moveSequences;
}
