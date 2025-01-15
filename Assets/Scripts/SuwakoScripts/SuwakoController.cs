using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SuwakoController : MonoBehaviour
{
    //유닛 가장 필요한 변수들
    State currentState;
    public Rigidbody2D rb;
    public Animator animator;
    public BoxCollider2D boxCollider;

    //움직임 상태 스크립트들
    public IdleState idleState { get; private set; }
    public WalkFrontState walkFrontState { get; private set; }
    public FlyingState flyingState { get; private set; }
    public JumpingState jumpingState { get; private set; }
    public FallingState fallingState { get; private set; }


    //좌우 이동 변수
    float _xORy = 5;
    //전방으로 점프 변수
    float _frontJumpPower = 10;
    //날아다니는 파워
    float _flyingPower = 2;
    //점프 파워
    float _jumpPower = 10;


    //공용 좌우 활용
    public int RiORLe { get; set; }

    //프로퍼티들
    public float xORy
    {
        get => _xORy;
    }
    public float frontJumpPower
    {
        get => _frontJumpPower;
    }
    public float flyingPower
    {
        get => _flyingPower;
    }
    public float jumpPower
    {
        get => _jumpPower;
    }

    //상태 변환
    public void ChangeState(State newstate)
    {
        currentState = newstate;
        currentState.Enter(this);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        //움직임 상태 스크립트들
        idleState = new IdleState();
        walkFrontState = new WalkFrontState();
        flyingState = new FlyingState();
        jumpingState = new JumpingState();
        fallingState = new FallingState();

        //상태 스타트문
        ChangeState(idleState);
    }

    void Update()
    {
        //상태 Update
        currentState.Update(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //상태 OnCollisionEnter2D
        currentState.OnCollisionEnter2D(this, collision);
    }

    void FixedUpdate()
    {
        //상태 FixUpdate
        currentState.FixUpdate(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnTriggerEnter2D(this, collision);

        if (collision.tag == "SideWall")
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            if (RiORLe == -1)
            {
                RiORLe = 1;
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            else if (RiORLe == 1)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                RiORLe = -1;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentState.OnTriggerExit2D(this, collision);
    }
}
