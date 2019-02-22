using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackParticleFlip : MonoBehaviour {
  //  GameObject player;
    public float lifetime;
	// Use this for initialization
	void Start () {
        StartCoroutine("Destroy", lifetime);
      //  player = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {

       // transform.localScale = player.transform.localScale;
	}

    IEnumerator Destroy(float lifetime) {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
