using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class StageManager : MonoBehaviour
{
    public List<EncounterList> encounterList ;
    public int startScene;

    void Awake() {
        startScene = SceneManager.GetActiveScene().buildIndex;
    }

    void Update() {
        if (startScene != SceneManager.GetActiveScene().buildIndex) {
            DoNotDestroy.created = false;
            Destroy(gameObject);
        }
    }
}
