using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelect : MonoBehaviour {
	public GameObject[] blades = new GameObject[12];
	public GameObject currentBlade;

	public int bladesnr;
	public int selected;
	public int selected1;
	public int selected2;

	public float speed;
	public float rotateSpeed;

	public bool canMove;
	public bool unsheathe;

	float rotation;

	// Use this for initialization
	void Start () {
		selected = 0;
		currentBlade = blades[0];
		canMove = true;
		rotation = 39.615f;
	}
	
	// Update is called once per frame
	void Update () {
		float rotateStep = rotateSpeed * Time.deltaTime;
		currentBlade = blades [selected];
		transform.rotation =Quaternion.Slerp(transform.rotation,Quaternion.Euler(33.286f,0,rotation),rotateStep);
		if (unsheathe == true) {
			float step = speed * Time.deltaTime;
			currentBlade.transform.GetChild(0).transform.position = Vector3.MoveTowards (currentBlade.transform.GetChild(0).transform.position, currentBlade.transform.GetChild(1).transform.position, step);
		}
	}

	public void ScrollUp(){
		if (canMove == true) {
			if (selected == bladesnr -1) {
				selected = 0;
			} else {
				selected += 1;
			}
			canMove = false;
			rotation -= 30;
			StartCoroutine (wait ());
		}
	}
	public void ScrollDown(){
		if (canMove == true) {
			if (selected == 0) {
				selected = bladesnr -1;
			} else {
				selected -= 1;
			}
			canMove = false;
			rotation += 30;
			StartCoroutine (wait ());
		}
	}

	public void Select(){
		if (canMove == true) {
			unsheathe = true;
			canMove = false;
			StartCoroutine (doors ());
		}
	}

	IEnumerator wait(){
		yield return new WaitForSeconds (0.25f);
		canMove = true;
	}
	IEnumerator doors(){
		yield return new WaitForSeconds (1);
		GameObject.Find ("Door").transform.GetChild (0).GetComponent<TransitionScript> ().Close = true;
		GameObject.Find ("Door").transform.GetChild (1).GetComponent<TransitionScript> ().Close = true;
	}
}
