using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTest : MonoBehaviour {

	public GameObject AttackObject1;
	public GameObject AttackObject2;
	public GameObject AttackObject3;
	public GameObject Launcher;
	public GameObject DownSlash;

	public float startup;
	public float active;
	public  float recovery;

	float initialstartup;
	float initialactive;
	float initialrecovery;

	// Use this for initialization
	void Start () {
		initialstartup = startup;
		initialactive = active;
		initialrecovery = recovery;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Attack")){
			if (startup > 0) {
				GetComponent<Player_Movement> ().mov = false;
				startup -= Time.deltaTime;
			} else {
				Attack ();
				if (active > 0) {
					active -= Time.deltaTime;
				} else {
					AttackStop ();
					if (recovery > 0) {
						recovery -= Time.deltaTime;
					} else {
						GetComponent<Player_Movement> ().mov = true;
						startup = initialstartup;
						active = initialactive;
						recovery = initialrecovery;
					}
				}
			}
		}
	}
	void Attack(){
		
	}
	void AttackStop(){
		
	}
}
