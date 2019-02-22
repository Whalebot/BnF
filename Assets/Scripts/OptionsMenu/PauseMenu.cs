using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {
    public EventSystem eventSystem;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public Button resumeButton;
    public Button settingsButton;
    public Button menuButton;
    public Button quitButton;

    public static bool gameIsPaused = false;


    // Use this for initialization
    void Start () {
        resumeButton.onClick.AddListener(delegate { ResumeGame(); });
        settingsButton.onClick.AddListener(delegate { goToSettings(); });
        menuButton.onClick.AddListener(delegate { ResumeGame(); });
        quitButton.onClick.AddListener(delegate { quitGame(); });
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Start")) {
            if (!gameIsPaused) { PauseGame(); }
            else { ResumeGame(); }
        }
	}

    void PauseGame() { gameIsPaused = true; pauseMenu.SetActive(true); Time.timeScale = 0; }

    void ResumeGame() { gameIsPaused = false; pauseMenu.SetActive(false); Time.timeScale = 1; settingsMenu.SetActive(false); }

    void goToSettings() { settingsMenu.SetActive(true); eventSystem.SetSelectedGameObject(settingsMenu.transform.GetChild(0).transform.GetChild(0).gameObject); pauseMenu.SetActive(false);}

    void goToMenu() { Debug.Log("Going to menu"); }

    void quitGame() { Application.Quit(); }


}
