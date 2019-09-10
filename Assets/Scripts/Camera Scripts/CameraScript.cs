using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    [Header("General Camera stuff")]
    [SerializeField] private float dampTime = 0.15f;
    [SerializeField] private Vector3 velocity = Vector3.zero;
    public Transform target;
    Camera cam;
    [SerializeField] private float directionOffset;
    Vector3 targetPos;

   [Header("Zoom")]
    [SerializeField] private Vector3 zoomTarget;
    [SerializeField] private bool zooming;
    private bool positionChange;
    [SerializeField] private float zoomSize;
    [SerializeField] private float zoomInVelocity;
    [SerializeField] private float zoomOutVelocity;
    [SerializeField] private float zoomInDuration;
    [SerializeField] private float zoomedDuration;
    [SerializeField] private float zoomOutDuration;
    [SerializeField] private float zoomTime;
    private int count;

    [Header("Camera Boundary")]
    [SerializeField] private bool xMin;
    [SerializeField] private float xMinValue = 0;
    [SerializeField] private bool xMax;
    [SerializeField] private float xMaxValue = 0;
    [SerializeField] private bool yMin;
    [SerializeField] private float yMinValue = 0;
    [SerializeField] private bool yMax;
    [SerializeField] private float yMaxValue = 0;
    [SerializeField] private float yMinClamp = 0;
    [SerializeField] private float yMaxClamp = 0;
    [SerializeField] private bool lockToPlayerY;
    [SerializeField] private float yLockMin = 0;
    [SerializeField] private float yLockMax = 0;
    [SerializeField] private bool lockToPlayerX;
    [SerializeField] private float xLockMin = 0;
    [SerializeField] private float xLockMax = 0;
    [SerializeField] private bool boundary;
    [SerializeField] private float bufferSpaceX;
    [SerializeField] private float bufferSpaceY;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void OnDrawGizmosSelected()
    {
        cam = GetComponent<Camera>();
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(xMinValue - cam.orthographicSize * 16 / 9, yMinValue - cam.orthographicSize, 0), new Vector3(xMaxValue + cam.orthographicSize * 16 / 9, yMinValue - cam.orthographicSize, 0));
        Gizmos.DrawLine(new Vector3(xMinValue - cam.orthographicSize * 16 / 9, yMaxValue + cam.orthographicSize, 0), new Vector3(xMaxValue + cam.orthographicSize * 16 / 9, yMaxValue + cam.orthographicSize, 0));
        Gizmos.DrawLine(new Vector3(xMinValue - cam.orthographicSize * 16 / 9, yMinValue - cam.orthographicSize, 0), new Vector3(xMinValue - cam.orthographicSize * 16 / 9, yMaxValue + cam.orthographicSize, 0));
        Gizmos.DrawLine(new Vector3(xMaxValue + cam.orthographicSize * 16 / 9, yMinValue - cam.orthographicSize, 0), new Vector3(xMaxValue + cam.orthographicSize * 16 / 9, yMaxValue + cam.orthographicSize, 0));
    }

    void FixedUpdate()
    {
        if (!zooming)
        {
            targetPos = target.position - Vector3.forward * 10 + Vector3.right * Player_Movement.faceDirection * directionOffset;

            if (yMin && yMax) targetPos.y = Mathf.Clamp(targetPos.y, yMinValue, yMaxValue);
            if (xMin && xMax) targetPos.x = Mathf.Clamp(targetPos.x, xMinValue, xMaxValue);
            if (!positionChange)
            {
                if (!boundary) transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, dampTime);
                if (boundary)
                {
                    transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, dampTime);
                    if (transform.position.x > target.position.x + bufferSpaceX) transform.position = new Vector3(target.position.x + bufferSpaceX, transform.position.y, -10);
                    if (transform.position.x < target.position.x - bufferSpaceX) transform.position = new Vector3(target.position.x - bufferSpaceX, transform.position.y, -10);

                    if (transform.position.y > target.position.y + bufferSpaceY) transform.position = new Vector3(transform.position.x, target.position.y + bufferSpaceY, -10);
                    if (transform.position.y < target.position.y - bufferSpaceY) transform.position = new Vector3(transform.position.x, target.position.y - bufferSpaceY, -10);
                }

                if (yMin && yMax) transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMinValue, xMaxValue), Mathf.Clamp(transform.position.y, yMinValue, yMaxValue), -10);
                if (lockToPlayerY) transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMinValue, xMaxValue), Mathf.Clamp(targetPos.y, target.transform.position.y + yLockMin, target.transform.position.y + yLockMax), -10);
                if (lockToPlayerX) transform.position = new Vector3(Mathf.Clamp(targetPos.x, target.transform.position.x + xLockMin, target.transform.position.x + xLockMax), Mathf.Clamp(transform.position.y, yMinValue, yMaxValue), -10);
            }
        }
    }

    private void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (Input.GetKeyDown("b")) { SmoothZoom(zoomTarget); }
    }

    public void SmoothZoom(Vector3 other)
    {
        zooming = true;
        positionChange = true;
        // transform.position = other;
        StopAllCoroutines();
        //Zooming in
        if (cam.orthographicSize > zoomSize)
        {
            print("Obama");
            StartCoroutine("ZoomInOn", other);
        }
        //Zooming out
        else
        {
        }
    }


    private IEnumerator ZoomInOn(Vector3 other)
    {
        while (cam.orthographicSize > zoomSize)
        {
            transform.position = Vector3.Lerp(transform.position, other, zoomTime);
            cam.orthographicSize += zoomInVelocity;
            yield return new WaitForEndOfFrame();
        }
        cam.orthographicSize = zoomSize;
        while (count < zoomedDuration)
        {
            count++;
            yield return new WaitForEndOfFrame();
        }
        count = 0;
        zooming = false;
        StartCoroutine("RevertZoom");
    }

    private IEnumerator RevertZoom()
    {
        while (cam.orthographicSize < 15)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, zoomTime * 2);

            cam.orthographicSize += zoomOutVelocity;
            yield return new WaitForEndOfFrame();
        }
        cam.orthographicSize = 15;
        positionChange = false;
    }

    public IEnumerator ZoomOut(float speed)
    {
        while (cam.orthographicSize < 20)
        {
            cam.orthographicSize += speed;
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator ZoomIn(float speed)
    {
        while (cam.orthographicSize > 15)
        {
            cam.orthographicSize -= speed;
            yield return new WaitForEndOfFrame();
        }
    }
}