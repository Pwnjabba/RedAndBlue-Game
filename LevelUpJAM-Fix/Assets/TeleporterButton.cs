using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;

public class TeleporterButton : MonoBehaviour
{
    public UnityEvent teleportEvent;
    public bool hasTeleported;
    SpringJoint2D springJoint;
    public Light2D bgLight, mainLight;
    public ParticleSystem ps;
    public Color color1, color2;

    public AudioClip teleportSound;
    

    public int checkPointNumber;
    void Start()
    {
        springJoint = GetComponent<SpringJoint2D>();
        springJoint.connectedAnchor = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (hasTeleported)
        {
            bgLight.color = color1;
            mainLight.color = color1;
        }
    }
    private void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("World") && !hasTeleported)
        {
            var main = ps.main;
            main.startColor = new ParticleSystem.MinMaxGradient(new Color(0, 200, 100, 100));
            ImmortalAudio.instance.PlayAudioClip(teleportSound);
            Debug.Log("Button pressed");
            hasTeleported = true;
            teleportEvent.Invoke();
            CheckpointManager.instance.SetCheckPoint(checkPointNumber);
            //set checkpoint
        }
    }
}
