using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_switch : MonoBehaviour {

	public AudioSource BGM;

	public AudioClip BGM1;
	public AudioClip BGM2;

    public GameObject bossHealthBar;

	public float fadeOutTime;
	public float fadeInTime;
	float initialVolume;
    
	bool switched;
	public bool bossMusic;

	GameObject manager;


	// Use this for initialization
	void Start () {

		initialVolume = BGM.volume;
		manager = GameObject.FindWithTag ("Manager");
	}
	
	// Update is called once per frame
	void Update () {
		if (bossMusic != true) {
			if (manager.GetComponent<DialogueManager> ().sentences.Count == 1) {
				bossMusic = true;
				PlayBossMusic ();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(switched != true){
			if (other.CompareTag ("Player")) {
				switched = true;
				StartCoroutine (Fade ());
			}
		}
	}
	void PlayBossMusic(){
		BGM.volume = initialVolume * 4;
		BGM.clip = BGM2;
		BGM.Play ();
		bossHealthBar.SetActive(true);
	}

	IEnumerator Fade(){
		while (BGM.volume > 0) {
			BGM.volume -= Time.deltaTime * fadeOutTime;
			yield return null;
		}
	}
}
