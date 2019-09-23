using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_sfx : MonoBehaviour
{

    public GameObject stepLeft;
    public GameObject stepRight;

    float initialpitch;

    // Use this for initialization
    void Start()
    {
     
    }

    // Update is called once per frame

    void StepLeft() {
        Instantiate(stepLeft, transform.position, transform.rotation);
    }

    void StepRight() {
        Instantiate(stepRight, transform.position, transform.rotation);
    }

}
