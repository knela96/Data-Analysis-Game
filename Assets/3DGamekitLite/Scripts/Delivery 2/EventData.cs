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
    public uint player_id;
    public DateTime timestamp;

    public EventData()
    {
        player_id = 0;
        timestamp = System.DateTime.Now;
    }

    public EventData(uint id, DateTime time)
    {
        player_id = id;
        timestamp = time;
    }

    public string GetJSON()
    {
        return JsonUtility.ToJson(this);
    }
}

public class PlayerPositionEvent : EventData     // Player current position | Also used for Heatmap --------- Should we create a timer for saving this position every 3 seconds ?
{
    public PlayerPositionEvent(uint ev, DateTime time, Vector3 pos) : base(ev, time)
    {
        x = pos.x;
        y = pos.y;
        z = pos.z;
    }
    public float x, y, z;
}
public class PlayerDeathEvent : EventData    // Player death position | Also used for Heatmap
{
    public PlayerDeathEvent(uint ev, DateTime time, Vector3 pos, ENEMY_TYPE enemy) : base(ev, time)
    {
        x = pos.x;
        y = pos.y;
        z = pos.z;
        this.enemy = (int)enemy;
    }
    public float x, y, z;
    public int enemy;
}
public class PlayerFallsEvent : EventData    // Player fall position & type of surface where player has fallen | Also used for Heatmap
{
    public PlayerFallsEvent(uint ev, DateTime time, Vector3 pos, SURFACE_TYPE surface_name) : base(ev, time)
    {
        x = pos.x;
        y = pos.y;
        z = pos.z;
        surface = (int)surface_name;
    }
    public float x,y,z;
    public int surface;
}
public class EnemyKillsEvent : EventData    // Enemy position where killed by the player & enemy type name
{
    public EnemyKillsEvent(uint ev, DateTime time, Vector3 enemy_pos, ENEMY_TYPE enemy_name) : base(ev, time)
    {
        x = enemy_pos.x;
        y = enemy_pos.y;
        z = enemy_pos.z;
        enemy = (int)enemy_name;
    }
    public float x, y, z;
    public int enemy;
}
public class PlayerLifeLostEvent : EventData    // Player lost life position & enemy that damaged you | Also used for Heatmap
{
    public PlayerLifeLostEvent(uint ev, DateTime time, Vector3 pos, ENEMY_TYPE enemy_name) : base(ev, time)
    {
        x = pos.x;
        y = pos.y;
        z = pos.z;
        enemy = (int)enemy_name;
    }
    public float x, y, z;
    public int enemy;
}
public class SwitchesTimeEvent : EventData    // Time that player takes to press each switch
{
    public SwitchesTimeEvent(uint ev, DateTime time, int switch_id, float ev_time) : base(ev, time)
    {
        current_switch_id = switch_id;
        global_time = ev_time;
    }
    public int current_switch_id;
    public float global_time;
}
public class FindKeyEvent : EventData   // Time to find the key
{
    public FindKeyEvent(uint ev, DateTime time, float ev_time) : base(ev, time)
    {
        global_time = ev_time;
    }
    public float global_time;
}
public class TimeToFinishEvent : EventData   // Time to finish the level
{
    public TimeToFinishEvent(uint ev, DateTime time, float ev_time) : base(ev, time)
    {
        global_time = ev_time;
    }
    public float global_time;
}
public class PlayerPathEvent : EventData    // Player position and rotation for debug path
{
    public PlayerPathEvent(uint ev, DateTime time, Vector3 pos, Vector3 orient) : base(ev, time)
    {
        x = pos.x;
        y = pos.y;
        z = pos.z;

        ex = orient.x;
        ey = orient.y;
        ez = orient.z;
    }
    public float x, y, z;
    public float ex, ey, ez;
}