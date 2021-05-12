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
        if (Input.GetKeyDown(KeyCode.E))
        {
            character.StartDash(true, dashForce);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            character.StartDash(false, dashForce);
        }
    }
}
