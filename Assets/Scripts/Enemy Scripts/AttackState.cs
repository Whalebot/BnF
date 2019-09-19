using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Attack State", menuName = "Attack state")]
[System.Serializable]
public class AttackState : ScriptableObject
{
    public List<float> ranges;
    [HideInInspector]
    public List<bool> withinRanges;
    public List<MoveSequence> moveSequences;
}
