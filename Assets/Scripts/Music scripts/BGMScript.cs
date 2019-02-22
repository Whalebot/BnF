using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour
{

    public AudioSource musicSource;
    public AudioSource combatSource;
    public AudioSource triggerSource;
    public float BGMVolume;
    public float cBGMVolume;
    public AudioClip BGM1;
    public AudioClip combatMusic;
    bool inBattle;
    bool loweringVolume;
    public float volumeChange;

    // Use this for initialization
    void Start()
    {
        musicSource.Play();
        combatSource.Stop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        musicSource.volume = BGMVolume;
        combatSource.volume = cBGMVolume;
        if (EnclosedBattle.inBattle && !inBattle)
        {
            inBattle = true;
            musicSource.Pause();
            combatSource.Play();
        }
        else if (!EnclosedBattle.inBattle && inBattle) {
            musicSource.UnPause();
            combatSource.Stop();
            inBattle = false;
        }

        if (loweringVolume) { BGMVolume -= volumeChange; if (BGMVolume <= 0) loweringVolume = false; }
    }

    public void LowerBGM() {
        loweringVolume = true;
    }

    public void TriggerMusic() {
        triggerSource.Play();
    }
}
