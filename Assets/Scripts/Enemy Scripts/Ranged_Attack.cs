using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Attack : MonoBehaviour
{

    [HeaderAttribute("Ranged attributes")]
    public GameObject fireball;
    int RNGCount;
    EnemyScript enemyScript;
    GameObject target;
    Rigidbody2D rb;
    public Vector3 direction;
    public bool whiffable;


    void Start() { }


    void Awake()
    {
        enemyScript = gameObject.transform.parent.parent.GetComponent<EnemyScript>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        {
            direction = ((target.transform.position + Vector3.down*3.5F) - transform.position ).normalized;
            if (whiffable)
            {
                if (transform.parent.parent.localScale.x > 0)
                {
                    if (enemyScript.mode == 2)
                    {
                        Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 360.0f - Vector3.Angle(Vector3.up, direction) + 15));
                        Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 360.0f - Vector3.Angle(Vector3.up, direction) - 15));
                        Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 360.0f - Vector3.Angle(Vector3.up, direction)));
                    }
                    else
                    {
                        Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 360.0f - Vector3.Angle(Vector3.up, direction)));
                        //    Instantiate(fireball, transform.position, Quaternion.Euler(direction));
                    }
                }
                else
                {
                    if (enemyScript.mode == 2)
                    {
                        Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 360.0f + Vector3.Angle(Vector3.up, direction) + 15));
                        Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 360.0f + Vector3.Angle(Vector3.up, direction) - 15));
                        Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 360.0f + Vector3.Angle(Vector3.up, direction)));
                    }
                    else
                        Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 360.0f + Vector3.Angle(Vector3.up, direction)));
                }
            }

            else {
                if (direction.x > 0)
                {
                    if (enemyScript.mode == 2)
                    {
                        Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 360.0f - Vector3.Angle(Vector3.up, direction) + 15));
                        Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 360.0f - Vector3.Angle(Vector3.up, direction) - 15));
                        Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 360.0f - Vector3.Angle(Vector3.up, direction)));
                    }
                    else
                    {
                        Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 360.0f - Vector3.Angle(Vector3.up, direction)));
                        //    Instantiate(fireball, transform.position, Quaternion.Euler(direction));
                    }
                }
                else
                {
                    if (enemyScript.mode == 2)
                    {
                        Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 360.0f + Vector3.Angle(Vector3.up, direction) + 15));
                        Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 360.0f + Vector3.Angle(Vector3.up, direction) - 15));
                        Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 360.0f + Vector3.Angle(Vector3.up, direction)));
                    }
                    else
                        Instantiate(fireball, transform.position, Quaternion.Euler(0, 0, 360.0f + Vector3.Angle(Vector3.up, direction)));
                }
            }
        }
    }
}
