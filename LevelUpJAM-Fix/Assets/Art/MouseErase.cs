using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseErase : MonoBehaviour
{

    public Texture2D texture;

    public Color[] colors;

    public Color color;
    void Start()
    {
        texture  = new Texture2D(512, 512);
        GetComponent<Renderer>().material.mainTexture = texture;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePos);

        Color color = Color.clear;



        if (Input.GetMouseButton(0))
        {
            texture.SetPixels(Mathf.FloorToInt(mousePos.x) ,Mathf.FloorToInt(mousePos.y), 1, 1, colors, 0);
            texture.Apply();
        }
    }
}
