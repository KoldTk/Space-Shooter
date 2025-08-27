using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    // Start is called before the first frame update
    public void SwitchScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
        
    }
}
