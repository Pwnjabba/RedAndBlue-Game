using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);

        foreach (var checkpoint in GetComponentsInChildren<Checkpoint>())
        {
            if (!checkPoints.Contains(checkpoint))
            {
                checkPoints.Add(checkpoint);
            }
        }
    }

    public PlayerController activePlayer, inactivePlayer;
    public List<Checkpoint> checkPoints = new List<Checkpoint>();
    public Checkpoint currentCheckPoint;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        activePlayer = CharacterManager.instance.activeCharacter;
        inactivePlayer = CharacterManager.instance.inactiveCharacter;
    }

    public void SetCheckPoint(int num)
    {
        currentCheckPoint = checkPoints[num];
    }
}
