using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMultiplayer : MonoBehaviour
{

    public Material playerMaterial;
    public Material enemyMaterial;
    public Material standardMaterial;
    public Material otherMaterial;
    public Material errorMaterial;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public int numberOfPlayers;
    public int numberOfEnemies;

    public float spawnBoundaryXMin;
    public float spawnBoundaryXMax;
    public float spawnBoundaryYMin;
    public float spawnBoundaryYMax;
    public float spawnBoundaryZMin;
    public float spawnBoundaryZMax;
    internal Material otherPlayerMaterial;
    internal Material otherEnemyMaterial;
}

