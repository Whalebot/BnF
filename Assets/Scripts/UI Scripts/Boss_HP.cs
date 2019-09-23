using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_HP : MonoBehaviour
{
    public EnemyScript enemyScript;
    Slider slider;
    // Use this for initialization
    void Start()
    {
        slider = GetComponent<Slider>();
        if (enemyScript == null) enemyScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyScript>();
        if (enemyScript != null) slider.maxValue = enemyScript.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyScript != null)
            slider.value = enemyScript.health;
    }
}
