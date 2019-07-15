using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSprite : MonoBehaviour
{
    public int screenWidth;
    public int screenHeight;
    public float speed;
    public Vector3 startPos;
    public Vector3 direction;
    public bool horizontal;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
    //    screenWidth = Screen.w;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //  transform.position -= new Vector3(speed,0,0);
        transform.position += direction.normalized * speed;
        if (horizontal && transform.localPosition.x < -screenWidth) { transform.localPosition = new Vector3(screenWidth, 0, 0); ; print("pop"); }
        if (transform.localPosition.y < -screenHeight) { transform.localPosition = new Vector3(0, screenHeight, 0); ; print("pop"); }
    }
}
