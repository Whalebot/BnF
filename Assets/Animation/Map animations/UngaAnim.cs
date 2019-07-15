using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UngaAnim : MonoBehaviour
{
    Animator anim;
    public GameObject doors;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Next());
        }
    }

    IEnumerator Next()
    {
        anim.SetTrigger("play");
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }

}
