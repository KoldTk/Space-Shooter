using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreMainMenu : MonoBehaviour
{
    public Text bestScore;
    // Start is called before the first frame update
    void Start()
    {
        DataManage.LoadData();
        bestScore.text = $"Best Score: {DataManage.highScore}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
