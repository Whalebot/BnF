using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMover : MonoBehaviour {

	public GameObject target;

	Vector3 initialPosition;
	public Vector3 currentPosition;
	public Vector3 activePosition;
	Vector3 velocity;

	public int NR;

	public float smoothTime;
	public float fadeTime;

	public bool shouldFade;

	public CanvasGroup image;

	// Use this for initialization
	void Start () {
		initialPosition = gameObject.transform.position;
		if (shouldFade == true) {
			image.alpha = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("MenuController").GetComponent<MenuController> ().currentNR == NR) {
			transform.position = Vector3.SmoothDamp (transform.position, target.transform.position, ref velocity, smoothTime);
			currentPosition = transform.position;
			if (shouldFade == true) {
				if (image.alpha < 1) {
					image.alpha += Time.deltaTime / fadeTime;
				}
			}
		} else {
			transform.position = Vector3.SmoothDamp (transform.position, initialPosition, ref velocity, smoothTime);
			currentPosition = transform.position;
			if (shouldFade == true) {
				if (image.alpha > 0) {
					image.alpha -= Time.deltaTime / fadeTime;
				}
			}
		}
	}
}
