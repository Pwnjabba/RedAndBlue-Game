using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermadeadEnemies : MonoBehaviour
{
    public static PermadeadEnemies instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public List<int> permaDeadEnemyIDs = new List<int>();


}
