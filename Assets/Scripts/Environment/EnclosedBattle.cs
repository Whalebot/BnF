using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnclosedBattle : MonoBehaviour
{
    public int encounterID;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject combatEndSound;
    public float endDelay;

    public bool activated;
    public bool disabled;
    public List<GameObject> triggerEnemies;
    public List<bool> killedEnemies;
    public static bool inBattle;
    bool alive;
    // Use this for initialization
    StageManager stageManager;

    void Start()
    {
        stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();
        inBattle = false;
        for (int i = 0; i < triggerEnemies.Count; i++)
        {
            if (stageManager.encounterList.Count > encounterID)
                if (stageManager.encounterList[encounterID].enemies.Count > i)
                    if (stageManager.encounterList[encounterID].enemies[i])
                    {
                        Destroy(triggerEnemies[i]);
                    }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < triggerEnemies.Count; i++)
        {
            if (stageManager.encounterList.Count > encounterID)
                if (stageManager.encounterList[encounterID].enemies.Count > i)
                    if (triggerEnemies[i] == null) stageManager.encounterList[encounterID].enemies[i] = true;
        }

        if (!disabled && activated)
        {
            bool anyAlive = false;
            for (int i = 0; i < triggerEnemies.Count; i++)
            {
                //if (triggerEnemies[i] == null) stageManager.encounterList[encounterID].enemies[i] = true;
                if (triggerEnemies[i] != null)
                    if (!triggerEnemies[i].GetComponent<EnemyScript>().knockout) anyAlive = true;
            }

            if (!anyAlive)
            {
                LowerWalls();

                if (combatEndSound != null)
                    Instantiate(combatEndSound, transform.position, Quaternion.identity);
                StartCoroutine("endFight");
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            activated = true;
            RaiseWalls();
            inBattle = true;
        }
    }

    void RaiseWalls()
    {
        leftWall.SetActive(true);
        rightWall.SetActive(true);
    }

    void LowerWalls()
    {
        disabled = true;
        leftWall.SetActive(false);
        rightWall.SetActive(false);
    }

    IEnumerator endFight()
    {
        yield return new WaitForSeconds(endDelay);
        inBattle = false;
    }
}
