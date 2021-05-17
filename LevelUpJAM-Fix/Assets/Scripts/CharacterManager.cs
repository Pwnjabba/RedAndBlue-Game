using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public PlayerController blue, red;
    public PlayerController activeCharacter;
    public PlayerController inactiveCharacter;

    public Color inactiveColor, defaultColor;

    public bool noCollide, canToggleCollision;
    public bool charactersOverlapping;

    public bool characterReactivated;



    private void Start()
    {
        blue = GameObject.FindGameObjectWithTag("Blue").GetComponent<PlayerController>();
        red = GameObject.FindGameObjectWithTag("Red").GetComponent<PlayerController>();

        //Physics2D.IgnoreCollision(red.col, blue.col, true);
        defaultColor = Color.white;
        SetActiveCharacter();
        SpawnCharacters();
    }

    // Update is called once per frame
    void Update()
    {
        SetActiveCharacter();
        CheckForCharacterOverlap();

        if (Input.GetKeyDown(KeyCode.F) && activeCharacter.nonPlayerGrounded)
        {
            SwitchCharacters();
        }

        if (Input.GetKeyDown(KeyCode.R) && !noCollide && !charactersOverlapping)
        {
            noCollide = !noCollide;
            DisableCollisionBetweenAllies();
        }
        else if (Input.GetKeyDown(KeyCode.R) && noCollide && !charactersOverlapping)
        {
            noCollide = !noCollide;
            EnableCollisionBetweenAllies();
        }
       


    }

    public void SpawnCharacters()
    {
        activeCharacter.transform.position = CheckpointManager.instance.currentCheckPoint.transform.position;
        inactiveCharacter.transform.position = activeCharacter.transform.position + new Vector3(-2, .5f, 0);
    }

    void SetActiveCharacter()
    {
        if (red.isActive)
        {
            activeCharacter = red;
            inactiveCharacter = blue;
        }
        else
        {
            inactiveCharacter = red;
            activeCharacter = blue;
        }
    }

    void CheckForCharacterOverlap()
    {
        if (red.col.bounds.Intersects(blue.col.bounds))
        {
            charactersOverlapping = true;
        }
        else
            charactersOverlapping = false;
    }

    void DisableCollisionBetweenAllies()
    {

        Physics2D.IgnoreCollision(red.col, blue.col, true);

    }
    void EnableCollisionBetweenAllies()
    {
        Physics2D.IgnoreCollision(red.col, blue.col, false);
    }
    void SwitchCharacters()
    {
        //red.gameObject.SetActive(!red.gameObject.activeSelf);
        //blue.gameObject.SetActive(!blue.gameObject.activeSelf);
        red.ToggleActiveCharacter();
        blue.ToggleActiveCharacter();
    }

    void ToggleAllyCollision()
    {
        noCollide = !noCollide;
    }

    public void TeleportInactiveCharacter()
    {
        activeCharacter.TeleportOtherCharacterToThis();
    }


}
