using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    public static MainUI instance;

    
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    CharacterManager characterManager;
    public TextMeshProUGUI followMode, followerRangeStatus, teleportInidatorText;
    public Image redSprite, blueSprite;

    public string text1, text2, text3;
    public Image activeCharacterPanel;

    public Color color1, color2, color3, color4, color5;
    PlayerController activePlayer;
    void Start()
    {
        characterManager = CharacterManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        TeleportIndicator();
        SetActiveCharacterIcon();
    }

    void TeleportIndicator()
    {
        if (characterManager.charactersOverlapping)
        {
            teleportInidatorText.text = text1;
        }
        else if (characterManager.noCollide)
        {
            teleportInidatorText.text = text2;
        }
        else
        {
            teleportInidatorText.text = text3;
        }

    }

    void SetActiveCharacterIcon()
    {
        if (characterManager.red.isActive)
        {
            redSprite.color = color1;
        }
        else
            redSprite.color = color2;

        if (characterManager.blue.isActive)
        {
            blueSprite.color = color1;
        }
        else
            blueSprite.color = color2;
    }


}
