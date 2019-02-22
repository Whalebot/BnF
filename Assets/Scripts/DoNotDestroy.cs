using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour {
    public static bool created;
    void Awake()
    {
        if (!created) { DontDestroyOnLoad(this); created = true; }
          else DestroyImmediate(gameObject);

    }

}
