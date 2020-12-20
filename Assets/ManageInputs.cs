using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageInputs : MonoBehaviour
{
    private string input;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadInputValue(string i)
    {
        input = i;
        Debug.Log(input);
    }
}
