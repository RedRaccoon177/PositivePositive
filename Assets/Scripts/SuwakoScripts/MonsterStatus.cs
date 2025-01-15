using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.RuleTile.TilingRuleOutput;

abstract public class State
{
    virtual public void Enter(SuwakoController suwako) { }
    virtual public void Update(SuwakoController suwako) { }
    virtual public void FixUpdate(SuwakoController suwako) { }
    virtual public void OnCollisionEnter2D(SuwakoController suwako, Collision2D collision) { }
    virtual public void OnTriggerEnter2D(SuwakoController suwako, Collider2D collider) { }
    virtual public void OnTriggerExit2D(SuwakoController suwako, Collider2D collider) { }

    //공용 시간 활용
    public float time { get; set; }

}
