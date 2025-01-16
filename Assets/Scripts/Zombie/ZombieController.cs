using Unity.VisualScripting;
using UnityEngine;


public class ZombieController : MonoBehaviour
{
    public Collider2D zombieCollider;
    Animator animator;
    Rigidbody2D rigid;
    State zombieState;
    [SerializeField] GameObject playerInfo;
    [SerializeField] GameObject wormShieldPrefab;
    [SerializeField] GameObject jumpSkillRange;

    public GameObject WormShieldPrefab
    {
        get
        {
            return wormShieldPrefab;
        }
        set
        {
            wormShieldPrefab = value;
        }
    }
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

    public short directionX { get; set; }
    public short directionY { get; set; }
    public Vector2 mapBounds;
    public Vector2 mosterToPlayer;
    // ______________ ป๓ลย ___________________
    public Jump jump { get; private set; }
    public JumpReady jumpReady { get; private set; } 
    public Walk walk { get; private set; }
    public Defalut defalut { get; private set; }
    // _____________________________________
    public Animator Animator { get { return animator; } }
    public Rigidbody2D Rigid { get { return rigid; } }

    public float distance;
    public RaycastHit2D ray2d;

    public bool jumpOn=false;

    public int wormShieldCount { get; set; }
    public int wormShieldMaxCount { get; set; }
    public float deltaTime { get; set; }

    public void ChangeState(State newState)
    {
        zombieState = newState;
        zombieState.Enter(this);
        deltaTime = 0;
    }
    void Start()
    {
        distance = 100f;
        
        //jumpSkillRange = Instantiate(jumpSkillRange);
        //jumpSkillRange.transform.SetParent(transform);


        wormShieldCount = 0;
        wormShieldMaxCount = 5;
        //______________________
        jump = new Jump();
        jumpReady = new JumpReady();
        walk = new Walk();
        defalut = new Defalut();
        //_____________________
        //setActive = new SetActive();
        zombieCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        ChangeState(defalut);

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
            zombieState.OnTriggerEnter2D(this, collision);
        }
        
    }

}
