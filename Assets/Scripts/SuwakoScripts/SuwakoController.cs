using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SuwakoController : MonoBehaviour
{
    //유닛 가장 필요한 변수들
    public SuwakoState currentState;
    public Rigidbody2D rb;
    public Animator animator;
    public BoxCollider2D boxCollider;

    [SerializeField]
    float _suwakoHP = 20;
    public float suwakoHP 
    {
        get => _suwakoHP; 
        set 
        {
            if (_suwakoHP >= 0)
            {
                _suwakoHP = value;
            }
            else if (_suwakoHP < 0)
            {
                _suwakoHP = 0;
            }

        }
    }
    float maxHealth = 20;

    float monsterHPfillAmount;


    //공격 받았을 때 상태 스크립트들
    public SuwakoGetHitState GetHitState { get; private set; }

    //움직임 상태 스크립트들
    public SuwakoIdleState idleState { get; private set; }
    public SuwakoWalkFrontState walkFrontState { get; private set; }
    public SuwakoFlyingState flyingState { get; private set; }
    public SuwakoJumpingState jumpingState { get; private set; }
    public SuwakoFallingState fallingState { get; private set; }
    public SuwakoLandingState landingState { get; private set; }

    //스킬 상태 스크립트들
    public SuwakoSkill0_ShootingBullet skill0_ShootingBullet {  get; private set; }
    public SuwakoSkill1_JumpORFlyShootingBullet skill1_JumpORFlyShootingBullet {  get; private set; }

    //스와코 탄알 발사하는 곳 위치
    public Transform bullet0Fire { get; private set; }

    //좌우 이동 변수
    float _xORy = 5;
    //전방으로 점프 변수
    float _frontJumpPower = 5;
    //날아다니는 파워
    float _flyingPower = 2;
    //점프 파워
    float _jumpPower = 10;

    //낙하 중 순간들
    public int falling { get; set; }

    //착지 중
    public bool landing { get; set; }

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

    //애니메이션을 위한 변수들
    public bool isSkill0End =false;
    public bool isLanding =false;

    //상태 변환
    public void ChangeState(SuwakoState newstate)
    {
        currentState = newstate;
        currentState.Enter(this);
    }

    private void Awake()
    {
        //
        GetHitState = new SuwakoGetHitState();

        //움직임 상태 스크립트들
        idleState = new SuwakoIdleState();
        walkFrontState = new SuwakoWalkFrontState();
        flyingState = new SuwakoFlyingState();
        jumpingState = new SuwakoJumpingState();
        fallingState = new SuwakoFallingState();
        landingState = new SuwakoLandingState();

        monsterHPfillAmount = suwakoHP / maxHealth;
    }

    void Start()
    {
        foreach (Transform child in transform)
        {

        }

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();


        //스킬 상태 스크립트들
        skill0_ShootingBullet = new SuwakoSkill0_ShootingBullet();
        skill1_JumpORFlyShootingBullet = new SuwakoSkill1_JumpORFlyShootingBullet();


        //상태 스타트문
        ChangeState(idleState);
    }

    void Update()
    {
        Debug.Log(currentState);
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
        //트리거 Enter
        currentState.OnTriggerEnter2D(this, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //트리거 Exit
        currentState.OnTriggerExit2D(this, collision);
    }
}
