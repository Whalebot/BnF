using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    bool taken;
    public GameObject checkpointFX;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, 10 * Time.deltaTime, 0);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) { Restart.checkpointNumber = 1; Restart.startPos = transform.position; gameObject.SetActive(false); Instantiate(checkpointFX, transform.position, Quaternion.identity); }
    }
}
