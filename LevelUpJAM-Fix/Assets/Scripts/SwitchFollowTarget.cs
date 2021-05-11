using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SwitchFollowTarget : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;

    public PlayerController red, blue;
    void Start()
    {
        red = GameObject.FindGameObjectWithTag("Red").GetComponent<PlayerController>();
        blue = GameObject.FindGameObjectWithTag("Blue").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
      if (red.isActive && vCam.m_Follow != red.transform)
        {
            vCam.m_Follow = red.transform;
        }
        else if (blue.isActive && vCam.m_Follow != blue.transform)
        {
            vCam.m_Follow = blue.transform;
        }
    }
}
