using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    public Vector2 dest;

    public float speed = 1;

    public float dist = 2;

    public float limitDist;



    public GameObject node;
    public GameObject player;
    public GameObject lastNode;

    GameObject obj;
    Vector2 createPos;
    bool done = false;
    bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        lastNode = transform.gameObject;

        float angle = Mathf.Atan2(player.transform.position.y - dest.y, player.transform.position.x - dest.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered == false)
        {
            //transform.position = Vector2.MoveTowards(transform.position, dest, speed);
            transform.Translate(0, -speed, 0);


            if (Vector2.Distance(player.transform.position, transform.position) >= limitDist)
            {
                Destroy(gameObject);
            }

            if ((Vector2)transform.position != dest)
            {
                if (Vector2.Distance(player.transform.position, lastNode.transform.position) > dist)
                {
                    CreateNode();
                }
            }
            else if (done == false)
            {
                done = true;

                lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
            }
        }
    }

    void CreateNode()
    {
        Debug.Log("1 " + player.transform.position + " " + lastNode.transform.position);
        //두 점 사이 거리 계산
        createPos = player.transform.position - lastNode.transform.position;
        Debug.Log("2 " + createPos);
        createPos.Normalize();
        Debug.Log("3 " + createPos);
        createPos *= dist;
        Debug.Log("4 " + createPos);
        createPos += (Vector2)lastNode.transform.position;
        Debug.Log("5 " + createPos);

        obj = Instantiate(node, createPos, Quaternion.identity);

        obj.transform.SetParent(transform);

        lastNode.GetComponent<HingeJoint2D>().connectedBody = obj.GetComponent<Rigidbody2D>();

        lastNode = obj;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && (collision.CompareTag("Ground") || collision.CompareTag("Wall")))
        {
            triggered = true;
            done = true;

            lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
        }
    }
}
