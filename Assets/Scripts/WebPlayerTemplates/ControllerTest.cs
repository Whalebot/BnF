using UnityEngine;
using System.Collections;
public class ControllerTest : MonoBehaviour {
	public float speed = 5f;
	public float jumpSpeed = 8f;
	private float movement = 0f;
//	private float pressUp = 0f;
	private Rigidbody2D rigidBody;
	public Transform groundCheckPoint;
	public float groundCheckRadius;
	public LayerMask groundLayer;
	private bool isTouchingGround;
	bool airborne;
	int airjumps;
	public int allowedAirJumps;
	float direction;
	public bool attackanim;
	float currentpress;
	public float combo;
	public float cooldowntime = 2;
	bool cooldown;

	public GameObject attack1;
	public GameObject attack2;
	public GameObject attack3;
	public GameObject attack4;
	public GameObject attack5;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		attackanim = false;
	}

	// Update is called once per frame
	void Update () {
		isTouchingGround = Physics2D.OverlapCircle (groundCheckPoint.position, groundCheckRadius, groundLayer);
		if (isTouchingGround == true) {
			airborne = false;
			airjumps = 0;
		} else {
			airborne = true;
		}

		movement = Input.GetAxis ("Horizontal");
		//pressUp = Input.GetAxis ("Vertical");


		if (movement > 0f) {
			if (attackanim != true) {
				rigidBody.velocity = new Vector2 (movement * speed, rigidBody.velocity.y);
				transform.rotation = Quaternion.Euler (0, 0, 0);
				direction = movement;
			}
		} else if (movement < 0f) {
			if (attackanim != true) {
				rigidBody.velocity = new Vector2 (movement * speed, rigidBody.velocity.y);
				transform.rotation = Quaternion.Euler (0, 180, 0);
				direction = movement;
			}
		} else {
			rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
		}
		if (Input.GetButtonDown ("Jump") && isTouchingGround) {
			rigidBody.velocity = new Vector2 (rigidBody.velocity.x, jumpSpeed);
			attack1.SetActive (false);
			attack2.SetActive (false);
			attack3.SetActive (false);
			combo = 0;
		}
			
		if (Input.GetButtonDown ("Jump")) {
			if (airborne == true) {
				if (airjumps != allowedAirJumps) {
					rigidBody.velocity = new Vector2 (rigidBody.velocity.x, jumpSpeed);
					airjumps = airjumps + 1;
					StopCoroutine (Attack1 ());
					StopCoroutine (Attack2 ());
					StopCoroutine (Attack3 ());
					attack1.SetActive (false);
					attack2.SetActive (false);
					attack3.SetActive (false);
					combo = 0;
				}
			}
		}
		if (Input.GetKey (KeyCode.I)) {
			if (airborne != true) {
				StartCoroutine (Attack5 ());
				return;
			}
		}

		if (Input.GetKeyDown (KeyCode.J)) {
			
			if (Input.GetKey (KeyCode.W)) {
				if (airborne != true) {
					StartCoroutine (Attack4 ());
					return;
				}
			}
			if (Input.GetKey (KeyCode.S)) {
				if (airborne == true) {
					StartCoroutine (Attack3 ());
					return;
				}
			}


			if (combo == 0) {
				StartCoroutine (Attack1 ());
				attackanim = true;
				combo = 1;
				return;
			}

				if (combo == 1) {
					StartCoroutine (Attack2 ());
					combo = 2;
					return;
				}
				if (combo == 2) {
					StartCoroutine (Attack3 ());
					combo = 3;
					return;
				}
	}
}

	IEnumerator Attack1(){
		if (airborne == true) {
			rigidBody.velocity = new Vector2 (direction * speed, 4);
		}
		rigidBody.velocity = new Vector2 (direction * speed, rigidBody.velocity.y);
		attack1.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		attack1.SetActive (false);
		attackanim = false;
		if (combo == 1) {
			combo = 0;
		}
	}
	IEnumerator Attack2(){
		attack1.SetActive (false);
		if (airborne == true) {
			rigidBody.velocity = new Vector2 (direction * speed, 4);
		}
		rigidBody.velocity = new Vector2 (direction * speed, rigidBody.velocity.y);
		attack2.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		attack2.SetActive (false);
		attackanim = false;
		if (combo == 2) {
			combo = 0;
		}
	}
	IEnumerator Attack3(){
		attack2.SetActive (false);
		if (airborne == true) {
			rigidBody.velocity = new Vector2 (direction * speed, -20);
		}
		rigidBody.velocity = new Vector2 (direction * speed, rigidBody.velocity.y);
		attack3.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		attack3.SetActive (false);
		attackanim = false;
		combo = 0;
	}
	IEnumerator Attack4(){
		attack1.SetActive (false);
		attack2.SetActive (false);
		attack3.SetActive (false);
		rigidBody.velocity = new Vector2 (rigidBody.velocity.x, jumpSpeed);
		attack4.SetActive (true);
		yield return new WaitForSeconds (0.3f);
		attack4.SetActive (false);
		combo = 0;
	}
	IEnumerator Attack5(){
		attack1.SetActive (false);
		attack2.SetActive (false);
		attack3.SetActive (false);
		if (cooldown != false) {
			attack5.SetActive (true);
			Physics2D.Raycast (transform.position, transform.forward * 10);
			transform.position = new Vector2 (transform.position.x, transform.position.y) + new Vector2 (direction * 10, 0);
			//rigidBody.velocity = new Vector2 (direction * 100, rigidBody.velocity.y);
			attack5.SetActive (true);
			yield return new WaitForSeconds (0.3f);
			attack5.SetActive (false);
			combo = 0;
		}
		yield return new WaitForSeconds (cooldowntime);
		cooldown = true;

	}
}