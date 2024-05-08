using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private float speed = 25f;
    private Vector3 Start_Point;
    private float repeatwidth;
    private GameManager gameManager;


    private void Start()
    {
        Start_Point = transform.position;
        repeatwidth = GetComponent<BoxCollider>().size.x / 2;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        if(gameManager.is_Activate == true)
        {
            MapMove();
            Map_Init_();
        }

    }

    void MapMove() // map move to player opposite direction
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void Map_Init_() // if map move that half point, map position init for player to show infinite map
    {
        if(transform.position.x <=Start_Point.x - repeatwidth)
        {
            transform.position = Start_Point;
        }
    }
}
