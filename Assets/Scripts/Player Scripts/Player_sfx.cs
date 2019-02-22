using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_sfx : MonoBehaviour
{

    public AudioClip Jump;
    public AudioClip Dash;
    public AudioClip Attack1;
    public AudioClip Attack2;
    public AudioClip Attack3;

    float initialpitch;

    // Use this for initialization
    void Start()
    {
        initialpitch = GetComponent<AudioSource>().pitch;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlaySoundID(int soundID)
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.75f, 1);
        switch (soundID)
        {
            case 1:
                GetComponent<AudioSource>().clip = Attack1;
                GetComponent<AudioSource>().Play(); return;
            case 2:
                GetComponent<AudioSource>().clip = Attack2;
                GetComponent<AudioSource>().Play(); return;
            case 3:
                GetComponent<AudioSource>().clip = Attack3;
                GetComponent<AudioSource>().Play(); return;
            default:
                GetComponent<AudioSource>().clip = Attack1;
                GetComponent<AudioSource>().Play(); return;
        }
    }

    public void PlayDash()
    {
        GetComponent<AudioSource>().pitch = 1;
     //   GetComponent<AudioSource>().clip = Dash;
     //   GetComponent<AudioSource>().Play();
    }

    public void PlayAttack1()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.75f, 1);
        GetComponent<AudioSource>().clip = Attack1;
        GetComponent<AudioSource>().Play();
    }
    public void PlayAttack2()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.75f, 1);
        GetComponent<AudioSource>().clip = Attack2;
        GetComponent<AudioSource>().Play();
    }

    public void PlayJump()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.75f, 1);
        GetComponent<AudioSource>().clip = Jump;
        GetComponent<AudioSource>().Play();
    }

}
