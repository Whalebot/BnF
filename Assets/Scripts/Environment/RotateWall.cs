using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWall : MonoBehaviour
{
    Animator anim;
    public bool activated;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateTrigger() { anim.SetBool("Activated", true); }
}
