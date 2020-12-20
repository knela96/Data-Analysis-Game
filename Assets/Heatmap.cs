using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Heatmap : MonoBehaviour
{
    int GridSizeX = 200, GridSizeY = 200;
    float map_originX = -34, map_originY = -40;
    public float cubeSize = 1;
    int[,] gridArray;
    public GameObject heatMapCube;
    public float transparency = 0.5f;
    Vector3 d_previous;

    //--------------------------
    [HideInInspector]
    public List<GameObject> instancedCubes;
    [HideInInspector]
    public List<GameObject> instancedArrows;
    public List<EventData> EventsList = new List<EventData>();

    public List<PlayerPathEvent> paths = new List<PlayerPathEvent>();

    public EventFilter current_filter;
    public ENEMY_TYPE enemy_filter;
    public SURFACE_TYPE surface_filter;

    public Gradient ColorGradient;

    public int heatMaxValue = 50;

    public GameObject arrow;
    public Color ArrowColor;
    public int maxArrows = 200;

    public void CountEvents()
    {
        /*
        gridArray = new int[GridSizeX, GridSizeY];//Remove for later

        for (int i = 0; i < EventsList.Count; i++)
        {
            EventData eventData = EventsList[i];
            if (eventData.type == current_filter)
            {
                float x = 0, y = 0;
                switch (eventData.type)
                {
                    case (EventFilter.Position):
                        x = ((PlayerPositionEvent)eventData).position.x;
                        y = ((PlayerPositionEvent)eventData).position.z;
                        break;
                    case (EventFilter.PlayerDeath):
                        x = ((PlayerDeathEvent)eventData).position.x;
                        y = ((PlayerDeathEvent)eventData).position.z;
                        break;
                    case (EventFilter.Fall):
                        if (((PlayerFallsEvent)eventData).surface_type == surface_filter || ((PlayerFallsEvent)eventData).surface_type == SURFACE_TYPE.ALL)
                        {
                            x = ((PlayerFallsEvent)eventData).position.x;
                            y = ((PlayerFallsEvent)eventData).position.z;
                        }
                        break;
                    case (EventFilter.LifeLost):
                        if (((PlayerLifeLostEvent)eventData).type == enemy_filter || ((PlayerLifeLostEvent)eventData).type == ENEMY_TYPE.ALL)
                        {
                            x = ((PlayerLifeLostEvent)eventData).position.x;
                            y = ((PlayerLifeLostEvent)eventData).position.z;
                        }
                        break;
                    case (EventFilter.EnemyDeath):
                        if(((EnemyKillsEvent)eventData).type == enemy_filter || ((EnemyKillsEvent)eventData).type == ENEMY_TYPE.ALL)
                        {
                            x = ((EnemyKillsEvent)eventData).position.x;
                            y = ((EnemyKillsEvent)eventData).position.z;
                        }
                        break;
                };

                int newX = (int)(x / cubeSize - map_originX / cubeSize);
                int newY = (int)(y / cubeSize - map_originY / cubeSize);


                if (newX > 0 && newY > 0 && newX < GridSizeX && newY < GridSizeY)
                {
                    gridArray[newX, newY]++;
                }
            }
        }
        */
        /* Change dynamically but affects all data
        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                if(maxCounts < gridArray[x, y])
                {
                    maxCounts = gridArray[x, y];
                }
            }
        }*/
    }

    void VisualizeGrid()
    {
        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                if(gridArray[x, y] > 0)
                    SpawnCube(x, y, gridArray[x, y]);
            }
        }
    }

    void VisualizeArrows()
    {
        int index = paths.Count - maxArrows;
        index = Mathf.Max(index, 0);
        for (int i = index; i < paths.Count; ++i)
        {
            Vector3 pos = paths[i].position;
            if(i > 0)
            {
                Vector3 previous = paths[i - 1].position;
                //Vector3 midpoint = new Vector3(previous.x + pos.x / 2, previous.y + pos.y / 2, previous.z + pos.z / 2);
                //Gizmos.DrawLine(previous, pos);
                pos.y = pos.y + 0.15f; 
                GameObject go = Instantiate(arrow, pos, Quaternion.Euler(paths[i].orientation.x, paths[i].orientation.y, paths[i].orientation.z));
                go.GetComponent<Renderer>().material.SetColor("_Color", ArrowColor);
                instancedArrows.Add(go);
            }
        }

    }

    void SpawnCube(int x, int y, int count)
    {
        float nwx = x * cubeSize + map_originX + cubeSize /2;
        float nwy = y * cubeSize + map_originY + cubeSize /2;
        Vector3 pos = new Vector3(nwx, GetHeight(nwx, nwy), nwy);
        GameObject cube = Instantiate(heatMapCube, pos, Quaternion.identity);
        instancedCubes.Add(cube);
        float f = Mathf.Clamp01((float)count / heatMaxValue);
        Color c = ColorGradient.Evaluate(f);
        c.a = transparency;
        cube.GetComponent<Renderer>().material.SetColor("_Color", c);
    }

    private float GetHeight(float x, float y)
    {
        Vector3 pos = new Vector3(x, 100, y);
        RaycastHit hit;
        int layerMask = 1 << 16;
        if (Physics.Raycast(pos,Vector3.down, out hit, Mathf.Infinity, layerMask))
        {
            return hit.point.y + cubeSize / 2;
        }
        return 0;
    }

    void Start()
    {
        GridSizeX = (int)(cubeSize * (float)GridSizeX);
        GridSizeY = (int)(cubeSize * (float)GridSizeY);

        gridArray = new int[GridSizeX, GridSizeY];

        heatMapCube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        //----------------------------
        
    }

    void Update()
    {
        DateTime time = new DateTime();
        Transform player_transform = GameObject.Find("Ellen").transform;

        //LOAD NEW DATA
        if (Input.GetMouseButtonDown(1))//Recalculate cubes
        {
            PlayerPositionEvent pos1 = new PlayerPositionEvent(0,time, player_transform.position);
            EventsList.Add(pos1);

            CountEvents();
        }

        foreach (var obj in instancedCubes)
        {
            Destroy(obj);
        }
        instancedCubes.Clear();

        VisualizeGrid();

        if (Vector3.Distance(player_transform.position,d_previous) >= 1)//Recalculate cubes
        {
            d_previous = player_transform.position;
            PlayerPathEvent pl = new PlayerPathEvent(0, time, player_transform.position, new Vector3(player_transform.eulerAngles.x, player_transform.eulerAngles.y + 90, player_transform.eulerAngles.z));
            paths.Add(pl);

        }

        foreach (var obj in instancedArrows)
        {
            Destroy(obj);
        }
        instancedArrows.Clear();

        VisualizeArrows();

    }

}
