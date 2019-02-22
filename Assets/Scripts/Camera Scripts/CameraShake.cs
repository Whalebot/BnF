using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	public Transform cam;
	public float duration = 1f;
	public float power = 0.7f;
	public float slowDownAmount = 1.0f;
	public bool shouldShake = false;

	Vector3 startPosition;
	float initialDuration;

	void Start()
	{
		cam = Camera.main.transform;

		initialDuration = duration;
	}

	void Update()
	{
		startPosition = cam.localPosition;
		if (shouldShake && !PauseMenu.gameIsPaused) {
			if (duration > 0) {
				cam.localPosition = startPosition + Random.insideUnitSphere * power;
				duration -= Time.deltaTime * slowDownAmount;
			} else 
			{
				shouldShake = false;
				duration = initialDuration;
				cam.localPosition = startPosition;
			}
		}

	}
}
