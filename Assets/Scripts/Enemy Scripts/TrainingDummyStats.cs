using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingDummyStats : MonoBehaviour {
    public EnemyScript enemyScript;
    public Slider hitstun;
    public Text comboHits;
    public Text moveDamage;
    public Text comboDamage;
	// Use this for initialization
	void Start () {
        //     enemyScript.GetComponent<EnemyScript>();
        //       hitstun = GetComponent<Slider>();
        //   comboHits = GetComponent<Text>();
        //  moveDamage =  GetComponent<Text>();
        // comboDamage =  GetComponent<Text>();
        comboHits.text = "Hits: " + enemyScript.comboHits.ToString();
        comboDamage.text = "Combo: " + enemyScript.comboDamage.ToString();
        moveDamage.text = "Damage: " + enemyScript.moveDamage.ToString();
    }
	
	// Update is called once per frame
	void Update () {

        hitstun.value = enemyScript.hitstun;
        if (enemyScript.comboDamage != 0) UpdateStats();
    }

    void UpdateStats()
    {
        comboHits.text = "Hits: " + enemyScript.comboHits.ToString();
        comboDamage.text = "Combo: " + enemyScript.comboDamage.ToString();
        moveDamage.text = "Damage: " + enemyScript.moveDamage.ToString();
    }
}
