using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Vector3 startPos;
    public List<Vector3> checkpoints;
    public float velocity;

    public int time;
    public bool active;
    public bool requiresTrigger;
    public bool reverse;
    public float teleportRange;
    Rigidbody2D rb;
    int counter;
    public int i;
    public Vector3 currentDirection;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        checkpoints[0] = transform.position;
        if (!requiresTrigger) active = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            if (!reverse)
            {
                if (checkpoints.Count > i)
                {
                    //transform.Translate((checkpoints[i] - transform.position).normalized * velocity);
                     rb.velocity = (checkpoints[i] - transform.position).normalized * velocity;
                    if ((checkpoints[i] - transform.position).magnitude < teleportRange) transform.position = checkpoints[i];
                    currentDirection = (checkpoints[i] - transform.position).normalized;
                    if (transform.position == checkpoints[i] && checkpoints.Count > i) i++;
                    if (i == checkpoints.Count) {
                        reverse = true;
                        i--;
                        rb.velocity = Vector3.zero;
                        if (requiresTrigger) active = false; }
                }
            }
            else if (reverse)
            {
                if (i >= 0)
                {
                   // transform.Translate((checkpoints[i] - transform.position).normalized * velocity);
                     rb.velocity = (checkpoints[i] - transform.position).normalized * velocity;
                    if ((checkpoints[i] - transform.position).magnitude < velocity) transform.position = checkpoints[i];
                    currentDirection = (checkpoints[i] - transform.position).normalized;
                    if (transform.position == checkpoints[i]) i--;
                    if (i == -1) {
                        rb.velocity = Vector3.zero;
                        reverse = false; 
                        i++;
                        if (requiresTrigger) active = false;
                    }
                }
            }
        }


        /*counter++;
        if (counter >= time)
        {
            counter = 0;
            movingRight = !movingRight;
        }
        if (movingRight) {
            rb.velocity = new Vector3(velocity, 0, 0);//    transform.Translate(velocity, 0, 0);
        }
        else rb.velocity = new Vector3(-velocity, 0, 0);
        //transform.Translate(-velocity, 0, 0);
        */
    }

    public void ActivateTrigger() {
        active = true;
    }

    void OnDrawGizmosSelected()
    {
        if (checkpoints.Count > 0)
        {
            //     checkpoints[0] = transform.position;
            Gizmos.color = Color.blue;
            for (int j = 0; j < checkpoints.Count - 1; j++) { Gizmos.DrawLine(checkpoints[j], checkpoints[j + 1]); }
        }
    }

}
