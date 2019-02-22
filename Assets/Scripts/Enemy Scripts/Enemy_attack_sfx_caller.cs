using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attack_sfx_caller : MonoBehaviour {

	public GameObject thisEnemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnEnable () {
		transform.parent.parent.GetComponent<Enemy_sfx> ().PlayAttack ();
	}
}
