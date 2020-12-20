using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EventFilter
{
    None = -1,
    Position,
    PlayerDeath,
    EnemyDeath,
    LifeLost,
    Fall,
    Objects
};
public enum ENEMY_TYPE
{
    ALL,
    SPITTER,
    CHOMPER
}
public enum SURFACE_TYPE
{
    ALL,
    FREE_FALL,
    ACID
}
public class EventData
{
    public static EventData eventdata;
    protected uint event_id;
    protected DateTime timestamp;
    public EventFilter type;

    public EventData()
    {
        event_id = 0;
        timestamp = System.DateTime.Now;
        type = EventFilter.None;
    }

    public EventData(uint ev, DateTime time, EventFilter eventfilter)
    {
        event_id = ev;
        timestamp = time;
        type = eventfilter;
    }

    public string GetJSON()
    {
        return JsonUtility.ToJson(this);
    }
}

public class PlayerPositionEvent : EventData     // Player current position | Also used for Heatmap --------- Should we create a timer for saving this position every 3 seconds ?
{
    public PlayerPositionEvent(uint ev, DateTime time, Vector3 pos) : base(ev, time, EventFilter.Position)
    {
        position = pos;
    }
    public Vector3 position;
}
public class PlayerDeathEvent : EventData    // Player death position | Also used for Heatmap
{
    public PlayerDeathEvent(uint ev, DateTime time, Vector3 pos) : base(ev, time, EventFilter.PlayerDeath)
    {
        position = pos;
    }
    public Vector3 position;
    ENEMY_TYPE enemy;
}
public class PlayerFallsEvent : EventData    // Player fall position & type of surface where player has fallen | Also used for Heatmap
{
    public PlayerFallsEvent(uint ev, DateTime time, Vector3 pos, SURFACE_TYPE surface_name) : base(ev, time, EventFilter.Fall)
    {
        position = pos;
        surface_type = surface_name;
    }
    public Vector3 position;
    public SURFACE_TYPE surface_type;
}
public class EnemyKillsEvent : EventData    // Enemy position where killed by the player & enemy type name
{
    public EnemyKillsEvent(uint ev, DateTime time, Vector3 enemy_pos, ENEMY_TYPE enemy_name) : base(ev, time, EventFilter.EnemyDeath)
    {
        position = enemy_pos;
        type = enemy_name;
    }
    public Vector3 position;
    public ENEMY_TYPE type;
}
public class PlayerLifeLostEvent : EventData    // Player lost life position & enemy that damaged you | Also used for Heatmap
{
    public PlayerLifeLostEvent(uint ev, DateTime time, Vector3 pos, ENEMY_TYPE enemy_name) : base(ev, time, EventFilter.LifeLost)
    {
        position = pos;
        type = enemy_name;
    }
    public Vector3 position;
    public ENEMY_TYPE type;
}
public class SwitchesTimeEvent : EventData    // Time that player takes to press each switch
{
    public SwitchesTimeEvent(uint ev, DateTime time, int switch_id, float ev_time) : base(ev, time, EventFilter.None)
    {
        current_switch_id = switch_id;
        global_time = ev_time;
    }
    int current_switch_id;
    float global_time;
}
public class TimePuzzleEvent : EventData    // Time to complete the puzzle
{
    public TimePuzzleEvent(uint ev, DateTime time, float ev_time) : base(ev, time, EventFilter.None)
    {
        global_time = ev_time;
    }
    float global_time;
}
public class ObjectsDestroyedEvent : EventData   // Objects destroyed & current destruction time
{
    public ObjectsDestroyedEvent(uint ev, DateTime time, Vector3 pos, float ev_time) : base(ev, time, EventFilter.Objects)
    {
        global_time = ev_time;
    }
    public Vector3 position;
    float global_time;
}
public class FindKeyEvent : EventData   // Time to find the key
{
    public FindKeyEvent(uint ev, DateTime time, float ev_time) : base(ev, time, EventFilter.None)
    {
        global_time = ev_time;
    }
    float global_time;
}
public class TimeToFinishEvent : EventData   // Time to finish the level
{
    public TimeToFinishEvent(uint ev, DateTime time, float ev_time) : base(ev, time, EventFilter.None)
    {
        global_time = ev_time;
    }
    float global_time;
}
public class PlayerPathEvent : EventData    // Player position and rotation for debug path
{
    public PlayerPathEvent(uint ev, DateTime time, Vector3 pos, Vector3 orient) : base(ev, time, EventFilter.None)
    {
        position = pos;
        orientation = orient;
    }
    public Vector3 position;
    public Vector3 orientation;
}

// ---------------------

// classes for lists
public class PositionEvent : EventData
{
    Vector3 position;

    public PositionEvent(uint ev, DateTime time, Vector3 pos) : base(ev, time, EventFilter.Position)
    {
        position = pos;
    }
}