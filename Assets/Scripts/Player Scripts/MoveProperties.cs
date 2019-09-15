using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Move", menuName = "Moves")]
public class MoveProperties : ScriptableObject
{
    public Move move;
    public GameObject hitbox;
}
