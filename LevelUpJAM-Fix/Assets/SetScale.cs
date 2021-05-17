using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScale : MonoBehaviour
{
    public GameObject referenceObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 localScale = transform.localScale;
        if (referenceObject.transform.localScale.x < 0)
        {
            localScale.x = -1;
        }
        else
        {
            localScale.x = 1;
        }
        transform.localScale = localScale;
    }
}
