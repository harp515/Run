using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] UpWalls;
    public GameObject[] DownWalls;
    public Transform[] SpawnPoint;

    private float maxSpawnDelay;
    private float curSpawnDelay;
    
    private int ranWalls;
    // Update is called once per frame
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnWalls();
            maxSpawnDelay = Random.Range(0.75f, 1.5f);
            curSpawnDelay = 0;
        }
    }
    void SpawnWalls()
    {
        int ranPoint = Random.Range(0, 2);
        if (ranPoint == 0)
        {
            ranWalls = Random.Range(0, UpWalls.Length);
            Instantiate(UpWalls[ranWalls],
                SpawnPoint[ranPoint].position,
                SpawnPoint[ranPoint].rotation);
        }
        else if (ranPoint == 1)
        {
            ranWalls = Random.Range(0, DownWalls.Length);
            Instantiate(DownWalls[ranWalls],
                SpawnPoint[ranPoint].position,
                SpawnPoint[ranPoint].rotation);
        }
    }
}
