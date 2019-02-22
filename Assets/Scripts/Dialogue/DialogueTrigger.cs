using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    public Dialogue dialogue;

	public void Start(){
	}

    public void TriggerDialogue() {
        GameObject.FindGameObjectWithTag("Manager").GetComponent<DialogueManager>().StartDialogue(dialogue);
    }
    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) { TriggerDialogue(); Destroy(gameObject); }
    }

}
