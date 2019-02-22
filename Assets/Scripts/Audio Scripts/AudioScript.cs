using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {
    public AudioClip BGM;
    AudioSource audioSource;
    bool isPaused;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (HitStopScript.hitStop) { audioSource.Pause(); isPaused = true; }
        else if (isPaused && !HitStopScript.hitStop) audioSource.UnPause();
	}
}
