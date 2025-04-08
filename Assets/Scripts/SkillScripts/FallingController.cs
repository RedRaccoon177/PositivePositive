using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingController : MonoBehaviour
{
    //▼ 현재 생성된 떨어지는 스킬 개수를 추적하는 변수
    private static int currentFallingSkills = 0;
    //▼ 최대 생성 가능한 떨어지는 스킬 개수
    public static int maxFallingSkills = 5;
    //▼ 최소 생성 가능한 떨어지는 스킬 개수
    public static int minFallingSkills = 3;
    //▼ x,y.z 담을 tempPosition 선언
    Vector3 tempPosition; 
    //▼ 떨어지는 속도 선언
    float moveSpeed;
    float startLine;

    void Start()
    {
        //▼ 현재 스킬 개수가 최대 개수를 넘지 않았을 경우에만 생성
        if (currentFallingSkills < maxFallingSkills)
        {
            //▼ 떨어지는 스킬 개수 증가
            currentFallingSkills++;

            //▼ transform.position을 tempPosition에 담기
            tempPosition = transform.position;
            //▼ 떨어지는 속도를 랜덤값으로 지정
            moveSpeed = Random.Range(0.2f, 0.2f);
            //▼ 시작 라인 랜덤값으로 지정
            startLine = Random.Range(11.0f, 12.0f);
            tempPosition.y = startLine;
            //▼ x좌표 랜덤값으로 지정
            tempPosition.x = Random.Range(-8.2f, 11.0f);
            //▼ 위치 업데이트
            transform.position = tempPosition;
        }
        else
        {
            // 최대 개수를 초과하면 오브젝트를 파괴
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //▼ 충돌한 객체가 벽일 경우
        if (collision.gameObject.tag == "Wall")  
        {
            //▼ 떨어지는 스킬 개수 감소
            currentFallingSkills--;
            //▼ 객체 파괴
            Destroy(gameObject);
            
        }
    }
    private void FixedUpdate()
    {
        //▼ moveSpeed를 -1 씩 tempPosition.y에 반복해서 넣기
        tempPosition.y -= moveSpeed;

        //▼ y가 -6보다 작아지면
        if (tempPosition.y < -6) 
        {
            //▼ y에 +30 더하기
            tempPosition.y += 18;
            //▼ x좌표를 랜던값으로 지정하여 반복
            tempPosition.x = Random.Range(-8.2f, 11.0f); 
            //▼ moveSpeed를 랜덤값으로 지정해서 반복
            moveSpeed = Random.Range(0.2f, 0.2f); 
        }
        //▼ tempPosition을 transform.position에 담아주기
        transform.position = tempPosition;
    }
    
    void Update()
    {
       
    }
}
