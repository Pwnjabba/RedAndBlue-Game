using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;   
public class EventButton : MonoBehaviour
{
    public UnityEvent buttonEvent;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Red") || collision.gameObject.layer == LayerMask.NameToLayer("Blue"))
        buttonEvent.Invoke();
    }
}
