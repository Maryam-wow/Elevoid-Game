using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGhosts : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject[] ghost;

    public int enemySpawnDelay = 10;
    public int enemyLimit = 3;

    private int _enemyCount;
    

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(StartSpawning());
    }

    // *BEWARE* it will create multiple instances of the coroutine, so handle it explicitly.
    // Update is called once per frame
    // void Update()
    // {
    //     if (_enemyCount < enemyLimit) StartCoroutine(StartSpawning());
    // }
    
    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(enemySpawnDelay);
        SpawnEnemies();
    }
    
    /// <summary>
    /// Spawns the enemies of the ghost prefab upto the amount provided.
    /// </summary>
    /// <param name="numberToSpawn">the amount of enemies to spawn</param>
    void SpawnEnemies(int numberToSpawn = 3)
    {
        for (int i =0; i < numberToSpawn; i++) {
            Instantiate(ghost[i], SpawnPoints[i].position, Quaternion.identity);
        }
    }
}