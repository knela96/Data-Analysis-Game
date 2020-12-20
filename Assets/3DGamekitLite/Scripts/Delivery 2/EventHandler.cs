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

    public List<PositionEvent> position_events;


    static EventHandler mInstance;
    private GameObject player;

    public static EventHandler Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject go = new GameObject("Event Handler");
                mInstance = go.AddComponent<EventHandler>();
            }
            return mInstance;
        }
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SendEventData(object eventData)
    {
        player.SendMessage("ReceiveEvent", eventData);
    }

    // Per no posar-ho tot a una mateix llista que englobi tots els events de tots els tipus
    // Podriem crear diferents classes que siguin de tipus de llistes i despres a dins de cada classe posar les variabels que necesitem per a cadascu
    // Per exemple: class PositionEvent {...}, despres crear una llista List<PositionEvent> o List<TimerEvent> 
    // i guardar els events de posicio del jugador, mort del jugador i altres coses relacionades amb la posicio o els events amb time 
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
        // Calling this event every X seconds (in our case 3 seconds) to save the current position and create a path
       Instance.SendEventData(AddPlayerPositionEvent());
    }

    void CreateLists()
    {
        events = new List<EventData>();
        //position_events = new List<PositionEvent>();
    }

    public object AddPlayerPositionEvent()
    {
        object e = new PlayerPositionEvent((uint)events.Count, System.DateTime.Now, ellen.transform.position);
        return e;
        //position_events.Add(e);
    }
    public void AddPlayerDeathEvent()
    {
        PlayerDeathEvent e = new PlayerDeathEvent((uint)events.Count, System.DateTime.Now, ellen.transform.position);
        events.Add(e);
        //position_events.Add(e);
    }
    public void AddEnemyKilledEvent(GameObject enemy)
    {
        EnemyKillsEvent e = new EnemyKillsEvent((uint)events.Count, System.DateTime.Now, enemy.transform.position, ENEMY_TYPE.ALL);
        events.Add(e);
    }

    public void AddKeyTimerEvent(float time)
    {
        // Don't know how to save the timer
        FindKeyEvent e = new FindKeyEvent((uint)events.Count, System.DateTime.Now, time);
        events.Add(e);
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
