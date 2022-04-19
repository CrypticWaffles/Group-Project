using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public Transform spawnPoint;
    bool quit = false;
    public int spawnRate;

    public int count = 0;
    public int enemyLimit = 5;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemies), 0, spawnRate);
    }
    void Update()
    {
        if(count == enemyLimit)
        {
            CancelInvoke(nameof(SpawnEnemies));
        }
             
    }

    void SpawnEnemies()
    {
        Instantiate(enemy, spawnPoint);
        count++;
    }

}
