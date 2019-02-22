using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Unsheath : MonoBehaviour {
    Animator anim;
    public int ID;
    public MenuController menu;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (menu.selectedWeapons.Contains(ID)) anim.SetBool("Selected", true);
        else anim.SetBool("Selected", false);
    }
}
