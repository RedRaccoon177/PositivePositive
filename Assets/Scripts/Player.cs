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

    public bool isJump = false;
    public bool isWall = false;
    bool isWallJumping = false;
    bool canBoost = true;

    public ThrowHook throwHook;
    public float boostCool = 2.0f;
    float curBoostCool;
    public float jumpPower;
    public float groundMoveSpeed;
    public float swingSpeed;
    public int facingRight;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
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
            if (curBoostCool >  boostCool )
            {
                canBoost = true;
            }
        }
        else
        {
            playerActs = groundActs;
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

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerActs.ShiftAct(this);
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                if (!throwHook.IsHookEnabled() && !isJump)
                {
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
                }
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                if (!throwHook.IsHookEnabled() && !isJump)
                {
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerActs.SpaceAct(this);
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall")) && isJump == true)
        {
            isJump = false;
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
