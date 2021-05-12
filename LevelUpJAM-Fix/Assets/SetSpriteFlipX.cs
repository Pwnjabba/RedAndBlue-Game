using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpriteFlipX : MonoBehaviour
{
    public SpriteRenderer refSprite;
    public SpriteRenderer thisSprite;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("SwordIdle"))
        {
            thisSprite.enabled = false;
            thisSprite.flipX = refSprite.flipX;

        }
        else
        {
            thisSprite.enabled = true;
        }

    }
}
