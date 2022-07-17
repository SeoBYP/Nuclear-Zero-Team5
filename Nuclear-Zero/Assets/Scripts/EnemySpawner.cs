using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void SpawnEnemy()
    {
        GameObject enemy = ResourcesManager.Instance.Instantiate("Enemy/Enemy");
        enemy.transform.position = transform.position;
    }
}
