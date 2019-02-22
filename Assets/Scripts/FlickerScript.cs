using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerScript : MonoBehaviour {
    SpriteRenderer SR;
    public int delay;
    public int counter;
	// Use this for initialization
	void Start () {
        SR = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate() {
        counter++;
        if (counter >= delay) { SR.enabled = !SR.enabled; counter = 0; }
    }
}
