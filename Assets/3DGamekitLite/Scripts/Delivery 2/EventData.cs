using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventData
{
    uint EventID;

    // We can get .json data from here
    public string GetJSON()
    {
        string json = JsonUtility.ToJson(this);
        return json;
    }
}
public class PlayerPositionEvent : EventData     // Player current position | Also used for Heatmap --------- Should we create a timer for saving this position every 3 seconds ?
{
    Vector3 position;
}
public class DeathEvent : EventData    // Player death position & enemy who killed you | Also used for Heatmap
{
    Vector3 position;
    string enemy;
}
public class PlayerFallsEvent : EventData    // Player fall position & type of surface where player has fallen | Also used for Heatmap
{
    Vector3 position;
    string type; //  ¿String? Maybe there's an enum of the types of surface ---- MUST CHECK 
}
public class EnemyKillsEvent : EventData    // Enemy position where killed the player & enemy type name
{
    Vector3 enemy_position;
    string enemy;
}
public class PlayerLifeLostEvent : EventData    // Player lost life position & enemy that damaged you | Also used for Heatmap
{
    Vector3 position;
    string enemy;
}
public class SwitchsTimeEvent : EventData    // Time that player takes to press each switch
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
    Vector3 position;
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
    Vector3 position;
    Vector3 orientation;
}