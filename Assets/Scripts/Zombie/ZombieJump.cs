
using UnityEngine;

public class ZombieJumpReady : ZombieState
{
    public override void Enter(ZombieController zombie)
    {
        Debug.Log("점프 들어왔음");
        zombie.Animator.SetTrigger("JumpReady");
        zombie.mosterToPlayer = zombie.PlayerInfo.transform.position - zombie.transform.position;
        // 180도 더한 이유 - 유니티 내 좌표계에서 시계방향으로 각도회전을 하는데, 그것을 이용해 내 이미지의 기준인 180도를 더해주었다.
        zombie.transform.rotation = Quaternion.Euler(0, 0, 180 + Mathf.Atan2(zombie.mosterToPlayer.y, zombie.mosterToPlayer.x) * Mathf.Rad2Deg);
        // 벡터 크기 1로
        zombie.mosterToPlayer.Normalize();
        // 레이 캐스트
        zombie.ray2d = Physics2D.Raycast(zombie.transform.position, zombie.mosterToPlayer, zombie.distance, LayerMask.GetMask("Wall"));
        if (zombie.ray2d == true)
        {
            zombie.mapBounds = zombie.ray2d.point;
            Debug.Log(zombie.mapBounds);
        }
        float distance = Vector2.Distance(zombie.mapBounds, zombie.transform.position);
        float length = distance / 10;
        zombie.JumpSkillRange.transform.localScale = new Vector3(distance / 10, 0.3f, 0);
        zombie.JumpSkillRange.transform.position = new Vector2(zombie.transform.position.x + (length * zombie.mosterToPlayer.x * 5), zombie.transform.position.y + (length * zombie.mosterToPlayer.y * 5));
        zombie.JumpSkillRange.SetActive(true);
    }
    public override void Update(ZombieController zombie)
    {
        Debug.DrawRay(zombie.transform.position, zombie.mosterToPlayer * zombie.distance, Color.green);

        if (zombie.jumpOn == true)
        {
            //zombie.jumpOn = false;
            zombie.ChangeState(zombie.jump);
        }
    }

}
public class ZombieJump : ZombieState
{
    public override void Enter(ZombieController zombie)
    {
        zombie.Animator.SetBool("IsJump", true);
        zombie.JumpSkillRange.SetActive(false);
    }
    public override void Update(ZombieController zombie)
    {
        //zombie.Rigid.AddForce(zombie.mosterToPlayer*10);
    }
    public override void FixUpdate(ZombieController zombie)
    {
        //if (zombie.ray2d.point == (Vector2)zombie.transform.position)
        //{
        //}
        zombie.Rigid.velocity = zombie.mosterToPlayer * 30;
        zombie.deltaTime += Time.deltaTime;
        if (zombie.deltaTime > 2)
        {
            Debug.Log(zombie.deltaTime);
            zombie.deltaTime = 0;
            zombie.Animator.SetBool("IsJump", false);
            zombie.ChangeState(zombie.idle);
        }


    }
    public override void OnCollisionEnter2D(ZombieController zombie, Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("닿음");
            // 레이캐스트
            // 감지되었다면!
            // if (zombie.ray2d.collider != null)
            // {
            zombie.Animator.SetBool("IsJump", false);
            zombie.ChangeState(zombie.idle);
            // }
        }
    }

}