using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghostscript : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject[] ghost;
    public static int enemyCount;
    public static int enemyLimit;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(20);
        for (int i = 0; i < 3; i++)
        {
            Instantiate(ghost[i], SpawnPoints[i].position, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount < enemyLimit)
            StartCoroutine(StartSpawning());
    }
}