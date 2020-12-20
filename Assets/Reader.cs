using System.Collections;
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
    public List<TimeToFinishEvent> arrFinish;
    public List<EnemyKillsEvent> arrEnemyKills;
    public List<SwitchesTimeEvent> arrSwitchesTime;
    public List<FindKeyEvent> arrFindKey;


    public bool isFilled = false;

    //We are using a static object to control the memory we use
    static string[] lineData;

    static PlayerPositionEvent PositionDataReturn;
    static PlayerDeathEvent DeathDataReturn;
    static PlayerFallsEvent FallsDataReturn;
    static PlayerLifeLostEvent LifeLostDataReturn;
    static PlayerPathEvent PathDataReturn;
    static TimeToFinishEvent FinishDataReturn;
    static EnemyKillsEvent EnemyKillsDataReturn;
    static SwitchesTimeEvent SwitchesTimeDataReturn;
    static FindKeyEvent FindKeyDataReturn;

    private void Awake()
    {
        PositionDataReturn = new PlayerPositionEvent();

        
        arrPosition = new List<PlayerPositionEvent>();
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
        //ReadDeath();
        //ReadFalls();
        //ReadKills();
        //ReadLifeLost();
        //ReadSwitches();
        //Readkey();
        //ReadFinish();
        //Readpath();
    }
    void ReadPosition()
    {
        string positionPath = Application.dataPath + "/CSV/" + "PlayerPositionEvent.csv";

        string fileData = System.IO.File.ReadAllText(positionPath.ToString());
        string[] lines = fileData.Split("\n"[0]);


        for (int i = 1; i < lines.Length - 1; i++) //i = 1 instead of 0 because we want to skip the headers
        {
            lineData = (lines[i].Trim()).Split(","[0]);
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
            FallsDataReturn.x = float.Parse(lineData[0], CultureInfo.InvariantCulture);
            FallsDataReturn.y = float.Parse(lineData[1], CultureInfo.InvariantCulture);
            FallsDataReturn.z = float.Parse(lineData[2], CultureInfo.InvariantCulture);
            FallsDataReturn.surface = int.Parse(lineData[4]);

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
            EnemyKillsDataReturn.x = float.Parse(lineData[0], CultureInfo.InvariantCulture);
            EnemyKillsDataReturn.y = float.Parse(lineData[1], CultureInfo.InvariantCulture);
            EnemyKillsDataReturn.z = float.Parse(lineData[2], CultureInfo.InvariantCulture);
            EnemyKillsDataReturn.enemy = int.Parse(lineData[4]);

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
            LifeLostDataReturn.x = float.Parse(lineData[0], CultureInfo.InvariantCulture);
            LifeLostDataReturn.y = float.Parse(lineData[1], CultureInfo.InvariantCulture);
            LifeLostDataReturn.z = float.Parse(lineData[2], CultureInfo.InvariantCulture);
            LifeLostDataReturn.enemy = int.Parse(lineData[4]);

            arrLifeLost.Add(LifeLostDataReturn);
        }
    }

    void ReadSwitches()
    {
        string positionPath = Application.dataPath + "/CSV/" + "SwitchesTimeEvent.csv";

        string fileData = System.IO.File.ReadAllText(positionPath.ToString());
        string[] lines = fileData.Split("\n"[0]);


        for (int i = 1; i < lines.Length - 1; i++) //i = 1 instead of 0 because we want to skip the headers
        {
            lineData = (lines[i].Trim()).Split(","[0]);
            SwitchesTimeDataReturn.current_switch_id = int.Parse(lineData[1]);
            SwitchesTimeDataReturn.global_time = int.Parse(lineData[2]);

            arrSwitchesTime.Add(SwitchesTimeDataReturn);
        }
    }

    void Readkey()
    {
        string positionPath = Application.dataPath + "/CSV/" + "FindKeyEvent.csv";

        string fileData = System.IO.File.ReadAllText(positionPath.ToString());
        string[] lines = fileData.Split("\n"[0]);


        for (int i = 1; i < lines.Length - 1; i++) //i = 1 instead of 0 because we want to skip the headers
        {
            lineData = (lines[i].Trim()).Split(","[0]);
            FindKeyDataReturn.global_time = float.Parse(lineData[1]);

            arrFindKey.Add(FindKeyDataReturn);
        }
    }

    void ReadFinish()
    {
        string positionPath = Application.dataPath + "/CSV/" + "TimetoFinish.csv";

        string fileData = System.IO.File.ReadAllText(positionPath.ToString());
        string[] lines = fileData.Split("\n"[0]);


        for (int i = 1; i < lines.Length - 1; i++) //i = 1 instead of 0 because we want to skip the headers
        {
            lineData = (lines[i].Trim()).Split(","[0]);
            FinishDataReturn.global_time = float.Parse(lineData[1]);

            arrFinish.Add(FinishDataReturn);
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