using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    Camera cam;
    public float directionOffset;

    public bool xMin;
    public float xMinValue = 0;
    public bool xMax;
    public float xMaxValue = 0;
    public bool yMin;
    public float yMinValue = 0;
    public bool yMax;
    public float yMaxValue = 0;
    public float yMinClamp = 0;
    public float yMaxClamp = 0;
    public bool lockToPlayerY;
    public float yLockMin = 0;
    public float yLockMax = 0;
    public bool lockToPlayerX;
    public float xLockMin = 0;
    public float xLockMax = 0;
    public bool boundary;
    public float bufferSpaceX;
    public float bufferSpaceY;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame

    void OnDrawGizmosSelected()
    {
        cam = GetComponent<Camera>();
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(xMinValue - cam.orthographicSize * 16 / 9, yMinValue- cam.orthographicSize, 0), new Vector3(xMaxValue+ cam.orthographicSize * 16 / 9,yMinValue - cam.orthographicSize, 0));
        Gizmos.DrawLine(new Vector3(xMinValue - cam.orthographicSize * 16 / 9, yMaxValue + cam.orthographicSize, 0), new Vector3(xMaxValue + cam.orthographicSize * 16 / 9, yMaxValue + cam.orthographicSize, 0));
        Gizmos.DrawLine(new Vector3(xMinValue - cam.orthographicSize * 16 / 9, yMinValue - cam.orthographicSize, 0), new Vector3(xMinValue - cam.orthographicSize * 16 / 9, yMaxValue + cam.orthographicSize, 0));
        Gizmos.DrawLine(new Vector3(xMaxValue + cam.orthographicSize * 16 / 9, yMinValue - cam.orthographicSize, 0), new Vector3(xMaxValue + cam.orthographicSize * 16 / 9, yMaxValue + cam.orthographicSize, 0));
    }

    void FixedUpdate()
    {
        // Debug.DrawLine(new Vector3(-xMinValue, -yMinValue, 0),new Vector3(xMaxValue, -yMinValue, 0), Color.blue);
        // Debug.DrawLine(new Vector3(-xMinValue, yMaxValue, 0), new Vector3(xMaxValue, yMaxValue, 0), Color.blue);
        //    Debug.DrawLine(new Vector3(-xMinValue, -yMinValue, 0),new Vector3(xMinValue, yMinValue, 0), Color.blue);
        //   Debug.DrawLine(new Vector3(xMaxValue, -yMinValue, 0), new Vector3(xMaxValue, yMaxValue, 0), Color.blue);


        if (Input.GetKeyDown("b"))
        {
            cam.orthographicSize = cam.orthographicSize - 1;
            //    StartCoroutine("ZoomOut", 1);
        }
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        Vector3 targetPos = target.position - Vector3.forward * 10 + Vector3.right * Player_Movement.faceDirection * directionOffset;
        //    if (lockToPlayer) targetPos.y = Mathf.Clamp(targetPos.y, target.transform.position.y - yLockMin, target.transform.position.y + yLockMax);
        if (yMin && yMax) targetPos.y = Mathf.Clamp(targetPos.y, yMinValue, yMaxValue);
        if (xMin && xMax) targetPos.x = Mathf.Clamp(targetPos.x, xMinValue, xMaxValue);

        //targetPos.y = Mathf.Clamp(targetPos.y, transform.position.y - yMinClamp, transform.position.y + yMaxClamp);
        // transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, target.position.y - yMinClamp, target.position.y + yMaxClamp), transform.position.z);
        if (!boundary) transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, dampTime);
        if (boundary)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, dampTime);
            if (transform.position.x > target.position.x + bufferSpaceX) transform.position = new Vector3(target.position.x + bufferSpaceX, transform.position.y, -10);// transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, dampTime);
                                                                                                                                                                       //      else transform.position = new Vector3(transform.position.x, targetPos.y, -10);
            if (transform.position.x < target.position.x - bufferSpaceX) transform.position = new Vector3(target.position.x - bufferSpaceX, transform.position.y, -10);// transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, dampTime);
                                                                                                                                                                       //    else transform.position = new Vector3(transform.position.x, targetPos.y, -10);

            if (transform.position.y > target.position.y + bufferSpaceY) transform.position = new Vector3(transform.position.x, target.position.y + bufferSpaceY, -10);
            //  else transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            if (transform.position.y < target.position.y - bufferSpaceY) transform.position = new Vector3(transform.position.x, target.position.y - bufferSpaceY, -10);
            //else transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        }

        if (yMin && yMax) transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMinValue, xMaxValue), Mathf.Clamp(transform.position.y, yMinValue, yMaxValue), -10);
        if (lockToPlayerY) transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMinValue, xMaxValue), Mathf.Clamp(targetPos.y, target.transform.position.y + yLockMin, target.transform.position.y + yLockMax), -10);
        if (lockToPlayerX) transform.position = new Vector3(Mathf.Clamp(targetPos.x, target.transform.position.x + xLockMin, target.transform.position.x + xLockMax), Mathf.Clamp(transform.position.y, yMinValue, yMaxValue), -10);

        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, target.position.x+yMinClamp, target.position.x+yMaxClamp), Mathf.Clamp(transform.position.y, target.position.y + yMinClamp, target.position.y + yMaxClamp), transform.position.z);

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