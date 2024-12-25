using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManage : MonoBehaviour
{
    public static int score;
    public static bool isAlive = true;
    public static bool isEndGame = false;
    public static int highScore;
    public static int enemyCount;


    private static string filePath = "Assets/Score.txt";
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        score = 0;
        isAlive = true;
        isEndGame = false;
        enemyCount = 50;
}

    // Update is called once per frame
    void Update()
    {
        if (score >= highScore)
        {
            highScore = score;
            SaveData(highScore);
        }
    }

    public static void SaveData(int score)
    { 
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }
        File.WriteAllText(filePath, score.ToString());
    }

    public static int LoadData()
    {
        if (File.Exists(filePath))
        {
            string fileData = File.ReadAllText(filePath);
            highScore = int.Parse(fileData);
            return highScore;
        }
        else
        {
            Debug.LogWarning("File không tồn tại.");
            return 0;
        }
    }
}
