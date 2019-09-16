using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    public GameObject player;
    public GameObject startPosition;
    public GameObject defaultPosition;
    public int restartScene = 0;
    public static int checkpointNumber;
    public static Vector3 startPos;
    public Vector3 currentCheckpoint;
    public bool canSpawn;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;

    public GameObject boss1;

    public GameObject boss2;
    public GameObject boss3;

    // Use this for initialization

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // checkpointNumber = 0;

    }
    void Start()
    {
        if (checkpointNumber == 0) startPos = defaultPosition.transform.position;
        player.transform.position = startPos;
        //      if (checkpointNumber == 1) startPos = startPosition.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //  currentCheckpoint = startPos;
        if (Input.GetButtonDown("Select")) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (Input.GetKeyDown(KeyCode.F1)) { SceneManager.LoadScene(0); checkpointNumber = 0; }
        if (Input.GetKeyDown(KeyCode.F2)) { SceneManager.LoadScene(1); checkpointNumber = 0; }
        if (Input.GetKeyDown(KeyCode.F3)) { SceneManager.LoadScene(2); checkpointNumber = 0; }
        if (Input.GetKeyDown(KeyCode.F4)) { SceneManager.LoadScene(3); checkpointNumber = 0; }
        if (Input.GetKeyDown(KeyCode.F5)) { SceneManager.LoadScene(4); checkpointNumber = 0; }
        if (canSpawn)
        {
            if (Input.GetKeyDown("1")) Instantiate(enemy1, transform.position, Quaternion.identity, transform);
            if (Input.GetKeyDown("2")) Instantiate(enemy2, transform.position, Quaternion.identity, transform);
            if (Input.GetKeyDown("3")) Instantiate(enemy3, transform.position, Quaternion.identity, transform);
            if (Input.GetKeyDown("4")) Instantiate(enemy4, transform.position, Quaternion.identity, transform);
            if (Input.GetKeyDown("5")) Instantiate(boss1, transform.position, Quaternion.identity, transform);
            if (Input.GetKeyDown("6")) Instantiate(boss2, transform.position, Quaternion.identity, transform);
            if (Input.GetKeyDown("7")) Instantiate(boss3, transform.position, Quaternion.identity, transform);
            if (Input.GetKeyDown("8")) player.GetComponent<PlayerStatus>().sword2Alive = true;
            if (Input.GetKeyDown("9")) player.GetComponent<PlayerStatus>().sword3Alive = true;
        }
    }
}
