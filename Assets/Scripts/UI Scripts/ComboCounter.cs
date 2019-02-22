using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCounter : MonoBehaviour {

	GameObject comboParticle;
	public GameObject hydrangeaPetal;
	public GameObject cherryPetal;
	public GameObject bambooPetal;
	public GameObject petalList;
	public List<GameObject> petals;
	public GameObject stroke;
	public GameObject petalTarget;
	GameObject player;
	GameObject newList;
	bool comboEnd;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		petals = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void ComboParticle(){
		
		if (player.GetComponent<PlayerStatus> ().activeWeapon == 1) {
			comboParticle = hydrangeaPetal;
		}
		if (player.GetComponent<PlayerStatus> ().activeWeapon == 2) {
			comboParticle = cherryPetal;
		}
		if (player.GetComponent<PlayerStatus> ().activeWeapon == 3) {
			comboParticle = bambooPetal;
		}
		if (UIManager.hits == 1) {
			newList = Instantiate (petalList);
			newList.transform.parent = gameObject.transform;
		}
		addPetal ();
	}

	void addPetal(){
		GameObject childObject =  Instantiate (comboParticle, transform.position, Quaternion.Euler(0,0,Random.Range(0f,360f)),stroke.transform) as GameObject;
		childObject.GetComponent<ComboParticle> ().destination2 = petalTarget.transform.position;
		//petals.Add (childObject);

		//childObject.transform.parent = gameObject.transform;
		childObject.transform.position = transform.position;
		newList.GetComponent<petalList> ().petals.Add (childObject);
	}
}
