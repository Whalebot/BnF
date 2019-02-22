using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour {
    public GameObject particle;

	// Use this for initialization
	void OnEnable() {
        Instantiate(particle, transform.position,Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
