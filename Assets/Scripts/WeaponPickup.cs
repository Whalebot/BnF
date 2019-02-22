using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject moveset1;
    public GameObject player;
    public GameObject pickupFx;
    bool pickedUp;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!pickedUp)
            if (other.CompareTag("Player"))
            {
                Instantiate(pickupFx, transform.position, Quaternion.identity);
                pickedUp = true;
                player.GetComponent<Player_AttackScript>().moveset1.SetActive(false);
                player.GetComponent<Player_AttackScript>().moveset1 = moveset1;
                player.GetComponent<Player_AttackScript>().MovesetChange(1);
            }
    }
}
