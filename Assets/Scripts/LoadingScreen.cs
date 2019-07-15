using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadingScreen : MonoBehaviour
{
    public bool wontOpen;
    bool countStart;
    public int counter;
    int count;
    public Animator doorAnim;

    void Start() {
        doorAnim = doorAnim.gameObject.GetComponent<Animator>();
        if(!wontOpen)
        doorAnim.SetInteger("WillOpen", 1);
    }

    void Update()
    {
        if (countStart) count++;

        if (Input.GetKeyDown("p"))
        {
            StartCoroutine("LoadAsync",1);
        }

    }

    public void LoadLevel()
    {

    }

    public IEnumerator LoadSame()
    {
        doorAnim.SetInteger("WillOpen", -1);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        countStart = true;

        while (count < counter && !operation.isDone) { operation.allowSceneActivation = false; print("pizza"); yield return null; operation.allowSceneActivation = true; }
    }

    public IEnumerator LoadAsync(int sceneIndex)
    {
        doorAnim.SetInteger("WillOpen", -1);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        countStart = true;

        while (count < counter && !operation.isDone) { operation.allowSceneActivation = false; print("pizza"); yield return null; operation.allowSceneActivation = true; }
    }
}
