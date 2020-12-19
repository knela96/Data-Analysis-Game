using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EventFilter
{
    Position,
    Death,
    LifeLost,
    Fall
};
public class EventData
{
    uint EventID;
    public EventFilter type;
    DateTime Timestamp;

    // We can get .json data from here
    public string GetJSON()
    {
        string json = JsonUtility.ToJson(this);
        return json;
    }
}
public class PlayerPositionEvent : EventData     // Player current position | Also used for Heatmap --------- Should we create a timer for saving this position every 3 seconds ?
{
    public Vector3 position;
}
public class PlayerDeathEvent : EventData    // Player death position & enemy who killed you | Also used for Heatmap
{
    public Vector3 position;
    public string enemy;
}
public class PlayerFallsEvent : EventData    // Player fall position & type of surface where player has fallen | Also used for Heatmap
{
    public Vector3 position;
    public string type; //  ¿String? Maybe there's an enum of the types of surface ---- MUST CHECK 
}
public class EnemyKillsEvent : EventData    // Enemy position where killed the player & enemy type name
{
    public Vector3 enemy_position;
    string enemy;
}
public class PlayerLifeLostEvent : EventData    // Player lost life position & enemy that damaged you | Also used for Heatmap
{
    public Vector3 position;
    string enemy;
}
public class SwitchesTimeEvent : EventData    // Time that player takes to press each switch
{
    int switch_id;
    float time;
}
public class TimePuzzleEvent : EventData    // Time to complete the puzzle
{
    float time;
}
public class ObjectsDestroyedEvent : EventData   // Objects destroyed & current destruction time
{
    public Vector3 position;
    float time;
}
public class FindKeyEvent : EventData   // Time to find the key
{
    float time;
}
public class TimeToFinishEvent : EventData   // Time to finish the level
{
    float time;
}
public class PlayerPathEvent : EventData    // Player position and rotation for debug path
{
    public Vector3 position;
    public Vector3 orientation;
}