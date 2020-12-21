using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanCubesUI : MonoBehaviour
{
    public Heatmap heatmap;
    public Toggle toggle_heat;
    public Toggle toggle_arrows;

    public void DeleteCubesOnDisableToggle()
    {
        if (toggle_heat.GetComponent<Toggle>().isOn == false)
        {
            heatmap.clearMap();
        }
    }

    public void DeleteCubesOnDisableToggleArrows()
    {
        if (toggle_arrows.GetComponent<Toggle>().isOn == false)
        {
            heatmap.clearArrows();
        }
    }
}
