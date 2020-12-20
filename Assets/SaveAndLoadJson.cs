using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


//data structure for player

[Serializable]
public class FileData
{



}


public class SaveAndLoadJson : MonoBehaviour
{

    [SerializeField]
    private string jsonpath;

    private FileData dataToSave;
    private FileData dataToLoad;

    public void Save()
    {
        using (StreamWriter stream = new StreamWriter(jsonpath))
        {
            string json = JsonUtility.ToJson(dataToSave);
            stream.Write(json);
        }

        Debug.Log("Data Saved");
    }

    public void Load()
    {
        using (StreamReader stream = new StreamReader(jsonpath))
        {
            string json = stream.ReadToEnd();
            dataToLoad = JsonUtility.FromJson<FileData>(json);
        }

        Debug.Log("Data Loaded");
    }


}
