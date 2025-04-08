using UnityEngine;

public class ZombieWalk : ZombieState
{
    int maxRandNum = 100;
    int leftRange = 25;
    int rightRange = 50;
    int upRange = 75;
    int downRange = 100;
    int increaseRange = 0;


    //public override void OnTriggerEnter2D(ZombieController zombie, Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Wall")
    //    {
    //        //DirectionChangeWall(zombie);
    //    }
    //}
    public override void Enter(ZombieController zombie)
    {
        zombie.Animator.SetBool("IsHittedd", false);
        zombie.Animator.SetBool("IsWalk", true);
        RandomMoveRange(zombie);
    }

    public override void Update(ZombieController zombie)
    {
        zombie.deltaTime += Time.deltaTime;
        if (zombie.deltaTime >= 3)
        {
            zombie.deltaTime = 0;
            zombie.ChangeState(zombie.idle);
        }
    }

    public override void FixUpdate(ZombieController zombie)
    {
        DirectionAddForce(zombie);
        DirectionRotation(zombie);
    }
    public override void OnCollisionEnter2D(ZombieController zombie, Collision2D collision)
    {

        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
        {
            Debug.Log("ㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏㅏ");
            DirectionChangeWall(zombie);    
        }
    }
    void ResetDiectionRange()
    {
        maxRandNum = 100;
        leftRange = 25;
        rightRange = 50;
        upRange = 75;
        downRange = 100;
        increaseRange = 0;
    }
    void DirectionChangeWall(ZombieController zombie)
    {
        zombie.directionX *= -1;
        zombie.directionY *= -1;
    }
    void DirectionAddForce(ZombieController zombie)
    {
        if (zombie.directionX == -1 || zombie.directionX == 1)
        {
            zombie.Rigid.velocity = new Vector2(2.0f * zombie.directionX, 0);
        }
        if (zombie.directionY == 1 || zombie.directionY == -1)
        {
            zombie.Rigid.velocity = new Vector2(0, 2.0f * zombie.directionY);
        }
    }
    void DirectionRotation(ZombieController zombie)
    {
        if (zombie.directionX == -1)
        {
            zombie.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (zombie.directionX == 1)
        {
            zombie.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (zombie.directionY == 1)
        {
            zombie.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (zombie.directionY == -1)
        {
            zombie.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
    void RandomMoveRange(ZombieController zombie)
    {
        zombie.randDirect = UnityEngine.Random.Range(0, maxRandNum + increaseRange);
        if (0 < zombie.randDirect && zombie.randDirect < leftRange)
        {
            zombie.directionX = -1;
            leftRange -= 5;
        }
        else if (leftRange < zombie.randDirect && zombie.randDirect < rightRange)
        {
            zombie.directionX = 1;
            rightRange -= 5;
        }
        else if (rightRange < zombie.randDirect && zombie.randDirect < upRange)
        {
            zombie.directionY = 1;
            upRange -= 5;
        }
        else if (upRange < zombie.randDirect && zombie.randDirect < downRange)
        {
            zombie.directionY = -1;
            downRange -= 5;
        }
        increaseRange -= 5;
        if (rightRange == 0 || leftRange == 0 || downRange == 0 || upRange == 0)
        {
            ResetDiectionRange();
        }
    }

}