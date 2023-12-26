using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float wallSpeed = 9;
    void Update()
    {
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.Translate(Vector2.left * (Time.deltaTime * (wallSpeed + manager.addWallSpeed)));
    }
}
