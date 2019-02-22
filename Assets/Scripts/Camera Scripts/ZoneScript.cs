using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    public int ZoneNR;
    public GameObject CameraManager;
    public GameObject skyBox;
    public CameraManagerScript camman;
    public bool isTest = false;

    // Use this for initialization
    void Start()
    {
        CameraManager = GameObject.FindGameObjectWithTag("CameraManager");
        //     CameraManager = transform.parent.gameObject;
        camman = CameraManager.GetComponent<CameraManagerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (camman == null) { CameraManager = GameObject.FindGameObjectWithTag("CameraManager"); camman = CameraManager.GetComponent<CameraManagerScript>(); }


        if (isTest) if (camman.currentZone == ZoneNR)
            {
                skyBox.SetActive(true);
            }
            else
            {
                skyBox.SetActive(false);
            }


        else if (camman.currentZone == ZoneNR)
        {
            skyBox.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            skyBox.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (camman == null) CameraManager = GameObject.Find("CameraManager"); camman = CameraManager.GetComponent<CameraManagerScript>();
            if (camman != null)
                camman.Zone(ZoneNR);
            else { CameraManager = GameObject.Find("CameraManager"); camman = CameraManager.GetComponent<CameraManagerScript>(); }
            NewPara.cameraToFollow = ZoneNR;
            //print(NewPara.cameraToFollow);
        }
    }
}
