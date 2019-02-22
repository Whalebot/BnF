using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTest : MonoBehaviour {
	public Animation anim;

	// Use this for initialization
	void Start () {
		StartCoroutine (destroy());
	}

	IEnumerator destroy(){
		yield return new WaitForSeconds (1);
		Destroy (gameObject);
	}
}
