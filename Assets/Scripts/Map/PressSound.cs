using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class PressSound : Selectable
{
    BaseEventData m_BaseEvent;
    bool playSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

  /*  // Update is called once per frame
    void Update()
    {
        if(IsHighlighted(m_BaseEvent) == true)
        {
            if (playSound != true)
            {
                GetComponent<AudioSource>().Play();
                playSound = true;
            }
        }
        if(IsHighlighted(m_BaseEvent) == false)
        {
            playSound = false;
        }
    }*/
}
