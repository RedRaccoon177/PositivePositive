using Unity.VisualScripting;
using UnityEngine;

public class SlugBoss : MonoBehaviour
{
    //▼ 오브젝트 프리팹
    public GameObject fallskill; // 떨어지는 스킬 프리팹
    public GameObject skillPrefab; // 발사체 프리팹 
    public GameObject wideareaPreafab; // 구체 프리팹
    public Animator animator;

    //▼ 발사 위치
    public Transform firePointFront1; // 정면 발사 위치1
    public Transform firePointFront2; // 정면 발사 위치2
    public Transform firePointFront3; // 정면 발사 위치3
    public Transform firePointUp1; // 위쪽 발사 위치1
    public Transform firePointUp2; // 위쪽 발사 위치2
    public Transform firePointUp3; // 위쪽 발사 위치3
    public Transform spawnPoint; // 구체가 생성되는 위치

    //▼ 타이머와 쿨타임
    public float fireInterval = 10f; // 발사 간격 (초 단위) 
    private float coolTime; // 스킬 쿨타임을 추적
    private float fireTimer; // 발사 지연 속도

    //▼ 발사 관련 변수
    private float delaySpeed; // 발사 지연 속도
    private float count; // 발사 횟수
    private bool isHorizontalAttack = true; // 수평 공격 여부

    //▼ 보스 상태 변수
    public bool MonsterDeath;
    public int maxFallingSkills = 10;  // 최대 떨어지는 스킬 개수
    public float slugBossHealth = 100f; // 보스의 체력
    float slugBossMax = 100;
    public float slugAttackDamage = 10f; // 보스의 공격 데미지
    private bool isWideAreaSkillActivated = false; // 구체 스킬이 활성화 되었는지
    int damage = 10;

    private MonsterHPObserver hpObserver; // MonsterHPObserver 객체

    bool isFire;

    void Start()
    {
        animator = GetComponent<Animator>();
        coolTime = 0f; // 스킬 쿨타임 초기화
        fireTimer = 0f; // 발사 타이머 초기화
        isFire = false;

        // hpObserver 초기화
        hpObserver = GetComponent<MonsterHPObserver>();

        // 체력 변화를 옵저버에게 알림
        if(hpObserver != null)
        {
            hpObserver.NotifyHealthChange(100f, slugBossHealth);
        }
       
    }

    void Update()
    {
        if (slugBossHealth <=0 && !isWideAreaSkillActivated)
        {
            ActivateWideAreaSkill();// 보스 체력이 0 이하일 때 구체 스킬 발동
            isWideAreaSkillActivated = true; // 구체 스킬 활성화 상태로 설정
            return;
        }

        coolTime += Time.deltaTime; // 스킬 쿨타임 증가

        if (coolTime > 3.5f) // 쿨타임이 지나면 떨어지는 스킬 생성
        {
            for(int i = 0; i <3; i++)
            {
              Instantiate(fallskill); // 떨어지는 스킬 3개 생성 
            }
            coolTime = 0; // 쿨타임 초기화
        }
        // 0 - 실제시간
        fireTimer -= Time.deltaTime; // 발사 타이머 감소

        if (fireTimer <= 0f) // 발사 타이머가 0 이하일 경우
        {
            if (count < 8) // 발사 횟수가 8보다 적을 경우
            {
                delaySpeed -= Time.deltaTime; // 발사 지연 속도 감소

                if (delaySpeed < -0.1f) // 발사 지연 속도가 -0.1f 이하일 경우
                {
                    count++; // 발사 횟수 증가
                    delaySpeed = 0f; // 발사 지연 속도 초기화
                    PerformAttack(); // 공격 수행
                }
            }
            else
            {
                count = 0; // 발사 횟수 초기화
                fireTimer = fireInterval; // 발사 타이머 초기화 
                isHorizontalAttack = !isHorizontalAttack; // 공격 방향 변경
            }
        }
    }

    void PerformAttack()
    {
        if (isFire == false)
        {


            if (isHorizontalAttack)
            {
                // 수평 공격: 정면 및 위쪽 발사
                Instantiate(skillPrefab, firePointFront1.position, transform.rotation); // 정면 발사
                Instantiate(skillPrefab, firePointUp1.position, firePointUp1.rotation); // 위쪽 발사
                Instantiate(skillPrefab, firePointFront3.position, transform.rotation); // 정면 발사
            }
            else
            {
                // 위쪽 공격: 정면 및 위쪽 발사
                Instantiate(skillPrefab, firePointUp2.position, firePointUp2.rotation); // 위쪽 발사
                Instantiate(skillPrefab, firePointFront2.position, transform.rotation); // 정면 발사
                Instantiate(skillPrefab, firePointUp3.position, firePointUp3.rotation); // 위쪽 발사
            }
        }
    }

    void ActivateWideAreaSkill()
    {
        //▼ 구체 스킬 생성
        Instantiate(wideareaPreafab, spawnPoint.position, Quaternion.identity);

        CancelInvoke("PerformAttack");
        CancelInvoke("Update");
    }
    public void TakeDamage()
    {
        slugBossHealth -= damage; // 데미지를 받으면 체력 감소

        if (hpObserver != null)
        {
            hpObserver.NotifyHealthChange(slugBossMax, slugBossHealth);
        }

        if (slugBossHealth <= 0 && !isWideAreaSkillActivated)
        {
            slugBossHealth = 0;  // 체력이 0 이하가 되면
            animator.SetTrigger("MonsterDeath");
            ActivateWideAreaSkill(); // 구체 스킬 발동
            isWideAreaSkillActivated = true; // 구체 스킬 활성화 상태로 설정
            isFire = true;
        }
        GetComponent<MonsterHPObserver>().NotifyHealthChange(slugBossMax, slugBossHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Player")
        {
            TakeDamage();
        }
    }
}
