using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour {
    BGMScript bgmManager;
    public bool playOnce;
	// Use this for initialization
	void Start () {
        bgmManager = GameObject.FindGameObjectWithTag("BGMManager").GetComponent<BGMScript>();
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !playOnce) {
            playOnce = true;
            bgmManager.LowerBGM();
            bgmManager.TriggerMusic();

        }
    }
}
