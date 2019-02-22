using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public bool hasLimit = true;
    public int duration;
    public int delay;
    public bool active;
    public int offSet;
    int durationCounter;
    int delayCounter;

    public bool doesDamage;
    public GameObject trap;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasLimit && !active)
        {
            offSet--;
            if (offSet <= 0) active = true;
        }
        if (active)
        {
            durationCounter++;
            delayCounter++;


            if (doesDamage)
            {
                trap.SetActive(true);
            }
            else
            {
                trap.SetActive(false);
            }
        }
        else trap.SetActive(false);

        if (durationCounter >= duration && hasLimit)
        {
            active = false;
            durationCounter = 0;
            delayCounter = 0;
        }

        if (delayCounter > delay)
        {
            doesDamage = !doesDamage;
            delayCounter = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TrapTrigger"))
        {
            Activate();
        }
    }

    void Activate()
    {
        active = true;
    }
}
