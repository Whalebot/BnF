using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	public GameObject bambooSpikes;
	public float interval;
	public float offset;
	public float duration;
	public float stopamount;
	public bool activated;
	public bool start = false;

	float initialDuration;
	float initialInterval;

	// Use this for initialization
	void Start () {
		bambooSpikes = transform.GetChild (1).gameObject;
		bambooSpikes.SetActive (false);
		initialDuration = duration;
		initialInterval = interval;
		StartCoroutine (Offset ());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (start == true) {
			if (activated != true) {
				if (interval > 0) {
					interval -= Time.deltaTime * stopamount;
				} else {
					activated = true;
					interval = initialInterval;
				}
			}

			if (activated == true) {
				if (duration > 0) {
					bambooSpikes.SetActive (true);
					duration -= Time.deltaTime * stopamount;
				} else {
					activated = false;
					duration = initialDuration;
					bambooSpikes.SetActive (false);
				}
			}
		}
	}
	IEnumerator Offset(){
		yield return new WaitForSeconds (offset);
		start = true;
	}
}
