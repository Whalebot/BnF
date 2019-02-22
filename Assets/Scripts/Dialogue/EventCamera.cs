using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCamera : MonoBehaviour {

	public float zoom;
	public float zoomTime;
	public float smoothTime;
	public float currentVelocity;
	public float maxSpeed;
	public float zoomMaxSpeed;
	public Vector3 targetPosition;
	public Vector3 velocity;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float deltaTime = Time.deltaTime;
		transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, smoothTime, maxSpeed, deltaTime);
		GetComponent<Camera>().orthographicSize = Mathf.SmoothDamp(GetComponent<Camera>().orthographicSize, zoom, ref currentVelocity, zoomTime,zoomMaxSpeed,deltaTime);
	}
}
