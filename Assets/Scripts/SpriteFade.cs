using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{

    public bool fadeIn;
    public float speed;
    public Collider2D triggerZone;
    SpriteRenderer SR;
    public GameObject fadeObject;
    Color c;
    bool isInside;
    // Use this for initialization
    void Start()
    {
        SR = fadeObject.GetComponent<SpriteRenderer>();
        c = SR.color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isInside)
        {
            if (fadeIn && c.a <= 1) c.a = c.a + speed;
            else if (!fadeIn && c.a >= 0) c.a = c.a - speed;
        }
        else
        {
            if (fadeIn && c.a >= 0) c.a = c.a - speed;
            else if (!fadeIn && c.a <= 1) c.a = c.a + speed;
        }
        SR.color = c;

        // if (triggerZone)
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Player"))
        isInside = true;

    }

    void OnTriggerExit2D(Collider2D other)
    {


        if (other.CompareTag("Player"))
            isInside = false;
    }
}
