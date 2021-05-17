using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCheck : MonoBehaviour
{
    public Transform headCheckPoint;
    public float radius;
    public bool noHeadRoom;
    public LayerMask layerMask;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        noHeadRoom = Physics2D.OverlapCircle(headCheckPoint.position, radius, layerMask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(headCheckPoint.position, radius);
    }


}
