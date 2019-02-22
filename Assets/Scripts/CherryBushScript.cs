using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryBushScript : MonoBehaviour {
	public bool activated;
	public float velocity;
	SpriteRenderer image;
	Color col;
	GameObject manager;


	// Use this for initialization
	void Start () {
		image = gameObject.GetComponent<SpriteRenderer>();
		col = image.color;
		manager = GameObject.FindWithTag ("Manager");
	}
	
	// Update is called once per frame
	void Update () {
		image.color = col;
		if (manager.GetComponent<DialogueManager> ().sentences.Count == 1) {
			activated = true;
		}
		if (activated == true) {
			transform.Translate (-transform.right * 0.5f);
			col.a = Mathf.SmoothDamp(col.a, 0f, ref velocity, 0.25f,100f, Time.deltaTime); 
		}
		}
		

	}
