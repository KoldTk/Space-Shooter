using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Replay : MonoBehaviour
{
    public Canvas gameOverMenu;
    // Start is called before the first frame update
    public void OnButtonClick()
    {
        SceneManager.LoadScene("Battle");
        Time.timeScale = 1.0f;
        gameOverMenu.enabled = false;
    }    
}
