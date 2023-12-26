using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            collision.gameObject.SetActive(false);
            
        }
        if (collision.gameObject.CompareTag("Lazer"))
        {
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.SpawnLazer();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Moon"))
        {
            Destroy(collision.gameObject);
        }
    }
}
