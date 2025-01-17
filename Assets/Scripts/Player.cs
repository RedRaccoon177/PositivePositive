using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;

    public Rigidbody2D Rigid
    {
        get
        {
            return rigid;
        }

        set
        {
            rigid = value;
        }
    }

    RopeActs ropeActs;
    GroundActs groundActs;
    PlayerActs playerActs;
    Animator animator;

    public int HP;
    public bool isJump = false;
    public bool isWall = false;
    bool isWallJumping = false;
    [SerializeField] bool canBoost = true;
    bool canSwingCharge = false;
    float startScaleX;
    [SerializeField] bool isCharging = false;

    public ThrowHook throwHook;
    public float boostCool = 2.0f;
    public float boostPower;
    float curBoostCool;
    public float jumpPower;
    public float groundMoveSpeed;
    public float swingSpeed;
    public float swingLimit;
    public int facingRight;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        startScaleX = transform.localScale.x;
        curBoostCool = boostCool;

        ropeActs = new RopeActs();
        groundActs = new GroundActs();

        playerActs = groundActs;
    }

    // Update is called once per frame
    void Update()
    {
        if (throwHook.IsHookEnabled())
        {
            playerActs = ropeActs;
            curBoostCool += Time.deltaTime;
            rigid.drag = 0;
            rigid.freezeRotation = false;
            canSwingCharge = true;
            SetAnimState("IsSwing", true);
            if (curBoostCool > boostCool)
            {
                canBoost = true;
            }
        }
        else
        {
            rigid.drag = 0.3f;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rigid.freezeRotation = true;
            SetAnimState("IsSwing", false);
            playerActs = groundActs;
            canSwingCharge = false;
        }

        if (rigid.velocity.y < 0)
        {
            SetAnimState("IsFall", true);
        }
        else
        {
            SetAnimState("IsFall", false);
        }

        if (isWallJumping == false)
        {
            if (isWall == true)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * -0.5f);
            }

            if (Input.GetKey(KeyCode.A))
            {
                playerActs.MoveLeftAct(this);
                facingRight = -1;

            }

            if (Input.GetKey(KeyCode.D))
            {
                playerActs.MoveRightAct(this);
                facingRight = 1;
            }


            if (Input.GetMouseButtonDown(0))
            {
                ThrowRopeAnim();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerActs.ShiftAct(this);
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                SetAnimState("IsWalk", false);
                if (!throwHook.IsHookEnabled() && !isJump)
                {
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
                }
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                SetAnimState("IsWalk", false);
                if (!throwHook.IsHookEnabled() && !isJump)
                {
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (Time.timeScale <= 0.6f)
                {
                    Time.timeScale = 1f;
                }
                else
                {
                    Time.timeScale = 0.5f;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerActs.SpaceAct(this);
            }
        }

        if (facingRight == -1)
        {
            transform.localScale = new Vector3(startScaleX * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (facingRight == 1)
        {
            transform.localScale = new Vector3(startScaleX, transform.localScale.y, transform.localScale.z);
        }
    }

    public void SetAnimState(string param, bool state)
    {
        animator.SetBool(param, state);
    }

    public void ThrowRopeAnim()
    {
        animator.SetTrigger("ThrowRope");
    }

    public IEnumerator HookCharge(Vector3 playerPos, Vector3 hookPos)
    {
        Debug.Log("dist " + Vector2.Distance(playerPos, hookPos));
        if (Vector2.Distance(playerPos, hookPos) >= 0.55f)
        {
            isCharging = true;
            //float startTime = Time.time;
            int childIndex = (int)(Vector2.Distance(transform.position, hookPos) / 2) - 1;
            //Vector2 direction = (hookPos - rigid.transform.position).normalized;
            Vector2 vel = (hookPos - playerPos).normalized * boostPower / Time.deltaTime;
            Debug.Log("velo " + vel);
            while (isCharging)
            {
                rigid.velocity = vel;
                //rigid.MovePosition((Vector2)rigid.transform.position + direction * 200 * Time.deltaTime);
                //transform.position = Vector3.Lerp(playerPos, hookPos, (Time.time - startTime) / chargeTime);
                childIndex = (int)(Vector2.Distance(transform.position, hookPos) / 2) - 1;
                //Debug.Log("dist " + Vector2.Distance(transform.position, hookPos));
                if (childIndex >= 0 && throwHook.GetCurHook().transform.childCount > childIndex && throwHook.GetCurHook().transform.GetChild(childIndex) != null)
                {
                    Destroy(throwHook.GetCurHook().transform.GetChild(childIndex).gameObject);
                    throwHook.GetCurHook().GetComponent<RopeScript>().SetLineRenderCount(childIndex);
                }
                yield return null;
            }
            throwHook.transform.position = hookPos;
            throwHook.GetCurHook().GetComponent<HingeJoint2D>().connectedBody = rigid;
        }
    }

    public IEnumerator WallJump()
    {
        isWallJumping = true;
        yield return new WaitForSeconds(0.2f);
        isWallJumping = false;
    }

    public bool GetCanBoost()
    {
        return canBoost;
    }

    public void SetBoost(bool state)
    {
        canBoost = state;
        if (state == false)
        {
            curBoostCool = 0;
        }
    }

    public bool GetCanCharge()
    {
        return canSwingCharge;
    }

    public void SetCanCharge(bool state)
    {
        canSwingCharge = state;
    }

    public void GetHit(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            //죽기
            return;
        }

        
    }

    IEnumerator HitReact()
    {
        rigid.velocity = new Vector2();
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall")) && isJump == true)
        {
            isJump = false;
            isCharging = false;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            isWall = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall")) && isJump == true)
        {
            isJump = false;
            isCharging = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = true;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            isJump = true;
            isWall = false;
        }
    }
}
