using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_sfx : MonoBehaviour {

	public AudioClip hit;

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource> ().clip = hit;
		GetComponent<AudioSource> ().pitch = Random.Range (0.75f, 1);
		GetComponent<AudioSource> ().Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
