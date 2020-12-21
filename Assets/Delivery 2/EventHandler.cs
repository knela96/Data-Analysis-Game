using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public GameObject ellen;
    public GameObject camera;
    public static EventData eventdata;
    public static EventHandler eventhandler;

    public List<EventData> events;

    public float timeStart = 0;


    public uint PlayerID = 0;


    public void SendEventData(object eventData)
    {
        ellen.SendMessage("ReceiveEvent", eventData);
    }

    // Per no posar-ho tot a una mateix llista que englobi tots els events de tots els tipus
    // Podriem crear diferents classes que siguin de tipus de llistes i despres a dins de cada classe posar les variabels que necesitem per a cadascu
    // Per exemple: class PositionEvent {...}, despres crear una llista List<PositionEvent> o List<TimerEvent> 
    // i guardar els events de posicio del jugador, mort del jugador i altres coses relacionades amb la posicio o els events amb time 

    private void OnEnable()
    {
        Damageable.PlayerDeathEvent += AddPlayerDeathEvent;
        Damageable.EnemyDeathEvent += AddEnemyKilledEvent;
        Damageable.PlayerHurtEvent += AddPlayerLifeLostEvent;
        DeathVolume.PlayerFallsEvent += AddPlayerFallEvent;
        InteractOnTrigger.SwitchTimerEvent += AddSwitchTimeEvent;
        InteractOnTrigger.LevelCompleteEvent += AddLevelCompleteEvent;
        InteractOnTrigger.KeyTimerEvent += AddKeyTimerEvent;
        PlayerController.playerPathEvent += AddPlayerPathEvent;
    }

    private void OnDisable()
    {
        Damageable.PlayerDeathEvent -= AddPlayerDeathEvent;
        Damageable.EnemyDeathEvent -= AddEnemyKilledEvent;
        Damageable.PlayerHurtEvent -= AddPlayerLifeLostEvent;
        DeathVolume.PlayerFallsEvent -= AddPlayerFallEvent;
        InteractOnTrigger.SwitchTimerEvent -= AddSwitchTimeEvent;
        InteractOnTrigger.LevelCompleteEvent -= AddLevelCompleteEvent;
        InteractOnTrigger.KeyTimerEvent -= AddKeyTimerEvent;
        PlayerController.playerPathEvent -= AddPlayerPathEvent;
    }

    public void Awake()
    {
        ellen = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        PlayerID = (uint)Random.Range(0, 99999);
    }

    void Start()
    {
        CreateLists();

        if (eventhandler == null)
        {
            eventhandler = this;
        }

        InvokeRepeating("UpdateInfoEvent", 0.0f, 0.3f);
    }
    void Update()
    {
        if (events.Count == 0)
            return;
    }

    void UpdateInfoEvent()
    {
       AddPlayerPositionEvent();
    }

    void CreateLists()
    {
        events = new List<EventData>();
    }
    public void AddPlayerPathEvent()
    {//orientation to camera
        SendEventData(new PlayerPathEvent(PlayerID, System.DateTime.Now, ellen.transform.position, new Vector3(camera.transform.localEulerAngles.x, camera.transform.eulerAngles.y, camera.transform.localEulerAngles.z)));
    }
    public void AddPlayerPositionEvent()
    {
        SendEventData(new PlayerPositionEvent(PlayerID, System.DateTime.Now, ellen.transform.position));
    }

    public void AddPlayerDeathEvent(ENEMY_TYPE type)
    {
        SendEventData(new PlayerDeathEvent(PlayerID, System.DateTime.Now, ellen.transform.position,type));
    }
    public void AddEnemyKilledEvent(GameObject enemy, ENEMY_TYPE type)
    {
        SendEventData(new EnemyKillsEvent(PlayerID, System.DateTime.Now, enemy.transform.position, type));
    }

    public void AddKeyTimerEvent(float time)
    {
        SendEventData(new FindKeyEvent(PlayerID, System.DateTime.Now, time));
    }

    public void AddPlayerLifeLostEvent(ENEMY_TYPE type)
    {
        SendEventData(new PlayerLifeLostEvent(PlayerID, System.DateTime.Now, ellen.transform.position, type));
    }

    public void AddPlayerFallEvent(SURFACE_TYPE type)
    {
        SendEventData(new PlayerFallsEvent(PlayerID, System.DateTime.Now, ellen.transform.position, type));
    }

    public void AddSwitchTimeEvent(int switch_id, float time)
    {
        SendEventData(new SwitchesTimeEvent(PlayerID, System.DateTime.Now, switch_id, time));
    }

    public void AddLevelCompleteEvent(float time)
    {
        SendEventData(new TimeToFinishEvent(PlayerID, System.DateTime.Now, time));
    }

    public void AddFindKeyEvent(float time)
    {
        SendEventData(new FindKeyEvent(PlayerID, System.DateTime.Now, time));
    }

    public void StartTimer()
    {
        timeStart = Time.time;
    }

    public float GetStartTime()
    {
        return timeStart;
    }
}
