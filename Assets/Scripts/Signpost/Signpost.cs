using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signpost : MonoBehaviour
{
    public GameObject sign;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) sign.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player")) sign.SetActive(false);
    }
}
