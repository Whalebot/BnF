using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public GameObject exit;
	public GameObject player;
	public bool playercollision;
	public static bool canEnter;
	public GameObject Door1;
	public GameObject Door2;
	public float transitionTime;
	bool transition;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playercollision = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (transition != true) {
			if (Input.GetAxis ("KB_Vertical") == 0) {
				canEnter = true;
			}
			if (Input.GetAxis ("KB_Vertical") == 1) {
				if (canEnter == true) {
					if (playercollision == true) {
						StartCoroutine(Transition());
						canEnter = false;
					}
				}
			}
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			playercollision = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.CompareTag("Player")){
			playercollision = false;
		}
	}
	IEnumerator Transition(){
		transition = true;
		Door1.GetComponent<TransitionScript> ().Close = true;
		Door2.GetComponent<TransitionScript> ().Close = true;
		yield return new WaitForSeconds (transitionTime);
		player.transform.position = exit.transform.position;
		Door1.GetComponent<TransitionScript> ().Close = false;
		Door2.GetComponent<TransitionScript> ().Close = false;
		transition = false;
	}
}
