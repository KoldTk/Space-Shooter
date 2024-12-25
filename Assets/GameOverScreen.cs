using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public Canvas gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (DataManage.isAlive == false || DataManage.isEndGame == true)
        {
            gameOverScreen.enabled = true;
        }    
    }
}
