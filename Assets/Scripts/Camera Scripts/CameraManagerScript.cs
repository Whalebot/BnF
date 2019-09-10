using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagerScript : MonoBehaviour
{
    public int currentZone;
    public GameObject[] zones;
    public GameObject[] cameras;
    public GameObject currentCamera;
    // Use this for initialization
    void Awake()
    {
        zones = GameObject.FindGameObjectsWithTag("Zone");
        cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        SortZones();
    }

    private void SortZones()
    {
        for (int i = 0; i < zones.Length; i++)
        {
            for (int j = 0; j < zones.Length; j++)
            {
                if (zones[i].name == ("Zone_" + j))
                {
                    SwapArrayElements(j, i);
                    break;
                }
            }
            zones[i] = GameObject.Find("Zone_" + i);
        }
        for (int i = 0; i < cameras.Length; i++)
        {
            for (int j = 0; j < cameras.Length; j++)
            {
                if (cameras[i].name == ("Main_Camera_" + j))
                {
                    SwapCameraElements(j, i);
                }
            }
            cameras[i] = GameObject.Find("Main_Camera_" + i);
        }
    }

    private void SwapArrayElements(int index1, int index2)
    {
        if (zones != null)
        {
            GameObject temp = zones[index1];
            zones[index1] = zones[index2];
            zones[index2] = temp;
        }

    }

    private void SwapCameraElements(int index1, int index2)
    {
        GameObject temp2 = cameras[index1];
        cameras[index1] = cameras[index2];
        cameras[index2] = temp2;
    }

    public void Zone(int zone)
    {
        currentZone = zone;
        currentCamera = cameras[zone];
        DisableCameras();
        cameras[zone].GetComponent<Camera>().enabled = true;
    }

    public void Zoom(Vector3 zoomTarget) { currentCamera.GetComponent<CameraScript>().SmoothZoom(zoomTarget); }

    void DisableCameras()
    {
        if (cameras != null) { for (int i = 0; i < cameras.Length; i++) { cameras[i].GetComponent<Camera>().enabled = false; } }
    }
}
