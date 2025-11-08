using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //Built-in singleton
    private static T instance;
    private static readonly object _lock = new object();
    private static bool applicationIsQuitting = false;
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public static T Instance
    {
        get
        {
            if (applicationIsQuitting) return null;

            lock (_lock)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        GameObject singletonObj = new GameObject($"{typeof(T)} (Singleton)");
                        instance = singletonObj.AddComponent<T>();
                    }
                    DontDestroyOnLoad(instance.gameObject);
                }

                return instance;
            }
        }
    }

    protected virtual void OnApplicationQuit()
    {
        applicationIsQuitting = true;
    }
}
