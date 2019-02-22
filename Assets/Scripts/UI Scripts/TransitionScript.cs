using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour {

	public GameObject ClosedPos;
	public GameObject OpenPos;

	public float speed;
	Vector2 initialPos;

	public bool Close;


	// Use this for initialization
	void Start () {
		transform.position = ClosedPos.transform.position;
		//Close = false;
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		if (Close == true) {
			transform.position = Vector2.MoveTowards (transform.position, ClosedPos.transform.position, step);
		} else {
			transform.position = Vector2.MoveTowards (transform.position, OpenPos.transform.position, step);
		}
	}
}
