using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class petalList : MonoBehaviour {

	public List<GameObject> petals;
	public bool comboEnd;
	float waitTime;
	float metergain;
	GameObject player;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		petals = new List<GameObject> ();
		waitTime = 0.05f;
	}
	
	// Update is called once per frame
	void Update () {
		if (UIManager.hits == 0) {
			if (comboEnd != true) {
				StartCoroutine (PetalBurst ());
				comboEnd = true;
			}
		}
		}
		
	
	IEnumerator PetalBurst(){
		while (petals.Count != 0) {
			petals [petals.Count - 1].GetComponent<ComboParticle> ().end = true;
			petals.Remove (petals [petals.Count - 1]);
			metergain += 1;
			GetComponent<AudioSource> ().Play ();
			yield return new WaitForSeconds (waitTime);;
		}
		player.GetComponent<PlayerStatus> ().special += metergain;
			yield return new WaitForSeconds(2f);
			Destroy(gameObject);

	}
}