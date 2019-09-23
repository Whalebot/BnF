using UnityEngine;
using System.Collections;

public class NewPara : MonoBehaviour
{
    public float speed;
    public bool skyBox;
    public static int cameraToFollow;
    public GameObject ghostCamera;
 //   private Transform cameraTransform;
 //   private Vector3 previousCameraPosition;
    private bool previousMoveParallax;
 //   private ParallaxCamera options;
    private Vector3 startPosition;
 //   private Vector3 startOffset;
    GameObject gameCamera;
    GameObject cm;
    Vector3 startCameraPos;
    public bool castleTest;

    void Start()
    {
        cm = GameObject.FindGameObjectWithTag("CameraManager");
        gameCamera = cm.GetComponent<CameraManagerScript>().cameras[cameraToFollow];
        startCameraPos = ghostCamera.transform.position;
        startPosition = transform.position;
    }

    void Update()
    {
        gameCamera = cm.GetComponent<CameraManagerScript>().cameras[cameraToFollow];

        if(castleTest) transform.position = startPosition + new Vector3(gameCamera.transform.position.x - startCameraPos.x, gameCamera.transform.position.y - startCameraPos.y, 0) * speed;
        else transform.position = startPosition + new Vector3(gameCamera.transform.position.x - startCameraPos.x, 0, 0) * speed;
    }
}
