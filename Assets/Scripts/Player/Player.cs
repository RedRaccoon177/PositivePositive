using System;
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
    [SerializeField] SpriteRenderer playerSprite;
    PlayerHPObserver observer;

    public int MaxHP;
    int HP;
    public bool isJump = false;
    public bool isWall = false;
    public bool blockMove = false;
    [SerializeField] bool isInvincible = false;
    [SerializeField] bool canBoost = true;
    bool canSwingCharge = false;
    [SerializeField] bool attackMode = false;
    GameObject attackingEnemy;
    float startScaleX;
    [SerializeField] bool isCharging = false;

    [SerializeField] float invincibleTime;
    [SerializeField] float moveDelay;
    public ThrowHook throwHook;
    public float boostCool = 2.0f;
    public float boostPower;
    [SerializeField] float attackPower;
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
        observer = GetComponent<PlayerHPObserver>();

        startScaleX = transform.localScale.x;
        curBoostCool = boostCool;

        ropeActs = new RopeActs();
        groundActs = new GroundActs();

        playerActs = groundActs;
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {

        if (curBoostCool > boostCool)
        {
            canBoost = true;
        }
        else
        {
            curBoostCool += Time.deltaTime;
        }
        if (rigid.velocity.y < 0)
        {
            SetAnimState("IsFall", true);
        }
        else
        {
            SetAnimState("IsFall", false);
        }
        if (rigid.freezeRotation == true)
        {
            if (facingRight == -1)
            {
                if (Rigid.velocity.x < -groundMoveSpeed)
                {
                    Rigid.velocity = new Vector2(Mathf.Lerp(Rigid.velocity.x, -groundMoveSpeed, 0.7f), Rigid.velocity.y);
                }
            }
            else if (facingRight == 1)
            {
                if (Rigid.velocity.x > groundMoveSpeed)
                {
                    Rigid.velocity = new Vector2(Mathf.Lerp(Rigid.velocity.x, groundMoveSpeed, 0.7f), Rigid.velocity.y);
                }
            }
        }

        if (blockMove == false)
        {
            if (attackMode == false)
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
            else
            {
                transform.position = Vector2.Lerp(transform.position, attackingEnemy.transform.position, 0.2f);
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                    Vector2 vel = (mousePos - (Vector2)transform.position).normalized * attackPower;
                    rigid.velocity = vel;
                    attackMode = false;
                }
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

    public void IsHookAttach(bool state)
    {
        if (state == true)
        {
            playerActs = ropeActs;
            curBoostCool += Time.deltaTime;
            rigid.drag = 0;
            if (isCharging == false)
            {
                rigid.freezeRotation = false;
            }
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
    }

    public void SetAnimState(string param, bool state)
    {
        animator.SetBool(param, state);
    }

    public void ThrowRopeAnim()
    {
        animator.SetTrigger("ThrowRope");
    }

    //캐릭터 rotation을 갈고리를 바라보게하고 z축 고정한 다음 up으로 돌진
    public IEnumerator HookCharge(Vector3 playerPos, Vector3 hookPos)
    {
        Debug.Log("dist " + Vector2.Distance(playerPos, hookPos));
        if (Vector2.Distance(playerPos, hookPos) >= 0.55f)
        {
            isCharging = true;
            throwHook.GetCurHook().GetComponent<RopeScript>().lastNode.GetComponent<HingeJoint2D>().connectedBody = null;
            float angle = Mathf.Atan2(transform.position.y - hookPos.y, transform.position.x - hookPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

            rigid.freezeRotation = true;
            //float startTime = Time.time;

            Vector2 rayDir = ((Vector2)hookPos - (Vector2)throwHook.transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(throwHook.transform.position, rayDir, 50, LayerMask.GetMask("Platforms"));
            //Vector2 direction = (hookPos - rigid.transform.position).normalized;

            int childIndex = (int)(Vector2.Distance(transform.position, hookPos) / 2) - 1;

            Vector2 vel = (hookPos - playerPos).normalized * boostPower / Time.deltaTime;
            Debug.Log("velo " + vel);
            while (isCharging)
            {
                angle = Mathf.Atan2(transform.position.y - hookPos.y, transform.position.x - hookPos.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
                vel = (hookPos - playerPos).normalized * boostPower / Time.deltaTime;
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

                Debug.Log("Dist hook " + Vector2.Distance(throwHook.transform.position, hookPos));
                if (Vector2.Distance(throwHook.transform.position, hookPos) <= 0.2f)
                {
                    Debug.Log("Dist close");
                    rigid.velocity = Vector2.zero;
                    isCharging = false;
                }
                yield return null;
            }
            throwHook.transform.position = hit.point;
            throwHook.GetCurHook().GetComponent<HingeJoint2D>().connectedBody = rigid;
            rigid.freezeRotation = false;
            yield return null;
        }

    }

    public void SetRopeCharging(bool value)
    {
        isCharging=value;
    }

    public IEnumerator WallJump()
    {
        blockMove = true;
        yield return new WaitForSeconds(0.2f);
        blockMove = false;
    }

    public bool GetAttackMode()
    {
        return attackMode;
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

    public void AttackEnemy(GameObject enemy)
    {
        attackMode = true;
        isInvincible = true;
        attackingEnemy = enemy;
        if (throwHook.GetCurHook()?.GetComponent<RopeScript>().lastNode.GetComponent<HingeJoint2D>().connectedBody != null)
        {
            throwHook.GetCurHook().GetComponent<RopeScript>().lastNode.GetComponent<HingeJoint2D>().connectedBody = null;
        }
        SetCanCharge(false);
        Destroy(throwHook.GetCurHook());
        SetBoost(false);
        IsHookAttach(false);
    }

    public void GetHit(int damage)
    {
        if (isInvincible == false)
        {
            HP -= damage;
<<<<<<< Updated upstream
=======
            StartCoroutine(PlayerBlink());
>>>>>>> Stashed changes
            observer.NotifyHealthChange(MaxHP, HP);
            if (HP <= 0)
            {
                HP = 0;
                animator.SetTrigger("Die");
                throwHook.enabled = false;
                enabled = false;
                GameManager.Instance.GameOver();
                //게임오버 띄우기
                return;
            }
            Debug.Log("아프다");
            StartCoroutine(HitReact());
        }
    }

    IEnumerator PlayerBlink()
    {
        isInvincible = true;
        float delay = invincibleTime / 5 / 2;
        for (int i = 0; i < 5; i++)
        {
            playerSprite.color = new Color(1, 1, 1, 0.3f);
            yield return new WaitForSeconds(delay);
            playerSprite.color = new Color(1, 1, 1, 0.8f);
            yield return new WaitForSeconds(delay);
        }
        playerSprite.color = new Color(1, 1, 1, 1);
        isInvincible = false;
    }

    IEnumerator HitReact()
    {
        blockMove = true;
        SetAnimState("IsHit", true);
        rigid.velocity = new Vector2(groundMoveSpeed * -facingRight, rigid.velocity.y);
        yield return new WaitForSeconds(1f);
        SetAnimState("IsHit", false);
        blockMove = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if ((collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall")) && isJump == true)
        {
            isJump = false;
            isCharging = false;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            isWall = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetHit(1);
            //Destroy(collision.gameObject);
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
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("WeakPoint")))
        {
            isInvincible = false;
        }
    }
}
