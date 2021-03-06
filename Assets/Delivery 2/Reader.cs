﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Reader : MonoBehaviour
{
    public List<PlayerPositionEvent> arrPosition;
    public List<PlayerDeathEvent> arrDeath;
    public List<PlayerFallsEvent> arrFalls;
    public List<PlayerLifeLostEvent> arrLifeLost;
    public List<PlayerPathEvent> arrPath;
    public List<EnemyKillsEvent> arrEnemyKills;


    public bool isFilled = false;

    string[] lineData;

    private void Awake()
    {        
        arrPosition = new List<PlayerPositionEvent>();
        arrDeath = new List<PlayerDeathEvent>();
        arrFalls = new List<PlayerFallsEvent>();
        arrLifeLost = new List<PlayerLifeLostEvent>();
        arrPath = new List<PlayerPathEvent>();
        arrEnemyKills = new List<EnemyKillsEvent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ReadEvents();
        isFilled = true;
    }

    public void ReadEvents()
    {
        ReadPosition();
        ReadDeath();
        ReadFalls();
        ReadKills();
        ReadLifeLost();
        Readpath();
    }
    void ReadPosition()
    {
        string positionPath = Application.dataPath + "/CSV/" + "PlayerPositionEvent.csv";

        string fileData = System.IO.File.ReadAllText(positionPath.ToString());
        string[] lines = fileData.Split("\n"[0]);

        for (int i = 1; i < lines.Length - 1; i++) //i = 1 instead of 0 because we want to skip the headers
        {
            lineData = (lines[i].Trim()).Split(","[0]);
            PlayerPositionEvent PositionDataReturn = new PlayerPositionEvent();
            PositionDataReturn.x = float.Parse(lineData[0], CultureInfo.InvariantCulture);
            PositionDataReturn.y = float.Parse(lineData[1], CultureInfo.InvariantCulture);
            PositionDataReturn.z = float.Parse(lineData[2], CultureInfo.InvariantCulture);

            arrPosition.Add(PositionDataReturn);      
        }
    }

    void ReadDeath()
    {
        string positionPath = Application.dataPath + "/CSV/" + "PlayerDeathEvent.csv";

        string fileData = System.IO.File.ReadAllText(positionPath.ToString());
        string[] lines = fileData.Split("\n"[0]);


        for (int i = 1; i < lines.Length - 1; i++) //i = 1 instead of 0 because we want to skip the headers
        {
            lineData = (lines[i].Trim()).Split(","[0]);
            PlayerDeathEvent DeathDataReturn = new PlayerDeathEvent();
            DeathDataReturn.x = float.Parse(lineData[0], CultureInfo.InvariantCulture);
            DeathDataReturn.y = float.Parse(lineData[1], CultureInfo.InvariantCulture);
            DeathDataReturn.z = float.Parse(lineData[2], CultureInfo.InvariantCulture);
            DeathDataReturn.enemy = int.Parse(lineData[3]);

            arrDeath.Add(DeathDataReturn);
        }
    }

    void ReadFalls()
    {
        string positionPath = Application.dataPath + "/CSV/" + "PlayerFallsEvent.csv";

        string fileData = System.IO.File.ReadAllText(positionPath.ToString());
        string[] lines = fileData.Split("\n"[0]);


        for (int i = 1; i < lines.Length - 1; i++) //i = 1 instead of 0 because we want to skip the headers
        {
            lineData = (lines[i].Trim()).Split(","[0]);
            PlayerFallsEvent FallsDataReturn = new PlayerFallsEvent();
            FallsDataReturn.x = float.Parse(lineData[0], CultureInfo.InvariantCulture);
            FallsDataReturn.y = float.Parse(lineData[1], CultureInfo.InvariantCulture);
            FallsDataReturn.z = float.Parse(lineData[2], CultureInfo.InvariantCulture);
            FallsDataReturn.surface = int.Parse(lineData[3]);

            arrFalls.Add(FallsDataReturn);
        }
    }

    void ReadKills()
    {
        string positionPath = Application.dataPath + "/CSV/" + "EnemyKills.csv";

        string fileData = System.IO.File.ReadAllText(positionPath.ToString());
        string[] lines = fileData.Split("\n"[0]);


        for (int i = 1; i < lines.Length - 1; i++) //i = 1 instead of 0 because we want to skip the headers
        {
            lineData = (lines[i].Trim()).Split(","[0]);
            EnemyKillsEvent EnemyKillsDataReturn = new EnemyKillsEvent();
            EnemyKillsDataReturn.x = float.Parse(lineData[0], CultureInfo.InvariantCulture);
            EnemyKillsDataReturn.y = float.Parse(lineData[1], CultureInfo.InvariantCulture);
            EnemyKillsDataReturn.z = float.Parse(lineData[2], CultureInfo.InvariantCulture);
            EnemyKillsDataReturn.enemy = int.Parse(lineData[3]);

            arrEnemyKills.Add(EnemyKillsDataReturn);
        }
    }

    void ReadLifeLost()
    {
        string positionPath = Application.dataPath + "/CSV/" + "PlayerLifeLostEvent.csv";

        string fileData = System.IO.File.ReadAllText(positionPath.ToString());
        string[] lines = fileData.Split("\n"[0]);


        for (int i = 1; i < lines.Length - 1; i++) //i = 1 instead of 0 because we want to skip the headers
        {
            lineData = (lines[i].Trim()).Split(","[0]);
            PlayerLifeLostEvent LifeLostDataReturn = new PlayerLifeLostEvent();
            LifeLostDataReturn.x = float.Parse(lineData[0], CultureInfo.InvariantCulture);
            LifeLostDataReturn.y = float.Parse(lineData[1], CultureInfo.InvariantCulture);
            LifeLostDataReturn.z = float.Parse(lineData[2], CultureInfo.InvariantCulture);
            LifeLostDataReturn.enemy = int.Parse(lineData[3]);

            arrLifeLost.Add(LifeLostDataReturn);
        }
    }

    void Readpath()
    {
        string positionPath = Application.dataPath + "/CSV/" + "PlayerPathEvent.csv";

        string fileData = System.IO.File.ReadAllText(positionPath.ToString());
        string[] lines = fileData.Split("\n"[0]);


        for (int i = 1; i < lines.Length - 1; i++) //i = 1 instead of 0 because we want to skip the headers
        {
            lineData = (lines[i].Trim()).Split(","[0]);
            PlayerPathEvent PathDataReturn = new PlayerPathEvent();
            PathDataReturn.x = float.Parse(lineData[0], CultureInfo.InvariantCulture);
            PathDataReturn.y = float.Parse(lineData[1], CultureInfo.InvariantCulture);
            PathDataReturn.z = float.Parse(lineData[2], CultureInfo.InvariantCulture);
            PathDataReturn.ex = float.Parse(lineData[3], CultureInfo.InvariantCulture);
            PathDataReturn.ey = float.Parse(lineData[4], CultureInfo.InvariantCulture);
            PathDataReturn.ez = float.Parse(lineData[5], CultureInfo.InvariantCulture);

            arrPath.Add(PathDataReturn);
        }
    }
}