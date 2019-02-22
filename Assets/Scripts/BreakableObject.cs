using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour {
    public bool breakable = true;
    public GameObject breakParticle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Attack") && breakable) BreakObject();
    }

    public void BreakObject()
    {
        if (breakParticle != null) Instantiate(breakParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
