using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreGameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreGameOverMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        highScoreGameOverMenu.text = $"Score: {DataManage.score}";
    }
}
