using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_HP : MonoBehaviour {
    public EnemyScript enemyScript;
    Slider slider;
	// Use this for initialization
	void Start () {
        slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {

        slider.value = enemyScript.health;
    }
}
