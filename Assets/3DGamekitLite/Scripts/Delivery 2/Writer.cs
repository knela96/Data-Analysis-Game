using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Writer : MonoBehaviour
{
    // IMPORTANT: This class is not related with serialization!
    // Its main purpose is to print out

    public static Writer Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public static void Print(string s)
    {
        Instance.WriterPrint(s);
    }
    
    private void WriterPrint(string s)
    {
        // write csv
        // write sql
    }


}
