using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWaveScript : MonoBehaviour
{
    public GameObject waveObject;
   // Rigidbody2D rb;




    void OnEnable()
    {
   //     Instantiate(waveObject, transform.position, Quaternion.Euler(0, 0, 360.0f - Vector3.Angle(Vector3.up, Vector3.left)));
   //     Instantiate(waveObject, transform.position, Quaternion.Euler(0, 0, 360.0f + Vector3.Angle(Vector3.up, -Vector3.left)));
    }
    // Use this for initialization
    void Start()
    {
       // rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Platform")) {
            StartWave();
            print("GroundSlamHit");
        }
            
            }

    void StartWave() {
        Instantiate(waveObject, transform.position, Quaternion.Euler(0, 0, 360.0f - Vector3.Angle(Vector3.up, Vector3.left)));
        Instantiate(waveObject, transform.position, Quaternion.Euler(0, 0, 360.0f + Vector3.Angle(Vector3.up, -Vector3.left)));
    }
}
