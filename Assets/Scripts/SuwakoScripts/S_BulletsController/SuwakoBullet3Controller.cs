using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SuwakoBullet3Controller : MonoBehaviour
{
    int bulletNum = 3; // 풀링에서 사용할 총알 번호
    SuwakoBulletPool pool; // 총알 풀
    string[] excludedTags = { "Bullet", "Enemy", "Untagged", "Wall", "Ground", "OutSideWall" };

    int bouns; // 반사 횟수 제한
    Rigidbody2D rb;

    private void Start()
    {
        bouns = 3; // 반사 횟수 초기화
        pool = SuwakoBulletPool.bulletPoolInstace; // 풀 초기화
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!excludedTags.Contains(collision.tag))
        {
            OnBulletDestroy(gameObject);
            return;
        }

        // 충돌 처리
        if ((collision.tag == "Ground" || collision.tag == "OutSideWall") && bouns > 0)
        {
            // 충돌 표면의 법선 벡터 계산
            Vector2 collisionNormal = (transform.position - collision.transform.position).normalized;

            // 이동 방향 반사 계산
            rb.velocity = -rb.velocity;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, gameObject.transform.rotation.eulerAngles.z + 180);

            bouns--; // 반사 횟수 감소
        }

        if ((collision.tag == "Ground" || collision.tag == "OutSideWall") && bouns <= 0)
        {
            bouns = 3; // 반사 횟수 초기화
            OnBulletDestroy(gameObject);
        }
    }

    public void OnBulletDestroy(GameObject bullet)
    {
        // 오브젝트 풀로 반환
        pool.ReturnObject(bullet, bulletNum);
    }
}
