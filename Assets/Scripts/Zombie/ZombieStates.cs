using UnityEngine;

// 모든 상태가 State를 들고 있지만 , 하나의 State를 공유하는것이 아닌 각각의 State를 들고 있음
abstract public class ZombieState
{
    abstract public void Enter(ZombieController zombie);
    virtual public void OnCollisionEnter2D(ZombieController zombie, Collision2D collision) { }
    virtual public void Update(ZombieController zombie) { }
    virtual public void FixUpdate(ZombieController zombie) { }

}





