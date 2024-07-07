using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject player;
    TMP_Text scoreText;
    private float _scoreTime = 0;
    public int score = 0;

    public static ScoreManager intance = null;

    private void Awake()
    {
        intance = this;
    }

    void Update()
    {
        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
        scoreText.text = string.Format("Score: {0:n0}", score);
        if (_scoreTime >= 0.1f)
        {
            score += 1;
            _scoreTime = 0;
        }
        if (player != null)
            _scoreTime += Time.deltaTime;
    }

    public void GameEnd()
    {
        
    }
}
