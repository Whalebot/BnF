using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour {
    public int time = 2;

	// Use this for initialization
	void Start () {

	}

    void OnEnable() { StartCoroutine("DestroyItem", time); }
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DestroyItem(int time) {
        yield return new WaitForSeconds(time);
        GameObject.Destroy(gameObject);
    }
}
