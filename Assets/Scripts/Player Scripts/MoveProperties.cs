using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveProperties
{

    public enum Attack
    {
        g5S,
        g5SS,
        g5SSS,
        g8S,
        g2S,

        g5A,
        g5AA,
        g5AAA,
        g5AAAA,
        g8A,
        g8AA,
        g2A,
        g2AA,
        g6A,



        j5S,
        j5SS,
        j5SSS,
        j8S,
        j2S,

        j5A,
        j5AA,
        j5AAA,
        j5AAAA,
        j8A,
        j8AA,
        j2A,
        j2AA,
        j6A,

        extra1,
        extra2

        






    };

    public Attack attack;
    public bool isTracking;
}
