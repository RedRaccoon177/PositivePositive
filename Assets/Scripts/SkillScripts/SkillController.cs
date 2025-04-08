using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillController : MonoBehaviour
{
    public float speedPower = -10; // 스킬 속도
    public float damage = 10f;    // 스킬의 데미지
    public float delayTime = 10f; // 스킬 이동 딜레이 시간
    public bool isMoving = false; // 스킬이 이동 중인지 여부

    void Start()
    {
        //딜레이 후 스킬 이동 시작
        StartCoroutine(DelayMovement());
    }

    void Update()
    {   
        transform.Translate(0.08f, 0, 0); // 스킬 이동

        if (transform.position.x < -10)
        {
            Destroy(gameObject); // 스킬이 화면 밖으로 나가면 옵젝 파괴
        }
    }

    IEnumerator DelayMovement()
    {
        yield return new WaitForSeconds(delayTime);
        isMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
        
