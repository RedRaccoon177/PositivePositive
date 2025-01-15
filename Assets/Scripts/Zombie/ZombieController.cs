using UnityEngine;


public class ZombieController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    State zombieState;
    [SerializeField] GameObject playerInfo;
    [SerializeField] GameObject wormShieldPrefab;

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

    //public LayerMask playerLayer;
    public short directionX { get; set; }
    public short directionY { get; set; }

    //public Transform target;
    public Vector2 mosterToPlayer;
    public Jump jump { get; private set; }
    public JumpReady jumpReady { get; private set; } 
    public Walk walk { get; private set; }
    public Defalut defalut { get; private set; }

    public Animator Animator { get { return animator; } }
    public Rigidbody2D Rigid { get { return rigid; } }

    public SetActive setActive;

    public bool jumpOn=false;


    public void ChangeState(State newState)
    {
        zombieState = newState;
        zombieState.Enter(this);
    }
    void Start()
    {
        //______________________
        jump = new Jump();
        jumpReady = new JumpReady();
        walk = new Walk();
        defalut = new Defalut();
        //_____________________
        //setActive = new SetActive();
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (zombieState != null)
        {
            zombieState.OnTriggerEnter2D(this, collision);
        }
    }
}
