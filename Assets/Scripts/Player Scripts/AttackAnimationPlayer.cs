using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationPlayer : MonoBehaviour {

	public Animation attackAnim;

	// Use this for initialization
	void Start () {
		
	}
	void onEnable(){
		attackAnim = GetComponent<Animation> ();
		attackAnim.Play(attackAnim.clip.name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
