using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue {
    public string title;
    [TextArea(3,10)]
    public string[] sentences;
	public Vector3[] cameraPositions;
	public float[] smoothTimes;
	public float[] maxSpeeds;
	public float[] zooms;
	public float[] zoomTimes;
	public float[] maxZoomSpeeds;

}
