using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public int phase;
    public GameObject boss;

    EnemyScript enemyScript;
    // Start is called before the first frame update
    void Start()
    {
        enemyScript = boss.GetComponent<EnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {
        phase = enemyScript.mode;
    }
}
