using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {
    public bool zoomTrigger = true;
    public bool zoomIn;
    public float zoomVelocity;
    CameraManagerScript camMan;
	// Use this for initialization
	void Start () {
        camMan = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManagerScript>();
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            camMan.StopAllCoroutines();
            if (zoomIn) camMan.currentCamera.GetComponent<CameraScript>().StartCoroutine("ZoomIn", zoomVelocity);
            else camMan.currentCamera.GetComponent<CameraScript>().StartCoroutine("ZoomOut", zoomVelocity);
        }

    }

	// Update is called once per frame
	void Update () {
		
	}
}
