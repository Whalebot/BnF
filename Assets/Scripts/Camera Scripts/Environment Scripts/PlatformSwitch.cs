using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitch : MonoBehaviour
{

    public float rotateSpeed;
    public float rotation;
    public bool isPlatform;
    public bool isRotatingWall;
    public MovingPlatform movingPlatform;
    public RotateWall rotateWall;
	public bool singleUse;
    public bool playerCollision;
    public bool canActivate;
    public bool activated;
    public bool setPermanent;
  //  public static bool permanentTrigger;
	public AudioClip audioClip;

    // Use this for initialization
    void Start()
    {
        playerCollision = false;
        canActivate = false;
        rotation = -30;
		GetComponent<AudioSource> ().clip = audioClip;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetAxis ("Vertical") == 0) {
			canActivate = true;
		}
		if (Input.GetAxis ("Vertical") == 1) {
			if (canActivate == true) {
				if (playerCollision == true) {
					transform.GetChild (2).gameObject.SetActive (true);
					canActivate = false;
					if (activated != true) {
						activated = true;
						rotation = 30;
					} else {
						activated = false;
						rotation = -30;
					}
				}
			}
		}*/
        if (playerCollision == true)
        {
            
            canActivate = false;
			if (activated != true) {
                if(!isPlatform)
                transform.GetChild (2).gameObject.SetActive (true);
				activated = true;
				rotation = 30;
				playerCollision = false;
				GetComponent<AudioSource> ().Play ();
			} else {
				if (singleUse != true) {
                    if (!isPlatform) transform.GetChild (2).gameObject.SetActive (false);
					activated = false;
					rotation = -30;
					playerCollision = false;
					GetComponent<AudioSource> ().Play ();
				}
			}
        }
        float rotateStep = rotateSpeed * Time.deltaTime;
        transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, Quaternion.Euler(0, 0, rotation), rotateStep);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            playerCollision = !playerCollision;
            if (isPlatform) movingPlatform.ActivateTrigger();
            if (isRotatingWall) rotateWall.ActivateTrigger();
        }
    }
/*
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            playerCollision = false;
        }
    }
    */
}
