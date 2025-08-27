using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager
{
    private static SaveDataManager _instance;
    public static SaveDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SaveDataManager();
            }
            return _instance;
        }
    }
}
