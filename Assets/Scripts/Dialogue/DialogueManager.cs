using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text titleText;
    public Text dialogueText;
    public Animator anim;

    public Queue<string> sentences;

    public Queue<Vector3> cameraPositions;

	public Queue<float> smoothTimes;

	public Queue<float> maxSpeeds;

	public Queue<float> zoomTargets;

	public Queue<float> zoomTimes;

	public Queue<float> maxZoomSpeeds;

    public Transform eventCamera;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
        cameraPositions = new Queue<Vector3>();
		zoomTargets = new Queue<float>();
        smoothTimes = new Queue<float>();
        maxSpeeds = new Queue<float>();
        zoomTimes = new Queue<float>();
        maxZoomSpeeds = new Queue<float>();
    }
	void Update() { if (Input.GetButtonDown("Submit") && PauseMenu.gameIsPaused) { DisplayNextSentence(); NextCameraPosition(); NextSmoothTime (); NextMaxSpeed (); NextZoom(); NextZoomTime (); NextMaxZoomSpeed(); } }

    public void StartDialogue(Dialogue dialogue)
    {
        PauseMenu.gameIsPaused = true;
        eventCamera.gameObject.SetActive(true);
        anim.SetBool("Dialogue", true);
        titleText.text = dialogue.title;
        sentences.Clear();
        cameraPositions.Clear();
		zoomTargets.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (Vector3 cameraPosition in dialogue.cameraPositions)
        {
            cameraPositions.Enqueue(cameraPosition);
        }
		foreach (float smoothTime in dialogue.smoothTimes) 
		{
			smoothTimes.Enqueue(smoothTime);
		}
		foreach (float maxSpeed in dialogue.maxSpeeds) 
		{
			maxSpeeds.Enqueue(maxSpeed);
		}
		foreach (float zoomTarget in dialogue.zooms) 
		{
			zoomTargets.Enqueue(zoomTarget);
		}
		foreach (float zoomTime in dialogue.zoomTimes) 
		{
			zoomTimes.Enqueue(zoomTime);
		}
		foreach (float maxZoomSpeed in dialogue.maxSpeeds) 
		{
			maxZoomSpeeds.Enqueue(maxZoomSpeed);
		}
		DisplayNextSentence();
        NextCameraPosition();
		NextSmoothTime ();
		NextMaxSpeed();
		NextZoom();
		NextZoomTime();
		NextMaxZoomSpeed ();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void NextCameraPosition()
    {
        if (cameraPositions.Count == 0)
        {
            return;
        }
//        print("Next position");
        Vector3 cameraPosition = cameraPositions.Dequeue();
		eventCamera.GetComponent<EventCamera> ().targetPosition = cameraPosition;
    }
	public void NextSmoothTime()
	{
		if (smoothTimes.Count == 0) {
			return;
		}
		float smoothTime = smoothTimes.Dequeue ();
		eventCamera.GetComponent<EventCamera> ().smoothTime = smoothTime;
	}
	public void NextMaxSpeed()
	{
		if (maxSpeeds.Count == 0) {
			return;
		}
		float maxSpeed = maxSpeeds.Dequeue ();
		eventCamera.GetComponent<EventCamera> ().maxSpeed = maxSpeed;
	}
	public void NextZoom(){
		if (zoomTargets.Count == 0) {
			return;
		}
        print("Next zoom");
        float zoomTarget = zoomTargets.Dequeue ();
		eventCamera.GetComponent<EventCamera> ().zoom = zoomTarget;
	}
	public void NextZoomTime()
	{
		if (zoomTimes.Count == 0) {
			return;
		}
		float zoomTime = zoomTimes.Dequeue ();
		eventCamera.GetComponent<EventCamera> ().zoomTime = zoomTime;
	}
	public void NextMaxZoomSpeed()
	{
		if (zoomTimes.Count == 0) {
			return;
		}	
		float maxZoomSpeed = maxZoomSpeeds.Dequeue ();
		eventCamera.GetComponent<EventCamera> ().zoomMaxSpeed = maxZoomSpeed;
	}
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        GameObject.FindGameObjectWithTag("Boss").GetComponent<EnemyScript>().detected = true;
        PauseMenu.gameIsPaused = false;
        eventCamera.gameObject.SetActive(false);
        anim.SetBool("Dialogue", false);
    }

}
