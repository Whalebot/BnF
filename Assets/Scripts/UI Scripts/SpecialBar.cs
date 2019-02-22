using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBar : MonoBehaviour
{
    public int flowerNumber;
    public int totalFlowers;
    public bool transparent = true;
    Slider slider;
    Image image;
    Color c;
    GameObject player;
    // Use this for initialization
    void Start()
    {
        image = transform.GetChild(1).GetChild(0).gameObject.GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");
        slider = GetComponent<Slider>();
  
        slider.maxValue = 60 / totalFlowers;
        c = image.color;

    }

    // Update is called once per frame
    void Update()
    {
        if (!transparent)
            slider.value = player.GetComponent<PlayerStatus>().special - (60 / totalFlowers) * flowerNumber;
        else
        {if((player.GetComponent<PlayerStatus>().special - (60 / totalFlowers) * flowerNumber) / (60 / totalFlowers) >= 1)
            c.a = (player.GetComponent<PlayerStatus>().special - (60 / totalFlowers) * flowerNumber) / (60 / totalFlowers);
        else
            c.a = (player.GetComponent<PlayerStatus>().special - (60 / totalFlowers) * flowerNumber) / (60 / totalFlowers) * 0.6F;
            image.color = c;
        }
    }
}
