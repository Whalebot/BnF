using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttractMode : MonoBehaviour
{
    public int timerCount;
    public int timerFrames;
    public static bool attractMode;
    public bool AMstart;

    // Use this for initialization
    void Start()
    {
        AMstart = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //    if (attractMode == true)
        //  {
        if (Input.GetButton("Attack") || Input.GetButton("Jump") || Input.GetButton("Start") || Input.GetAxis("Horizontal") != 0 || Input.GetButton("Start"))
        { AMstart = true;
            timerCount = 0;
        }

        if (Input.GetButton("Attack") || Input.GetButton("Jump") || Input.GetButton("Start") || Input.GetAxis("Horizontal") != 0 || Input.GetButton("Start") || Restart.checkpointNumber != 0)
        {
            AMstart = true;
        }

        if (AMstart) timerFrames++;
        if (timerFrames >= 60)
        {

            timerFrames = 0;
            timerCount++;
        }

        if (timerCount >= 30)
        {
            attractMode = true;
            Restart.checkpointNumber = 0;
            SceneManager.LoadScene(2);
            //  PauseMenu.gameIsPaused = true;

        }
        if (attractMode == true)
        {
            if (Input.GetButton("Attack") || Input.GetButton("Jump") || Input.GetButton("Start") || Input.GetAxis("Horizontal") != 0 || Input.GetButton("Start"))
            {
                attractMode = false;
                SceneManager.LoadScene(0);
                //           AMstart = true;
                //           timerCount = 0; attractMode = false; PauseMenu.gameIsPaused = false;
            }
        }
        // }
    }
}
