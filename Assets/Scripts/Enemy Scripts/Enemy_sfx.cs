using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_sfx : MonoBehaviour {

	public AudioClip hit;
	public AudioClip attack;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void PlayHit(){
		GetComponent<AudioSource> ().pitch = Random.Range (0.75f, 1);
		GetComponent<AudioSource> ().clip = hit;
		GetComponent<AudioSource> ().Play ();
	}
	public void PlayAttack(){
		GetComponent<AudioSource> ().pitch = Random.Range (0.75f, 1);
		GetComponent<AudioSource> ().clip = attack;
		GetComponent<AudioSource> ().Play ();
	}
}
