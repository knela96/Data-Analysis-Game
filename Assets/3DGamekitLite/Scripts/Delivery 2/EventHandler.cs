using Gamekit3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("UpdateInfoEvent", 3.0f, 3.0f); // InvokeRepeating(function called, starting time, repeat rate)
    }
    void Update()
    {

    }

    void UpdateInfoEvent()
    {
        // code that will be executing every X seconds
    }

    public void AddNewDamageEvent(Damageable player)
    {

    }

}
