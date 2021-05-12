using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAbilities : MonoBehaviour
{
    public Animator swordAnim;
    public PlayerController blue;
    BlueAudio blueAudio;
    void Start()
    {
        blueAudio = GetComponent<BlueAudio>();
        blue = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && blue.isActive)
        {
            if (swordAnim.GetCurrentAnimatorStateInfo(0).IsName("SwordIdle"))
            {
                blueAudio.PlaySwordSound();
            }
            if (blue.sprite.flipX)
            {
                swordAnim.SetBool("SwingL", true);
            } 
            else
            {
                swordAnim.SetBool("SwingR", true);
            }

        }
    }
}
