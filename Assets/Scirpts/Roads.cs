using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roads : MonoBehaviour
{
    private float destory_Boarder_XPos = -130;
    private float speed = 25f;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        if (gameManager.is_Activate == true)
        {
            RoadMove();
        }
    }

    private void Update()
    {
        RoadDestroy();
    }

    void RoadMove() // road move to player opposite direction 
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void RoadDestroy()
    {
        if(transform.position.x <= destory_Boarder_XPos)
        {
            Destroy(gameObject);
        }
    }

    

}
