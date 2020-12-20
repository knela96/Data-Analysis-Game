using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public GameObject ellen;
    public static EventData eventdata;
    public static EventHandler eventhandler;

    public List<EventData> events;
    public List<PlayerPositionEvent> position_events;
    void Start()
    {
        CreateLists();

        if (eventhandler == null)
        {
            eventhandler = this;
        }

        InvokeRepeating("UpdateInfoEvent", 3.0f, 3.0f); 
    }
    void Update()
    {
        if (events.Count == 0)
            return;
    }

    void UpdateInfoEvent()
    {
        // Calling this event every X seconds (in our case 3 seconds)
        //AddEvent(EventFilter.Position);
        AddPlayerPositionEvent();
    }

    void CreateLists()
    {
        events = new List<EventData>();
        position_events = new List<PlayerPositionEvent>();
    }

    public void AddPlayerPositionEvent()
    {
        PlayerPositionEvent e = new PlayerPositionEvent((uint)events.Count, System.DateTime.Now, ellen.transform.position);
        events.Add(e);
        position_events.Add(e);
    }

    public void AddPlayerDeathEvent()
    {
        PlayerDeathEvent e = new PlayerDeathEvent((uint)events.Count, System.DateTime.Now, ellen.transform.position);
        events.Add(e);
        //position_events.Add(e);
    }

    //public void AddEvent(EventFilter filter)
    //{
    //    if (filter == EventFilter.Position)
    //    {
    //        PlayerPositionEvent player_pos_ev = new PlayerPositionEvent((uint)events.Count, System.DateTime.Now, ellen.transform.position);
    //        events.Add(player_pos_ev);
    //        position_events.Add(player_pos_ev);
    //    }
    //    else if (filter == EventFilter.PlayerDeath)
    //    {
    //        //PlayerDeathEvent player_death_ev = new PlayerDeathEvent((uint)events.Count, System.DateTime.Now, ?? );
    //    }
    //    else if (filter == EventFilter.Objects)
    //    {

    //    }
    //    else if (filter == EventFilter.None)
    //    {
    //        // Used for timer events

    //    }
    //    else if (filter == EventFilter.LifeLost)
    //    {

    //    }
    //    else if (filter == EventFilter.Fall)
    //    {

    //    }
    //    else if (filter == EventFilter.EnemyDeath)
    //    {

    //    }
    //}

}
