using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboParticle : MonoBehaviour {

	Image img;
	Color col;
	Vector3 destination;
	public Vector3 destination2;
	float speed;
	float velocity;
	Vector3 velocity1;
	Vector3 canvas;
	public bool end;



	// Use this for initialization
	void Start () {
		speed = 0.25f;
		img = GetComponent<Image> ();
		col = img.color;
		canvas = GetComponent<RectTransform> ().position;
		destination = new Vector3 (canvas.x + Random.Range(-75,75), canvas.y + Random.Range(-75,75), canvas.z);
		//destination2 = new Vector3 (canvas.x + -900, canvas.y + 100, canvas.z);
	}
	
	// Update is called once per frame
	void Update ()
	{
		img.color = col;
			if (end == true) {
				transform.position = Vector3.SmoothDamp (transform.position, destination2, ref velocity1, speed);
				col.a = Mathf.SmoothDamp (col.a, 0f, ref velocity, 1f, 150f, Time.deltaTime); 
				if (col.a <= 0.001f) {
					Destroy (gameObject);
				}
				return;
			}
			transform.position = Vector3.SmoothDamp (transform.position, destination, ref velocity1, speed);
		}
}
