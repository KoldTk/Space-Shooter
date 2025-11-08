using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonLoader : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadSceneCoroutine("Main Menu"));
    }
    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            Debug.Log($"Loading progress: {asyncLoad.progress}");
            yield return null;
        }
    }
}
