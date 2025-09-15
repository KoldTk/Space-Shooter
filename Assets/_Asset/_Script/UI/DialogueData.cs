using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData
{
    public ActorData[] actors;
    public MessageData[] messages; 
}

[System.Serializable]
public class MessageData
{
    public int actorId;
    public string message;
}
[System.Serializable]
public class ActorData
{
    public int id;
    public string name;
    public string spritePath;
}
