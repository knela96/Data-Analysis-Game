using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


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

    private Reader reader;

    public List<PlayerPathEvent> paths = new List<PlayerPathEvent>();

    public Gradient ColorGradient;

    private float heatMaxValue = 0;
    private float heatMinValue = 0;

    public GameObject arrow;
    public Color ArrowColor;
    public int maxArrows = 200;

    public Dropdown heatmap_selector;
    public Dropdown surface_selector;
    public Dropdown enemy_selector;

    public void Awake()
    {
        heatmap_selector.onValueChanged.AddListener(delegate { reloadHeatmap(); });
        surface_selector.onValueChanged.AddListener(delegate { reloadHeatmap(); });
        enemy_selector.onValueChanged.AddListener(delegate { reloadHeatmap(); });

        reader = gameObject.GetComponent<Reader>();

        GridSizeX = (int)(cubeSize * (float)GridSizeX);
        GridSizeY = (int)(cubeSize * (float)GridSizeY);

        gridArray = new int[GridSizeX, GridSizeY];

        heatMapCube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
    }
    void Start()
    {
    }

    public void clearMap()
    {
        foreach (GameObject obj in instancedCubes)
        {
            Destroy(obj);
        }
        instancedCubes.Clear();
    }

    public void reloadHeatmap()
    {
        gridArray = new int[GridSizeX, GridSizeY];

        CountEvents();

        clearMap();

        VisualizeGrid();
    }

    private void SetValue(float x, float y)
    {
        int newX = (int)(x / cubeSize - map_originX / cubeSize);
        int newY = (int)(y / cubeSize - map_originY / cubeSize);


        if (newX > 0 && newY > 0 && newX < GridSizeX && newY < GridSizeY)
        {
            gridArray[newX, newY]++;
        }
    }

    public void CountEvents()
    {
        float x = 0, y = 0;
        switch ((EventFilter)heatmap_selector.value)
        {
            case EventFilter.Position:
                for (int i = 0; i < reader.arrPosition.Count; i++)
                {
                    EventData eventData = reader.arrPosition[i];
                    x = ((PlayerPositionEvent)eventData).x;
                    y = ((PlayerPositionEvent)eventData).z;
                    SetValue(x, y);
                }
                break;
            case EventFilter.PlayerDeath:
                for (int i = 0; i < reader.arrDeath.Count; i++)
                {
                    EventData eventData = reader.arrDeath[i];
                    if (((PlayerDeathEvent)eventData).enemy == enemy_selector.value ||
                        enemy_selector.value == 0)
                    {
                        x = ((PlayerDeathEvent)eventData).x;
                        y = ((PlayerDeathEvent)eventData).z;
                        SetValue(x, y);
                    }
                }
                break;
            case EventFilter.LifeLost:
                for (int i = 0; i < reader.arrLifeLost.Count; i++)
                {
                    EventData eventData = reader.arrLifeLost[i];
                    if (((PlayerLifeLostEvent)eventData).enemy == enemy_selector.value ||
                        enemy_selector.value == 0)
                    {
                        x = ((PlayerLifeLostEvent)eventData).x;
                        y = ((PlayerLifeLostEvent)eventData).z;
                        SetValue(x, y);
                    }
                }
                break;
            case EventFilter.Fall:
                for (int i = 0; i < reader.arrFalls.Count; i++)
                {
                    EventData eventData = reader.arrFalls[i];
                    if (((PlayerFallsEvent)eventData).surface == surface_selector.value ||
                       surface_selector.value == 0)
                    {
                        x = ((PlayerFallsEvent)eventData).x;
                        y = ((PlayerFallsEvent)eventData).z;
                        SetValue(x, y);
                    }
                }
                break;
            case EventFilter.EnemyDeath:
                for (int i = 0; i < reader.arrEnemyKills.Count; i++)
                {
                    EventData eventData = reader.arrEnemyKills[i];
                    if (((EnemyKillsEvent)eventData).enemy == enemy_selector.value ||
                        enemy_selector.value == 0)
                    {
                        x = ((EnemyKillsEvent)eventData).x;
                        y = ((EnemyKillsEvent)eventData).z;
                        SetValue(x, y);
                    }
                }
                break;
        }

        // Change dynamically color
        int c = 0;
        heatMaxValue = 0;
        heatMinValue = 0;
        for (int i = 0; i < GridSizeX; i++)
        {
            for (int j = 0; j < GridSizeY; j++)
            {
                if (gridArray[i, j] > 0)
                {
                    if(gridArray[i, j] >= heatMaxValue)
                        heatMaxValue = gridArray[i, j];
                    else if(heatMinValue > gridArray[i, j] || heatMinValue == 0)
                        heatMinValue = gridArray[i, j];
                }
            }
        }
        heatMaxValue = heatMaxValue - heatMinValue;
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
        //int index = paths.Count - maxArrows;
        //index = Mathf.Max(index, 0);
        //for (int i = index; i < paths.Count; ++i)
        //{
        //    Vector3 pos = paths[i].position;
        //    if(i > 0)
        //    {
        //        Vector3 previous = paths[i - 1].position;
        //        //Vector3 midpoint = new Vector3(previous.x + pos.x / 2, previous.y + pos.y / 2, previous.z + pos.z / 2);
        //        //Gizmos.DrawLine(previous, pos);
        //        pos.y = pos.y + 0.15f; 
        //        GameObject go = Instantiate(arrow, pos, Quaternion.Euler(paths[i].orientation.x, paths[i].orientation.y, paths[i].orientation.z));
        //        go.GetComponent<Renderer>().material.SetColor("_Color", ArrowColor);
        //        instancedArrows.Add(go);
        //    }
        //}

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

    

    void Update()
    {
        //if (Vector3.Distance(player_transform.position,d_previous) >= 1)//Recalculate cubes
        //{
        //    d_previous = player_transform.position;
        //    PlayerPathEvent pl = new PlayerPathEvent(0, time, player_transform.position, new Vector3(player_transform.eulerAngles.x, player_transform.eulerAngles.y + 90, player_transform.eulerAngles.z));
        //    paths.Add(pl);

        //}

        //foreach (var obj in instancedArrows)
        //{
        //    Destroy(obj);
        //}
        //instancedArrows.Clear();

        //VisualizeArrows();

    }

}
