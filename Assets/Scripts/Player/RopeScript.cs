using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    public Vector2 dest;

    public float speed = 1;

    public float dist = 2;

    public float limitDist;

    Rigidbody2D rb;

    public GameObject node;
    public GameObject player;
    public GameObject lastNode;
    public LineRenderer lineRenderer;

    GameObject obj;
    Vector2 createPos;
    Vector2 colPos;
    bool done = false; //생성이 다 되었는가
    bool triggered = false; //갈고리가 닿았는가

    int vertexCount = 2;
    public List<GameObject> nodes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //player = GameObject.Find("Player");

        lastNode = gameObject;
        nodes.Add(gameObject);

        float angle = Mathf.Atan2(player.transform.position.y - dest.y, player.transform.position.x - dest.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    private void Update()
    {
        if (triggered == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 0.1f, LayerMask.GetMask("Platforms", "WeakPoint"));
            Debug.DrawRay(transform.position, transform.up, new Color(1, 0, 0));
            if (hit.collider != null)
            {
                colPos = transform.position;
                Debug.Log(hit.collider.name);
                triggered = true;
                if (hit.collider.CompareTag("WeakPoint"))
                {
                    player.GetComponentInParent<Player>().AttackEnemy(hit.collider.gameObject);
                }
            }
            //transform.Translate(0, speed, 0);

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
                player.GetComponentInParent<Player>().IsHookAttach(true);
                lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
            }
        }
        else
        {
            transform.position = dest;
            if (lastNode.GetComponent<HingeJoint2D>()?.connectedBody == null)
            {
                lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
                player.GetComponentInParent<Player>().IsHookAttach(true);
                lastNode.GetComponent<HingeJoint2D>().useLimits = true;

            }
        }
        RenderLine();
    }

    // Update is called once per frame
    void FixedUpdate() //Update에서 감지하고 Fixed에서 물리처리
    {
        if (triggered == false)
        {
            //rb.velocity = new Vector2(transform.up.x * speed, transform.up.y * speed);
            transform.Translate(0, speed, 0);
        }
    }

    public void SetLineRenderCount(int count)
    {
        lineRenderer.positionCount = count;
    }

    public bool GetDone()
    {
        return done;
    }

    void RenderLine()
    {
        lineRenderer.positionCount = vertexCount;

        int i;
        for (i = 0; i < nodes.Count; i++)
        {
            lineRenderer.SetPosition(i, nodes[i].transform.position);
        }

        lineRenderer.SetPosition(i, player.transform.position);
    }

    void CreateNode()
    {
        //두 점 사이 거리 계산
        createPos = player.transform.position - lastNode.transform.position;
        //정규화
        createPos.Normalize();
        //값에 설정한 간격을 곱해줌
        createPos *= dist;
        //마지막 노드에서 구한 간격을 더해서 노드의 생성 위치 구함
        createPos += (Vector2)lastNode.transform.position;

        Debug.Log(createPos);

        obj = Instantiate(node, createPos, Quaternion.identity);

        obj.transform.SetParent(transform);

        float angle = Mathf.Atan2(player.transform.position.y - dest.y, player.transform.position.x - dest.x) * Mathf.Rad2Deg;
        obj.transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);

        lastNode.GetComponent<HingeJoint2D>().connectedBody = obj.GetComponent<Rigidbody2D>();

        lastNode = obj;
        nodes.Add(obj);
        vertexCount++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("WeakPoint")))
        {
            if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
            {
                transform.SetParent(collision.gameObject.transform);
            }
            rb.velocity = Vector2.zero;
            transform.position = dest;
            triggered = true;
            done = true;
            player.GetComponentInParent<Player>().SetBoost(true);
            player.GetComponentInParent<Player>().IsHookAttach(true);
            lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();

            if (collision.gameObject.CompareTag("WeakPoint"))
            {
                player.GetComponentInParent<Player>().AttackEnemy(collision.gameObject);
            }
        }
    }

}
