using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Heatmap : MonoBehaviour
{
    public int GridSizeX = 200, GridSizeY = 200;
    public float map_originX = -34, map_originY = -40;
    public float cubeSize = 1;
    int[,] gridArray;
    public GameObject heatMapCube;
    public float transparency = 0.5f;

    //--------------------------

    public List<GameObject> instancedCubes;
    public List<EventData> EventsList = new List<EventData>();

    public EventFilter current_filter;

    public Gradient ColorGradient;

    public int maxCounts = 50;

    public void CountEvents()
    {
        
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
                    case (EventFilter.Death):
                        x = ((PlayerDeathEvent)eventData).position.x;
                        y = ((PlayerDeathEvent)eventData).position.z;
                        break;
                    case (EventFilter.LifeLost):
                        x = ((PlayerLifeLostEvent)eventData).position.x;
                        y = ((PlayerLifeLostEvent)eventData).position.z;
                        break;
                    case (EventFilter.Fall):
                        x = ((PlayerFallsEvent)eventData).position.x;
                        y = ((PlayerFallsEvent)eventData).position.z;
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

    void SpawnCube(int x, int y, int count)
    {
        x = (int)(x * cubeSize + map_originX + cubeSize / 2);
        y = (int)(y * cubeSize + map_originY + cubeSize / 2);
        Vector3 pos = new Vector3(x, GetHeight(x, y), y);
        GameObject cube = Instantiate(heatMapCube, pos, Quaternion.identity);
        instancedCubes.Add(cube);
        float f = Mathf.Clamp01((float)count / maxCounts);
        Color c = ColorGradient.Evaluate(f);
        c.a = transparency;
        cube.GetComponent<Renderer>().material.SetColor("_Color", c);
    }

    private int GetHeight(int x, int y)
    {
        Vector3 pos = new Vector3(x, 10, y);
        RaycastHit hit;
        int layerMask = 1 << 16;
        if (Physics.Raycast(pos,Vector3.down, out hit, Mathf.Infinity, layerMask))
        {
            return (int)(hit.point.y + cubeSize);
        }
        return 0;
    }

    void Start()
    {
        GridSizeX = (int)(cubeSize * (float)GridSizeX);
        GridSizeY = (int)(cubeSize * (float)GridSizeY);

        gridArray = new int[GridSizeX, GridSizeY];

        heatMapCube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CountEvents();
            PlayerPositionEvent pos1 = new PlayerPositionEvent();
            pos1.position = GameObject.Find("Ellen").transform.position;
            EventsList.Add(pos1);

            foreach (var obj in instancedCubes)
            {
                DestroyImmediate(obj);
            }
            instancedCubes.Clear();

            VisualizeGrid();
        }
    }

}
