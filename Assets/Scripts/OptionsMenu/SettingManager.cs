using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class SettingManager : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Dropdown textureQualityDropdown;
    public Dropdown antialiasingDropdown;
    public Dropdown vSyncDropdown;
    public Slider masterVolumeSlider;
    public Slider FXVolumeSlider;
    public Slider BGMVolumeSlider;
    public Button applyButton;

    public AudioMixer audioMixer;
    public Resolution[] resolutions;
    public GameSettings gameSettings;

    void Update() { }
    void OnEnable()
    {
        gameSettings = new GameSettings();
        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        masterVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        FXVolumeSlider.onValueChanged.AddListener(delegate { OnFXVolumeChange(); });
        BGMVolumeSlider.onValueChanged.AddListener(delegate { OnBGMVolumeChange(); });
        applyButton.onClick.AddListener(delegate { OnApplyButton(); });


        resolutions = Screen.resolutions;
        foreach (Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
        if (File.Exists(Application.persistentDataPath + "/gamesettings.json") == true)
        {
            LoadSettings();
        }
    }

    public void OnFullscreenToggle()
    {
        gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }
    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;
    }

    public void OnTextureQualityChange()
    {
        QualitySettings.masterTextureLimit = gameSettings.textureQuality = textureQualityDropdown.value;
    }

    public void OnAntialiasingChange()
    {
        QualitySettings.antiAliasing = gameSettings.antialiasing = (int)Mathf.Pow(2, antialiasingDropdown.value);
    }

    public void OnVSyncChange()
    {
        QualitySettings.vSyncCount = gameSettings.vSync = vSyncDropdown.value;
    }

    public void OnMusicVolumeChange()
    {
        audioMixer.SetFloat("Master", masterVolumeSlider.value);
        gameSettings.masterVolume = masterVolumeSlider.value;

    }
    public void OnFXVolumeChange()
    {
        audioMixer.SetFloat("FX", FXVolumeSlider.value);
        gameSettings.FXVolume = FXVolumeSlider.value;

    }
    public void OnBGMVolumeChange()
    {
        audioMixer.SetFloat("BGM",BGMVolumeSlider.value);
        gameSettings.BGMVolume = BGMVolumeSlider.value;

    }

    public void OnApplyButton() { SaveSettings(); eventSystem.SetSelectedGameObject(pauseMenu.transform.GetChild(0).transform.GetChild(0).gameObject); pauseMenu.SetActive(true); settingsMenu.SetActive(false); }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }
    public void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        textureQualityDropdown.value = gameSettings.textureQuality;
        resolutionDropdown.value = gameSettings.resolutionIndex;
        fullscreenToggle.isOn = gameSettings.fullscreen;
        antialiasingDropdown.value = gameSettings.antialiasing;
        vSyncDropdown.value = gameSettings.vSync;
        masterVolumeSlider.value = gameSettings.masterVolume;
        FXVolumeSlider.value = gameSettings.FXVolume;
        BGMVolumeSlider.value = gameSettings.BGMVolume;

        resolutionDropdown.RefreshShownValue();
    }




}
