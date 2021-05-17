using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpriteFlipX : MonoBehaviour
{
    public SpriteRenderer refSprite;
    public SpriteRenderer thisSprite;
    bool flipped;
    void Start()
    {
        thisSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("SwordIdle"))
        {
            //thisSprite.enabled = false;
            flipped = refSprite.flipX;

        }
        if (flipped)
        {
            Vector2 flipVector = new Vector2(0, 180);
            transform.rotation = Quaternion.Euler(flipVector);
        }
        else
        {
            Vector2 defaultVector = new Vector2(0, 0);
            transform.rotation = Quaternion.Euler(defaultVector);
        }

    }
}
