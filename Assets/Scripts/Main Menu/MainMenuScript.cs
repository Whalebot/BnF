using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour {
    public Button startButton;
    public Button settingsButton;
    public Button creditsButton;
    public Button quitButton;
    public GameObject settingsMenu;
    public GameObject mainMenu;
    public GameObject saveMenu;
    public EventSystem eventSystem;

    public Button save1Button;
    public Button save2Button;
    public Button save3Button;
    public Button returnButton;

    public LoadingScreen loadingScreen;


    // Use this for initialization


    void Start () {
      //  loadingScreen.GetComponent<LoadingScreen>();

        startButton.onClick.AddListener(delegate { StartGame(); });
        settingsButton.onClick.AddListener(delegate { Settings(); });
        creditsButton.onClick.AddListener(delegate { Credits(); });
        quitButton.onClick.AddListener(delegate { Quit(); });

        save1Button.onClick.AddListener(delegate { SaveSlot1(); });
        save2Button.onClick.AddListener(delegate { SaveSlot2(); });
        save3Button.onClick.AddListener(delegate { SaveSlot3(); });
        returnButton.onClick.AddListener(delegate { ReturnToMain(); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void StartGame() { saveMenu.SetActive(true); eventSystem.SetSelectedGameObject(settingsMenu.transform.GetChild(0).transform.GetChild(0).gameObject); mainMenu.SetActive(false); }

    public void Settings() { settingsMenu.SetActive(true); eventSystem.SetSelectedGameObject(settingsMenu.transform.GetChild(0).transform.GetChild(0).gameObject); mainMenu.SetActive(false); } 

    public void Credits() { }

    void ReturnToMain()
    {
        mainMenu.SetActive(true); eventSystem.SetSelectedGameObject(mainMenu.transform.GetChild(0).transform.GetChild(0).gameObject); saveMenu.SetActive(false);
    }


    void SaveSlot1()
    {
        loadingScreen.StartCoroutine("LoadAsync", 1);
    }


    void SaveSlot2()
    {
        loadingScreen.StartCoroutine("LoadAsync", 1);
    }


    void SaveSlot3()
    {
        loadingScreen.StartCoroutine("LoadAsync", 1);
    }



    public void Quit() { }

}
