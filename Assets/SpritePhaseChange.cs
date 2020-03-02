using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePhaseChange : MonoBehaviour
{
    public int phase;
    public Color color = Color.white;
    public Sprite sprite;
    SpriteRenderer SR;
    BossManager bossManager;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        bossManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<BossManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossManager.phase > phase) { UpdateSprite(); }
    }

    void UpdateSprite() {
        phase = bossManager.phase;
        //  if (sprite != null) SR.sprite = sprite;
        Color c = color;
        SR.color = c;
    }
}
