using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageDropdowns : MonoBehaviour
{
    public GameObject enemy;
    public GameObject surface;
    
    public void Manage(int val)
    {
        if (val == 0)
        {
            surface.SetActive(false);
            enemy.SetActive(false);
        }
        if (val == 1)
        {
            enemy.SetActive(true);
            surface.SetActive(false);
        }
        if (val == 2)
        {
            enemy.SetActive(true);
            surface.SetActive(false);
        }
    }
}
