using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMoveMent : MonoBehaviour
{
    Rigidbody2D rigid;
    //int directionX;
    float test;
    float num;
    float randNum;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        test = 0;
        randNum = Random.Range(0.002f, 0.007f);
        num = randNum;
    }

    // Update is called once per frame
    void Update()
    {
        //test += num;
        transform.position = new Vector2(transform.position.x + num, transform.position.y);
        if (transform.position.x + num < - 15|| transform.position.x + num > 15)
        {
            ChangeDirectionX();
        }
    }
    public void ChangeDirectionX()
    {
        num *= -1;
    }
}
