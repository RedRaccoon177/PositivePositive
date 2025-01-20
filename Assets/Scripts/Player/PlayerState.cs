using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MoveLeft
{
    public void MoveLeft(Player player);
}
public interface MoveRight
{
    public void MoveRight(Player player);
}

public interface Shift
{
    public void Shift(Player player);
}

public interface Space
{
    public void Space(Player player);
}

public class GroundMoveLeft : MoveLeft
{
    public void MoveLeft(Player player)
    {
        if ((player.isWall && player.facingRight == -1) == false)
        {
            player.SetAnimState("IsWalk", true);
            if (player.Rigid.velocity.x < -player.groundMoveSpeed)
            {
                player.Rigid.velocity = new Vector2(player.Rigid.velocity.x + Time.deltaTime / 2, player.Rigid.velocity.y);
            }
            else
            {
                player.Rigid.velocity = new Vector2(-player.groundMoveSpeed, player.Rigid.velocity.y);
            }
        }
    }
}

public class GroundMoveRight : MoveRight
{
    public void MoveRight(Player player)
    {
        if ((player.isWall && player.facingRight == 1) == false)
        {
            player.SetAnimState("IsWalk", true);
            if (player.Rigid.velocity.x > player.groundMoveSpeed)
            {
                player.Rigid.velocity = new Vector2(player.Rigid.velocity.x - Time.deltaTime / 2, player.Rigid.velocity.y);
            }
            else
            {
                player.Rigid.velocity = new Vector2(player.groundMoveSpeed, player.Rigid.velocity.y);
            }
        }
    }
}

public class GroundShift : Shift
{
    public void Shift(Player player)
    {
        //아무기능없음
    }
}

public class GroundSpace : Space
{
    public void Space(Player player)
    {
        if (player.isJump == false)
        {
            player.isJump = true;
            if (player.isWall == false)
            {
                player.Rigid.AddForce(Vector3.up * player.jumpPower, ForceMode2D.Impulse);
            }
            else
            {
                player.StartCoroutine(player.WallJump());
                player.Rigid.velocity = Vector2.zero;
                player.Rigid.AddForce(new Vector2(-player.facingRight * player.groundMoveSpeed, player.jumpPower), ForceMode2D.Impulse);
            }
        }
    }
}

public class RopeMoveLeft : MoveLeft
{
    public void MoveLeft(Player player)
    {
        if (player.Rigid.velocity.x > 0)
        {
            player.Rigid.velocity = new Vector2(-player.Rigid.velocity.x, player.Rigid.velocity.y);
        }
        player.Rigid.AddForce(new Vector2(-player.swingSpeed, 0));
    }
}


public class RopeMoveRight : MoveRight
{
    public void MoveRight(Player player)
    {
        if (player.Rigid.velocity.x < 0)
        {
            player.Rigid.velocity = new Vector2(-player.Rigid.velocity.x, player.Rigid.velocity.y);
        }
        player.Rigid.AddForce(new Vector2(player.swingSpeed, 0));
    }
}

public class RopeShift : Shift
{
    public void Shift(Player player)
    {
        if (player.GetCanBoost() == true)
        {
            player.SetBoost(false);
            if (player.Rigid.velocity.x > 0)
            {
                player.Rigid.AddForce(new Vector2(30, 0), ForceMode2D.Impulse);
            }
            else
            {
                player.Rigid.AddForce(new Vector2(-30, 0), ForceMode2D.Impulse);
            }
        }
    }
}

public class RopeSpace : Space
{
    public void Space(Player player)
    {
        //돌진구현하기
        Debug.Log("Charge");
        if (player.GetCanCharge() == true)
        {
            if (player.throwHook.GetCurHook()?.GetComponent<RopeScript>().lastNode.GetComponent<HingeJoint2D>().connectedBody != null)
            {
                player.throwHook.GetCurHook().GetComponent<RopeScript>().lastNode.GetComponent<HingeJoint2D>().connectedBody = null;
            }
            player.SetCanCharge(false);
            player.StartCoroutine(player.HookCharge(player.throwHook.transform.position, player.throwHook.GetCurHook().transform.position));
        }
    }
}

public class PlayerActs
{
    public MoveLeft moveLeftStrategy;
    public MoveRight moveRightStrategy;
    public Shift shiftStrategy;
    public Space spaceStrategy;

    public void MoveLeftAct(Player player)
    {
        moveLeftStrategy.MoveLeft(player);
    }

    public void MoveRightAct(Player player)
    {
        moveRightStrategy.MoveRight(player);
    }

    public void ShiftAct(Player player)
    {
        shiftStrategy.Shift(player);
    }

    public void SpaceAct(Player player)
    {
        spaceStrategy.Space(player);
    }
}

public class GroundActs : PlayerActs
{
    public GroundActs()
    {
        moveLeftStrategy = new GroundMoveLeft();
        moveRightStrategy = new GroundMoveRight();
        shiftStrategy = new GroundShift();
        spaceStrategy = new GroundSpace();
    }
}

public class RopeActs : PlayerActs
{
    public RopeActs()
    {
        moveLeftStrategy = new RopeMoveLeft();
        moveRightStrategy = new RopeMoveRight();
        shiftStrategy = new RopeShift();
        spaceStrategy = new RopeSpace();
    }
}