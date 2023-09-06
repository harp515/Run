using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] UpWalls;
    public GameObject[] DownWalls;
    public GameObject player;
    public Transform[] SpawnPoint;
    public Text scoreText;

    private float maxSpawnDelay;
    private float curSpawnDelay;
    
    private int ranWalls;
    
    int poolSize = 20;
    private GameObject[] UwallObjectPool;
    private GameObject[] DwallObjectPool;
    
    void Start()
    {
        UwallObjectPool = new GameObject[poolSize];
        DwallObjectPool = new GameObject[poolSize];


        for (int i = 0; i < poolSize; i++) {
            ranWalls = Random.Range(0, UpWalls.Length);
            GameObject Upwall = Instantiate(UpWalls[ranWalls]);
            
            Upwall.SetActive(false);
            UwallObjectPool[i] = Upwall;
        }
        for (int i = 0; i < poolSize; i++) {
            ranWalls = Random.Range(0, DownWalls.Length);
            GameObject Downwall = Instantiate(DownWalls[ranWalls]);
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
            maxSpawnDelay = Random.Range(0.75f, 1.5f);
            curSpawnDelay = 0;
        }

        Player playerLogic = player.GetComponent<Player>();
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
                
                    wall.transform.position = SpawnPoint[ranPoint].position;
                        
                    break;
                }
            }
            else if (ranPoint == 1)
            {
                GameObject wall = DwallObjectPool[i];

                if (wall.activeSelf == false) {
                    wall.SetActive(true);
                
                    wall.transform.position = SpawnPoint[ranPoint].position;
                        
                    break;
                }
            }
        }
    }
}
