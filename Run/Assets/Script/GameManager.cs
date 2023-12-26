using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private Transform _spawnA;
    private Transform _spawnB;
    
    GameObject _upWalls;
    GameObject _downWalls;
    public GameObject[] meteorSpawn;
    GameObject _lazerDis;
    GameObject _moon;
    GameObject _healingPotion;

    Player _playerLogic;
    public GameObject getscore;
    public float addWallSpeed = 0;
    
    private float _maxSpawnDelay;
    private float _curSpawnDelay;
    private int RanMeteor;

    private float _maxlazerSpawnDelay = 3f;
    private float _lazerSpawnDelay;
    
    private int _ranWalls;
    private int _poolSize = 20;
    private int _randPoint;
    private int _up = 0;
    private int _down = 0;

    private bool _spawnLazer = true;
    
    private GameObject[] _uwallObjectPool;
    private GameObject[] _dwallObjectPool;
    
    private void Awake()
    {
        _spawnA = Resources.Load("Prefeb/Spawn/Up").GetComponent<Transform>();  
        _spawnB = Resources.Load("Prefeb/Spawn/Down").GetComponent<Transform>();

        _upWalls = Resources.Load<GameObject>("Prefeb/Wall Up");
        _downWalls = Resources.Load<GameObject>("Prefeb/Wall Down");
        _lazerDis = Resources.Load<GameObject>("Prefeb/lazerDis");
        _moon = Resources.Load<GameObject>("Prefeb/moon");

        _playerLogic = GameObject.Find("Player").GetComponent<Player>();
    }

    void Start()
    {
        _uwallObjectPool = new GameObject[_poolSize];
        _dwallObjectPool = new GameObject[_poolSize];


        for (int i = 0; i < _poolSize; i++) {
            GameObject upwall = Instantiate(_upWalls);
            
            upwall.SetActive(false);
            _uwallObjectPool[i] = upwall;
        }
        for (int i = 0; i < _poolSize; i++) {
            GameObject Downwall = Instantiate(_downWalls);
            Downwall.SetActive(false);
            _dwallObjectPool[i] = Downwall;
        }
    }
    public void Update()
    {
        if (_playerLogic.hp <= 0)
        {
            SceneManager.LoadScene("Game End");
            DontDestroyOnLoad(getscore);
        }
        _curSpawnDelay += Time.deltaTime;

        if (_spawnLazer)
            _lazerSpawnDelay += Time.deltaTime;

        if (_curSpawnDelay > _maxSpawnDelay)
        {
            SpawnWalls();
            _maxSpawnDelay = Random.Range(1f, 2f);
            _curSpawnDelay = 0;
        }
        if (_lazerSpawnDelay > _maxlazerSpawnDelay)
        {
            RanMeteor = Random.Range(0, 2);
            SpawnLazerDis();
        }
        addWallSpeed += Time.deltaTime * 0.05f;
    }
    void SpawnWalls()
    {
        int ranPoint = Random.Range(0, 2);
        if (_up > 2)
            ranPoint = 1;
        if (_down > 2)
            ranPoint = 0;
        for (int i = 0; i < _poolSize; i++)
        {
            if (ranPoint == 0)
            {
                _down = 0;
                _up++;
                GameObject wall = _uwallObjectPool[i];

                if (wall.activeSelf == false) {
                    wall.SetActive(true);
                
                    wall.transform.position = _spawnA.position;
                        
                    break;
                }
            }
            else if (ranPoint == 1)
            {
                _up = 0;
                _down++;
                GameObject wall = _dwallObjectPool[i];
                
                if (wall.activeSelf == false) {
                    wall.SetActive(true);
                
                    wall.transform.position = _spawnB.position;
                        
                    break;
                }
            }
        }
    }
    void SpawnLazerDis()
    {
        Instantiate(_lazerDis, meteorSpawn[RanMeteor].transform.position, transform.rotation);
        _maxlazerSpawnDelay = Random.Range(4f, 6f);
        _lazerSpawnDelay = 0;
        _spawnLazer = false;
    }

    public void SpawnLazer()
    {
        Instantiate(_moon, meteorSpawn[RanMeteor].transform.position, transform.rotation);
        _spawnLazer = true;
    }
}