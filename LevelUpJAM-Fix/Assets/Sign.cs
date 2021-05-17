using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public GameObject signText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Red" || collision.tag == "Blue")
        {
            signText.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        signText.SetActive(false);
    }
}
