using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Attack State", menuName = "Attack state")]
[System.Serializable]
public class AttackState : ScriptableObject
{
    [HideInInspector]
    public int numberOfRanges;
    [HideInInspector]
    public List<int> tempRanges;
    [HideInInspector]
    public List<int> temp2Ranges;
    [HideInInspector]
    public int[] RNGlevels;
   [HideInInspector]
    public int[] rangeLevels;

    [HideInInspector]
    public List<bool> withinRanges;
    public List<MoveSequence> moveSequences;


}
