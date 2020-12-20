using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using System.Reflection;

enum Table
{
    PlayerPositionEvent = 0,
    PlayerDeathEvent,
    PlayerFallsEvent,
    PlayerLifeLostEvent,
    PlayerPathEvent,
    TimetoFinish,
    EnemyKills,
    SwitchesTimeEvent,
    FindKeyEvent,
    NullEvent
}

public class Writer : MonoBehaviour
{

    string[] paths = new string[9];


    void Start()
    {
        paths[(int)Table.PlayerPositionEvent] = Application.dataPath + "/CSV/" + "PlayerPositionEvent.csv";
        paths[(int)Table.PlayerDeathEvent] = Application.dataPath + "/CSV/" + "PlayerDeathEvent.csv";
        paths[(int)Table.PlayerFallsEvent] = Application.dataPath + "/CSV/" + "PlayerFallsEvent.csv";
        paths[(int)Table.PlayerLifeLostEvent] = Application.dataPath + "/CSV/" + "PlayerLifeLostEvent.csv";
        paths[(int)Table.PlayerPathEvent] = Application.dataPath + "/CSV/" + "PlayerPathEvent.csv";
        paths[(int)Table.TimetoFinish] = Application.dataPath + "/CSV/" + "TimetoFinish.csv";
        paths[(int)Table.EnemyKills] = Application.dataPath + "/CSV/" + "EnemyKills.csv";
        paths[(int)Table.SwitchesTimeEvent] = Application.dataPath + "/CSV/" + "SwitchesTimeEvent.csv";
        paths[(int)Table.FindKeyEvent] = Application.dataPath + "/CSV/" + "FindKeyEvent.csv";

        // Clear all the files
        //foreach (string path in paths)
        //    File.Delete(path);

        // Position Event
        if (!File.Exists(getPath(Table.PlayerPositionEvent)))
        {
            string[] RowHeadersPosition = { "PosX", "PosY", "PosZ", "PlayerID", "DateTime"};
            Save(RowHeadersPosition, Table.PlayerPositionEvent);
        }
        if (!File.Exists(getPath(Table.PlayerDeathEvent)))
        {
            string[] RowHeadersPosition = { "PosX", "PosY", "PosZ", "Enemy", "PlayerID", "DateTime" };
            Save(RowHeadersPosition, Table.PlayerDeathEvent);
        }
        if (!File.Exists(getPath(Table.PlayerFallsEvent)))
        {
            string[] RowHeadersPosition = { "PosX", "PosY", "PosZ", "Fall Type", "PlayerID", "DateTime" };
            Save(RowHeadersPosition, Table.PlayerFallsEvent);
        }
        if (!File.Exists(getPath(Table.PlayerLifeLostEvent)))
        {
            string[] RowHeadersPosition = { "PosX", "PosY", "PosZ", "Enemy", "PlayerID", "DateTime" };
            Save(RowHeadersPosition, Table.PlayerLifeLostEvent);
        }
        if (!File.Exists(getPath(Table.PlayerPathEvent)))
        {
            string[] RowHeadersPosition = { "PosX", "PosY", "PosZ", "Orientation X", "Orientation Y", "Orientation Z", "PlayerID", "DateTime" };
            Save(RowHeadersPosition, Table.PlayerPathEvent);
        }
        if (!File.Exists(getPath(Table.TimetoFinish)))
        {
            string[] RowHeadersPosition = { "Time", "PlayerID", "DateTime" };
            Save(RowHeadersPosition, Table.TimetoFinish);
        }
        if (!File.Exists(getPath(Table.EnemyKills)))
        {
            string[] RowHeadersPosition = { "EnemyPos X", "EnemyPos Y", "EnemyPos Z", "Enemy Type", "PlayerID", "DateTime" };
            Save(RowHeadersPosition, Table.EnemyKills);
        }
        if (!File.Exists(getPath(Table.SwitchesTimeEvent)))
        {
            string[] RowHeadersPosition = { "Switch Number", "Time", "PlayerID", "DateTime" };
            Save(RowHeadersPosition, Table.SwitchesTimeEvent);
        }
        if (!File.Exists(getPath(Table.FindKeyEvent)))
        {
            string[] RowHeadersPosition = { "Time", "PlayerID", "DateTime" };
            Save(RowHeadersPosition, Table.FindKeyEvent);
        }
    }


    void Save(string[] rowData, Table table)
    {

        string delimiter = ",";
        string filePath = getPath(table);

        File.AppendAllText(filePath, string.Join(delimiter, rowData) + ",\n");
    }

    private string getPath(Table table)
    {
        return paths[(int)table];
    }


    public void ReceiveEvent(object eventData)
    {
        // Decide to which table write



        // Properties serialization
        FieldInfo[] properties = eventData.GetType().GetFields();

        string[] rowDataTemp = new string[properties.Length];

        int i = 0;
        foreach (FieldInfo property in properties)
            rowDataTemp[i++] = property.GetValue(eventData).ToString().Replace(',', '.');

        Save(rowDataTemp, GetTable(eventData));
    }

    Table GetTable(object eventData)
    {
        //Table currentWriteTable = Table.NullEvent;
        if (eventData is PlayerPositionEvent)
            return Table.PlayerPositionEvent;
        else if (eventData is PlayerDeathEvent)
            return Table.PlayerDeathEvent;
        else if (eventData is PlayerFallsEvent)
            return Table.PlayerFallsEvent;
        else if (eventData is PlayerLifeLostEvent)
            return Table.PlayerLifeLostEvent;
        else if (eventData is PlayerPathEvent)
            return Table.PlayerPathEvent;
        else if (eventData is TimeToFinishEvent)
            return Table.TimetoFinish;
        else if (eventData is EnemyKillsEvent)
            return Table.EnemyKills;
        else if (eventData is SwitchesTimeEvent)
            return Table.SwitchesTimeEvent;
        else if (eventData is FindKeyEvent)
            return Table.FindKeyEvent;


        return Table.NullEvent;
    }
}