using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    private GameObject _gameObject;

    public void Menu()
    {
        SceneManager.LoadScene("Game Start");
        _gameObject = GameObject.Find("ScoreManager");
        Destroy(_gameObject);
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
        _gameObject = GameObject.Find("ScoreManager");
        Destroy(_gameObject);
    }
    public void End()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); // 어플리케이션 종료
        #endif
    }
}
