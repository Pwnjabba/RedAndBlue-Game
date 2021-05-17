using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameVentGroup : MonoBehaviour
{
    public FlameventController[] flameVents;

    AudioSource flameAudio;
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
        if (GetComponentInChildren<FlameAudio>())
        {
            GetComponentInChildren<FlameAudio>().gameObject.SetActive(false);
        }
    }
}
