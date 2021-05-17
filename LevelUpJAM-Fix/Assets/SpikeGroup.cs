using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeGroup : MonoBehaviour
{
    public Trap[] traps;

    AudioSource flameAudio;
    void Start()
    {
        traps = GetComponentsInChildren<Trap>();

    }

    void Update()
    {

    }

    public void DisableTraps()
    {
        foreach (var trap in traps)
        {
            trap.Disable();
        }
    }
}
