using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class ParallaxObject : MonoBehaviour {
	public float speedX;
	public float speedY;
	public bool moveInOppositeDirection;
	public bool skyBox;
	public int cameraToFollow;
	private Transform cameraTransform;
	private Vector3 previousCameraPosition;
	private bool previousMoveParallax;
	private ParallaxCamera options;

	void Start() {
        GameObject gameCamera = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManagerScript>().cameras[cameraToFollow];
        options = gameCamera.GetComponent<ParallaxCamera>();
        cameraTransform = gameCamera.transform;
        previousCameraPosition = cameraTransform.position;
        /*if (cameraToFollow == 1) {
			GameObject gameCamera = GameObject.Find ("CameraManager").GetComponent<CameraManagerScript> ().zone1Cam;
			options = gameCamera.GetComponent<ParallaxCamera>();
			cameraTransform = gameCamera.transform;
		}
		if (cameraToFollow == 2) {
			GameObject gameCamera = GameObject.Find ("CameraManager").GetComponent<CameraManagerScript> ().zone2Cam;
			options = gameCamera.GetComponent<ParallaxCamera>();
			cameraTransform = gameCamera.transform;
		}
		if (cameraToFollow == 3) {
			GameObject gameCamera = GameObject.Find ("CameraManager").GetComponent<CameraManagerScript> ().zone3Cam;
			options = gameCamera.GetComponent<ParallaxCamera>();
			cameraTransform = gameCamera.transform;
		}
		if (cameraToFollow == 4) {
			GameObject gameCamera = GameObject.Find ("CameraManager").GetComponent<CameraManagerScript> ().zone4Cam;
			options = gameCamera.GetComponent<ParallaxCamera>();
			cameraTransform = gameCamera.transform;
		}
		if (cameraToFollow == 5) {
			GameObject gameCamera = GameObject.Find ("CameraManager").GetComponent<CameraManagerScript> ().zone5Cam;
			options = gameCamera.GetComponent<ParallaxCamera>();
			cameraTransform = gameCamera.transform;
		}
        previousCameraPosition = cameraTransform.position;*/
    }

    void Update () {
		//GameObject gameCamera = GameObject.Find("CameraManager").GetComponent<CameraManagerScript>().currentCamera;
		//options = gameCamera.GetComponent<ParallaxCamera> ();
		//cameraTransform = gameCamera.transform;
		if(options.moveParallax && !previousMoveParallax)
			previousCameraPosition = cameraTransform.position;

		previousMoveParallax = options.moveParallax;

		if(!Application.isPlaying && !options.moveParallax)
			return;

		Vector3 distance = cameraTransform.position - previousCameraPosition;
		float direction = (moveInOppositeDirection) ? -1f : 1f;
		transform.position += Vector3.Scale(distance, new Vector3(speedX, speedY)) * direction;

		previousCameraPosition = cameraTransform.position;
	}
}
