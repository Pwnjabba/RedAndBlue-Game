using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameVentGroup : MonoBehaviour
{
    public FlameventController[] flameVents;
    void Start()
    {
        flameVents = GetComponentsInChildren<FlameventController>();
    }

    void Update()
    {
        
    }

    public void DisableVents()
    {
        foreach (var vent in flameVents)
        {
            vent.DisableVent();
        }
    }
}
