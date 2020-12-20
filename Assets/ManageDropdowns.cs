using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageDropdowns : MonoBehaviour
{
    public GameObject toggle;
    public GameObject enemy;
    public GameObject surface;

    public void Start()
    {
        toggle.GetComponent<Toggle>().isOn = false;
        enemy.SetActive(false);
        surface.SetActive(false);
    }

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
            enemy.SetActive(false);
            surface.SetActive(true);
        }
        if (val == 3)
        {
            enemy.SetActive(true);
            surface.SetActive(false);
        }
        if (val == 4)
        {
            enemy.SetActive(true);
            surface.SetActive(false);
        }
    }
}
