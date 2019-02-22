using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopScript : MonoBehaviour
{
    public static bool hitStop;
    public float hitStopCounter;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hitStop) hitStopCounter--;
        if (hitStopCounter <= 0) hitStop = false;
    }

    public void HitStop(float dur)
    {
        hitStopCounter = dur;
        hitStop = true;
    }
}
