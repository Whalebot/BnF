using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAI : MonoBehaviour
{
    public int state;
    public float lineLength;

    public float walkDistance;
    public int walkDuration;

    public AttackState groundToGround;
    public AttackState groundToAir;
    public AttackState airToGround;
    public AttackState airToAir;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(Mathf.Sign(transform.parent.localScale.x) * lineLength, 0, 0));
    }


    void Setup(AttackState attackState)
    {


        attackState.tempRanges.Clear();
        attackState.temp2Ranges.Clear();
        attackState.withinRanges.Clear();
        attackState.RNGlevels = new int[0];


        for (int h = 0; h < attackState.moveSequences.Count; h++)
        {
            attackState.tempRanges.Add(attackState.moveSequences[h].range);
        }

        for (int h = 0; h < attackState.moveSequences.Count; h++)
        {
            if (!attackState.temp2Ranges.Contains(attackState.tempRanges[h])) attackState.temp2Ranges.Add(attackState.tempRanges[h]);
        }
        attackState.numberOfRanges = attackState.temp2Ranges.Count;

        attackState.rangeLevels = new int[attackState.numberOfRanges];
        attackState.RNGlevels = new int[attackState.numberOfRanges];

        for (int h = 0; h < attackState.numberOfRanges; h++)
        {
            attackState.withinRanges.Add(false);
            for (int i = 0; i < attackState.moveSequences.Count; i++)
            {
                if (attackState.temp2Ranges[h] == attackState.moveSequences[i].range)
                {
                    attackState.RNGlevels[h] += attackState.moveSequences[i].RNGWeight;
                    attackState.rangeLevels[h]++;
                }
            }
        }
    }


    private void Start()
    {
        Setup(groundToGround);
        Setup(groundToAir);
        Setup(airToGround);
        Setup(airToAir);
    }
}