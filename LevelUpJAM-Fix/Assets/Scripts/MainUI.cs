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
    public TextMeshProUGUI followMode, followerRangeStatus, characterOverlapWarningText;
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
        CharacterOverlapWarning();
        SetActiveCharacterIcon();
    }

    void CharacterOverlapWarning()
    {
        if (characterManager.charactersOverlapping)
        {
            characterOverlapWarningText.enabled = true;
        }
        else
        {
            characterOverlapWarningText.enabled = false;
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
