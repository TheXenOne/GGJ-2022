using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainGameplayScene : MonoBehaviour
{
    public List<string> _sceneToLoad;

    private void Awake()
    {
        foreach (var scene in _sceneToLoad)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }
}
