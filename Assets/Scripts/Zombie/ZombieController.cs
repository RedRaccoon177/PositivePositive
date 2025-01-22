using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class ZombieController : MonoBehaviour
{
    public Collider2D zombieCollider;
    Animator animator;
    Rigidbody2D rigid;
    ZombieState zombieState;
    public ZombieObjectPooling zomObjPool { get; set; }
    public ZombieWormBulletPool zomBulletObjPool { get; set; }
    [SerializeField] GameObject playerInfo;
    [SerializeField] GameObject wormPrepeb;
    [SerializeField] GameObject jumpSkillRange;
    [SerializeField] GameObject blackHoleSkillPrepeb;

    public GameObject WormPrepeb
    {
        get
        {
            return wormPrepeb;
        }
    }

    public GameObject BlackHoleSkillPrepeb
    {
        get
        {
            return blackHoleSkillPrepeb;
        }
    }

    //public GameObject WormShieldPrefab
    //{
    //    get
    //    {
    //        return wormShieldPrefab;
    //    }
    //    set
    //    {
    //        wormShieldPrefab = value;
    //    }
    //}
    public GameObject PlayerInfo
    {
        get
        {
            return playerInfo;
        }
    }
    public GameObject JumpSkillRange
    {
        get
        {
            return jumpSkillRange;
        }
        set
        {
            jumpSkillRange = value;
        }
    }

    public float zombieHp;
    public short directionX { get; set; }
    public short directionY { get; set; }
    // ______________ 상태 ___________________
    public ZombieDie zombiDie { get; private set; }
    public ZombieHitted zombieHitted { get; set; }
    public ZombieSkillBlackHole skillBlackHole { get; private set; }
    public ZombieJump jump { get; private set; }
    public ZombieJumpReady jumpReady { get; private set; }
    public ZombieWalk walk { get; private set; }
    public ZombieIdle idle { get; private set; }
    public ZombieWormBullet wormBullet { get; private set; }
    public ZombieSkillWormBullet skillWormBullet { get; private set; }
    // ___________ 컴포넌트 _____________________
    public Animator Animator { get { return animator; } }
    public Rigidbody2D Rigid { get { return rigid; } }
    // ___________ 점프 기능 _____________________
    public float distance { get; set; }
    public RaycastHit2D ray2d { get; set; }
    public Vector2 mapBounds { get; set; }
    public Vector2 mosterToPlayer;
    public bool jumpOn = false;
    public bool isHitted = false;
    public bool isDie = false;
    // ______________ 스킬 1 WormShield ___________________

    public int wormMaxCount { get; set; }
    public int wormHaveCount { get; set; }
    public bool wormCreated { get; set; }
    public float deltaTime { get; set; }
    public int randState;
    public int randDirect;


    public void ChangeState(ZombieState newState)
    {
        zombieState = newState;
        zombieState.Enter(this);
        deltaTime = 0;
    }
    private void Awake()
    {
        //________ 상태 _____________
        skillWormBullet = new ZombieSkillWormBullet();
        zombiDie = new ZombieDie();
        zombieHitted = new ZombieHitted();
        jump = new ZombieJump();
        jumpReady = new ZombieJumpReady();
        walk = new ZombieWalk();
        idle = new ZombieIdle();
        skillBlackHole = new ZombieSkillBlackHole();
        
    }
    void Start()
    {
        zomObjPool = gameObject.GetComponent<ZombieObjectPooling>();
        zomBulletObjPool = gameObject.GetComponent<ZombieWormBulletPool>();
        zomBulletObjPool.CreatObject();
        zomObjPool.CreatObject();
        wormHaveCount = 0;
        wormMaxCount = 4;
        zombieHp = 20;
        distance = 100f;
        //_____________________
        zombieCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        ChangeState(idle);
        
    }

    void Update()
    {
        if (zombieState != null)
        {
            zombieState.Update(this);
        }
    }

    private void FixedUpdate()
    {
        if (zombieState != null)
        {
            zombieState.FixUpdate(this);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (zombieState != null)
        {
            zombieState.OnCollisionEnter2D(this, collision);
        }

    }
    public void CorutinPlay(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
