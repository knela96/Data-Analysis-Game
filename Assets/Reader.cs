using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using System.Reflection;



public class Reader : MonoBehaviour
{
    
    //string[] paths = new string[11];


    //void Start()
    //{
    //    paths[(int)Table.PlayerPositionEvent - 1] = Application.dataPath + "/CSV/" + "PlayerPositionEvent.csv";
    //    paths[(int)Table.PlayerDeathEvent - 1] = Application.dataPath + "/CSV/" + "PlayerDeathEvent.csv";
    //    paths[(int)Table.PlayerFallsEvent - 1] = Application.dataPath + "/CSV/" + "PlayerFallsEvent.csv";
    //    paths[(int)Table.PlayerLifeLostEvent - 1] = Application.dataPath + "/CSV/" + "PlayerLifeLostEvent.csv";
    //    paths[(int)Table.ObjectsDestroyed - 1] = Application.dataPath + "/CSV/" + "ObjectsDestroyed.csv";
    //    paths[(int)Table.PlayerPathEvent - 1] = Application.dataPath + "/CSV/" + "PlayerPathEvent.csv";
    //    paths[(int)Table.TimetoFinish - 1] = Application.dataPath + "/CSV/" + "TimetoFinish.csv";
    //    paths[(int)Table.EnemyKills - 1] = Application.dataPath + "/CSV/" + "EnemyKills.csv";
    //    paths[(int)Table.SwitchesTimeEvent - 1] = Application.dataPath + "/CSV/" + "SwitchesTimeEvent.csv";
    //    paths[(int)Table.TimePuzzleEvent - 1] = Application.dataPath + "/CSV/" + "TimePuzzleEvent.csv";
    //    paths[(int)Table.FindKeyEvent - 1] = Application.dataPath + "/CSV/" + "FindKeyEvent.csv";

    //    // Clear all the files
    //    //foreach (string path in paths)
    //    //    File.Delete(path);

    //    // Position Event
    //    if (!File.Exists(getPath(Table.PlayerPositionEvent)))
    //    {
    //        string[] RowHeadersPosition = { "PlayerID", "PosX", "PosY", "PosZ"};
    //        Save(RowHeadersPosition, Table.PlayerPositionEvent);
    //    }
    //    if (!File.Exists(getPath(Table.PlayerDeathEvent)))
    //    {
    //        string[] RowHeadersPosition = { "PlayerID", "PosX", "PosY", "PosZ", "Enemy" };
    //        Save(RowHeadersPosition, Table.PlayerDeathEvent);
    //    }
    //    if (!File.Exists(getPath(Table.PlayerFallsEvent)))
    //    {
    //        string[] RowHeadersPosition = { "PlayerID", "PosX", "PosY", "PosZ", "Fall Type" };
    //        Save(RowHeadersPosition, Table.PlayerFallsEvent);
    //    }
    //    if (!File.Exists(getPath(Table.PlayerLifeLostEvent)))
    //    {
    //        string[] RowHeadersPosition = { "PlayerID", "PosX", "PosY", "PosZ", "Enemy", "Life Lost" };
    //        Save(RowHeadersPosition, Table.PlayerLifeLostEvent);
    //    }
    //    if (!File.Exists(getPath(Table.ObjectsDestroyed)))
    //    {
    //        string[] RowHeadersPosition = { "PlayerID", "PosX", "PosY", "PosZ", "Time passed" };
    //        Save(RowHeadersPosition, Table.ObjectsDestroyed);
    //    }
    //    if (!File.Exists(getPath(Table.PlayerPathEvent)))
    //    {
    //        string[] RowHeadersPosition = { "PlayerID", "PosX", "PosY", "PosZ", "Orientation X", "Orientation Y", "Orientation Z" };
    //        Save(RowHeadersPosition, Table.PlayerPathEvent);
    //    }
    //    if (!File.Exists(getPath(Table.TimetoFinish)))
    //    {
    //        string[] RowHeadersPosition = { "PlayerID", "Time" };
    //        Save(RowHeadersPosition, Table.TimetoFinish);
    //    }
    //    if (!File.Exists(getPath(Table.EnemyKills)))
    //    {
    //        string[] RowHeadersPosition = { "PlayerID", "EnemyPos X", "EnemyPos Y", "EnemyPos Z", "Enemy Type", "Player Pos X", "Player Pos Y", "Player Pos Z"};
    //        Save(RowHeadersPosition, Table.EnemyKills);
    //    }
    //    if (!File.Exists(getPath(Table.SwitchesTimeEvent)))
    //    {
    //        string[] RowHeadersPosition = { "PlayerID", "Switch Number", "Time"};
    //        Save(RowHeadersPosition, Table.SwitchesTimeEvent);
    //    }
    //    if (!File.Exists(getPath(Table.TimePuzzleEvent)))
    //    {
    //        string[] RowHeadersPosition = { "PlayerID", "Puzzle ID", "Time" };
    //        Save(RowHeadersPosition, Table.TimePuzzleEvent);
    //    }
    //    if (!File.Exists(getPath(Table.FindKeyEvent)))
    //    {
    //        string[] RowHeadersPosition = { "PlayerID",  "Time" };
    //        Save(RowHeadersPosition, Table.FindKeyEvent);
    //    }
    //}


    //void Save(string[] rowData, Table table)
    //{

    //    string delimiter = ",";
    //    string filePath = getPath(table);

    //    File.AppendAllText(filePath, string.Join(delimiter, rowData) + ",\n");
    //}

    //private string getPath(Table table)
    //{
    //    return paths[(int)table - 1];
    //}


    //void ReceiveEvent(object eventData)
    //{
    //    // Decide to which table write



    //    // Properties serialization
    //    FieldInfo[] properties = eventData.GetType().GetFields();

    //    string[] rowDataTemp = new string[properties.Length];

    //    int i = 0;
    //    foreach (FieldInfo property in properties)
    //        rowDataTemp[i++] = property.GetValue(eventData).ToString().Replace(',', '.');

    //    Save(rowDataTemp, GetTable(eventData));
    //}

    //Table GetTable(object eventData)
    //{
    //    ////Table currentWriteTable = Table.NullEvent;
    //    //if (eventData is PositionEventData)
    //    //    return Table.PositionEvent;
    //    //else if (eventData is SessionEventData)
    //    //    return Table.SessionEvent;
    //    //else if (eventData is HitEvent)
    //    //    return Table.HitEvent;
    //    //else if (eventData is RoundEndEvent)
    //    //    return Table.RoundEndEvent;
    //    //else if (eventData is ErrorEvent)
    //    //    return Table.ErrorEvent;
        
    //    return Table.NullEvent;
    //}
}