using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSoundCaller : MonoBehaviour {

	public GameObject sfx;

	// Use this for initialization

	void Awake() {

	}
	void OnEnable () {
		Instantiate (sfx);
	}		

	// Update is called once per frame
	void Update () {
		
	}
}
