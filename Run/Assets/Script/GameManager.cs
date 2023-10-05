using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private Transform SpawnA;
    private Transform SpawnB;
    
    GameObject UpWalls;
    GameObject DownWalls;
    Player player;
    Text scoreText;

    private float maxSpawnDelay;
    private float curSpawnDelay;
    
    private int ranWalls;
    
    int poolSize = 20;
    private GameObject[] UwallObjectPool;
    private GameObject[] DwallObjectPool;

    private void Awake()
    {
        SpawnA = Resources.Load("Prefeb/Spawn/Up").GetComponent<Transform>();  
        SpawnB = Resources.Load("Prefeb/Spawn/Down").GetComponent<Transform>();

        UpWalls = Resources.Load<GameObject>("Prefeb/Wall Up");
        DownWalls = Resources.Load<GameObject>("Prefeb/Wall Down");
        player = Resources.Load<Player>("Prefeb/Player");
        scoreText = Resources.Load("Prefeb/Score").GetComponent<Text>();
    }

    void Start()
    {
        
        
        
        UwallObjectPool = new GameObject[poolSize];
        DwallObjectPool = new GameObject[poolSize];


        for (int i = 0; i < poolSize; i++) {
            GameObject Upwall = Instantiate(UpWalls);
            
            Upwall.SetActive(false);
            UwallObjectPool[i] = Upwall;
        }
        for (int i = 0; i < poolSize; i++) {
            GameObject Downwall = Instantiate(DownWalls);
            Downwall.SetActive(false);
            DwallObjectPool[i] = Downwall;
        }
    }
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnWalls();
            maxSpawnDelay = Random.Range(0.75f, 1f);
            curSpawnDelay = 0;
        }

        Player playerLogic = player;
        scoreText.text = string.Format("{0:n0}",playerLogic.score);
    }
    void SpawnWalls()
    {
        int ranPoint = Random.Range(0, 2);
        for (int i = 0; i < poolSize; i++)
        {
            if (ranPoint == 0)
            {
                GameObject wall = UwallObjectPool[i];

                if (wall.activeSelf == false) {
                    wall.SetActive(true);
                
                    wall.transform.position = SpawnA.position;
                        
                    break;
                }
            }
            else if (ranPoint == 1)
            {
                GameObject wall = DwallObjectPool[i];

                if (wall.activeSelf == false) {
                    wall.SetActive(true);
                
                    wall.transform.position = SpawnB.position;
                        
                    break;
                }
            }
        }
    }
}