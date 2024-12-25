using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DataManage.isAlive == false && DataManage.enemyCount > 0)
        {
            gameOverText.text = "Game over";
        }
        if (DataManage.enemyCount <= 0 && DataManage.isEndGame == true)
        {
            gameOverText.text = "You win!";
        }
    }
}
