using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    static int dontDestroyCount = 0;

    private void Awake()
    {
        ++dontDestroyCount;

        if (dontDestroyCount == 1)
            DontDestroyOnLoad(this.gameObject);
        else
            Destroy(this.gameObject);
    }
}
