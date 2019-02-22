using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SaveMenuScript : MonoBehaviour {

    public Button save1Button;
    public Button save2Button;
    public Button save3Button;
    public Button returnButton;
    public GameObject mainMenu;
    public GameObject saveMenu;
    public EventSystem eventSystem;

    // Use this for initialization
    void Start () {
       save1Button.onClick.AddListener(delegate { SaveSlot1(); });
        save2Button.onClick.AddListener(delegate { SaveSlot2(); });
        save3Button.onClick.AddListener(delegate { SaveSlot3(); });
        returnButton.onClick.AddListener(delegate { ReturnToMain(); });
    }


    void SaveSlot1()
    {
    }


    void SaveSlot2()
    {
    }


    void SaveSlot3()
    {
    }


    void ReturnToMain()
    {
        mainMenu.SetActive(true); eventSystem.SetSelectedGameObject(mainMenu.transform.GetChild(0).transform.GetChild(0).gameObject); saveMenu.SetActive(false);
    }

}
