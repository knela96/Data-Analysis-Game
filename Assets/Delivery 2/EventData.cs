﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EventFilter
{
    None = -1,
    Position,
    PlayerDeath,
    Fall,
    LifeLost,
    EnemyDeath
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
    //public static EventData eventdata;
    public uint player_id;
    public uint session_id;
    public DateTime timestamp;

    public EventData()
    {
        player_id = 0;
        session_id = 0;
        timestamp = System.DateTime.Now;
    }

    public EventData(uint _sessions_id,uint _player_id, DateTime time)
    {
        session_id = _sessions_id;
        player_id = _player_id;
        timestamp = time;
    }

    public string GetJSON()
    {
        return JsonUtility.ToJson(this);
    }
}

public class PlayerPositionEvent : EventData     // Player current position | Also used for Heatmap --------- Should we create a timer for saving this position every 3 seconds ?
{
    public PlayerPositionEvent()
    {
        x = 0;
        y = 0;
        z = 0;
    }

    public PlayerPositionEvent(uint session_id, uint player_id, DateTime time, Vector3 pos) : base(session_id,player_id, time)
    {
        x = pos.x;
        y = pos.y;
        z = pos.z;
    }
    public float x, y, z;
}
public class PlayerDeathEvent : EventData    // Player death position | Also used for Heatmap
{
    public PlayerDeathEvent()
    {
        x = 0;
        y = 0;
        z = 0;
        enemy = 0;
    }

    public PlayerDeathEvent(uint session_id, uint player_id, DateTime time, Vector3 pos, ENEMY_TYPE enemy) : base(session_id, player_id, time)
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
    public PlayerFallsEvent()
    {
        x = 0;
        y = 0;
        z = 0;
        surface = 0;
    }

    public PlayerFallsEvent(uint session_id, uint player_id, DateTime time, Vector3 pos, SURFACE_TYPE surface_name) : base(session_id, player_id, time)
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
    public EnemyKillsEvent()
    {
        x = 0;
        y = 0;
        z = 0;
        enemy = 0;
    }

    public EnemyKillsEvent(uint session_id, uint player_id, DateTime time, Vector3 enemy_pos, ENEMY_TYPE enemy_name) : base(session_id, player_id, time)
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
    public PlayerLifeLostEvent()
    {
        x = 0;
        y = 0;
        z = 0;
        enemy = 0;
    }

    public PlayerLifeLostEvent(uint session_id, uint player_id, DateTime time, Vector3 pos, ENEMY_TYPE enemy_name) : base(session_id, player_id, time)
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
    public SwitchesTimeEvent(uint session_id, uint player_id, DateTime time, int switch_id, float ev_time) : base(session_id, player_id, time)
    {
        current_switch_id = switch_id;
        global_time = ev_time;
    }
    public int current_switch_id;
    public float global_time;
}
public class FindKeyEvent : EventData   // Time to find the key
{
    public FindKeyEvent(uint session_id, uint player_id, DateTime time, float ev_time) : base(session_id, player_id, time)
    {
        global_time = ev_time;
    }
    public float global_time;
}
public class TimeToFinishEvent : EventData   // Time to finish the level
{
    public TimeToFinishEvent(uint session_id, uint player_id, DateTime time, float ev_time) : base(session_id, player_id, time)
    {
        global_time = ev_time;
    }
    public float global_time;
}
public class PlayerPathEvent : EventData    // Player position and rotation for debug path
{
    public PlayerPathEvent()
    {
        x = 0;
        y = 0;
        z = 0;

        ex = 0;
        ey = 0;
        ez = 0;
    }

    public PlayerPathEvent(uint session_id, uint player_id, DateTime time, Vector3 pos, Vector3 orient) : base(session_id, player_id, time)
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

public class SessionPlayerEvent    // Player position and rotation for debug path
{
    public SessionPlayerEvent(uint session, uint player, DateTime start, DateTime end)
    {
        SessionID = session;
        PlayerID = player;
        this.start = start;
        this.end = end;
    }
    public uint SessionID, PlayerID;
    public DateTime start, end;
}