using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    PlayerController character;

    [SerializeField] float dashForce, dashTime, startDashTime;
    void Start()
    {
        character = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
