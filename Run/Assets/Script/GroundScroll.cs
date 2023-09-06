using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroll : MonoBehaviour
{
    public SpriteRenderer[] tiles;
    public Sprite[] groundImg;
    private SpriteRenderer temp;
    public float speed = 6;
    public float x = 2;
    public float y = -4;
    public float returnPos = -12;
    void Start()
    {
        temp = tiles[0];
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (returnPos > tiles[i].transform.position.x)
            {
                for (int q = 0; q < tiles.Length; q++)
                {
                    if (temp.transform.position.x < tiles[q].transform.position.x)
                        temp = tiles[q];
                }

                tiles[i].transform.position = new Vector2(temp.transform.position.x + x, y);
                tiles[i].sprite = groundImg[Random.Range(0, groundImg.Length)];
            }
        }
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].transform.Translate(new Vector2(-1,0) * (Time.deltaTime * speed));
        }
    }
}