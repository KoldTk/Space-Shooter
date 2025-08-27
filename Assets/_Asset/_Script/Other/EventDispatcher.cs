using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Event
{
    SpawnEnemy, 
    SpawnAlly, 
    GainExp, 
    GainGold, 
    EnemyDie, //Enemy die, give exp and gold
    EnemyPass, //Enemy pass the gate, lose health
    LevelUp, //Level up, open level up menu
    GameOver,
    StageClear,
    WaveEnd,
}
public class EventDispatcher<T>
{
    private static Dictionary<string, Action<T>> eventTable = new();

    public static void AddListener(string eventName, Action<T> listener)
    {
        if (!eventTable.ContainsKey(eventName))
        {
            eventTable[eventName] = delegate { };
        }
        eventTable[eventName] += listener;
    }

    public static void RemoveListener(string eventName, Action<T> listener)
    {
        if (eventTable.ContainsKey(eventName))
        {
            eventTable[eventName] -= listener;
        }
    }

    public static void Dispatch(string eventName, T parameter)
    {
        if(eventTable.TryGetValue(eventName, out var action))
        {
            action.Invoke(parameter);
        }    
    }

    public static void ClearAll()
    {
        eventTable.Clear();
    }
}
