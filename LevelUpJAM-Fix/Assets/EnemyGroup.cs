using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public List<BaseEnemy> enemies = new List<BaseEnemy>();

    public Door door;
    void Start()
    {
        foreach (var enemy in GetComponentsInChildren<BaseEnemy>())
        {
            enemies.Add(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var enemy in enemies)
        {
            if (enemies.Contains(enemy) && enemy.health <= 0)
            {
                enemies.Remove(enemy);
            }

            if (enemies.Count == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
