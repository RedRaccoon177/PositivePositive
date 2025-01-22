using UnityEngine;

public class SlugBoss : MonoBehaviour
{
    //�� ������Ʈ ������
    public GameObject fallskill; // �������� ��ų ������
    public GameObject skillPrefab; // �߻�ü ������ 
    public GameObject wideareaPreafab; // ��ü ������

    //�� �߻� ��ġ
    public Transform firePointFront1; // ���� �߻� ��ġ1
    public Transform firePointFront2; // ���� �߻� ��ġ2
    public Transform firePointFront3; // ���� �߻� ��ġ3
    public Transform firePointUp1; // ���� �߻� ��ġ1
    public Transform firePointUp2; // ���� �߻� ��ġ2
    public Transform firePointUp3; // ���� �߻� ��ġ3
    public Transform spawnPoint; // ��ü�� �����Ǵ� ��ġ

    //�� Ÿ�̸ӿ� ��Ÿ��
    public float fireInterval = 5f; // �߻� ���� (�� ����) 
    private float coolTime; // ��ų ��Ÿ���� ����
    private float fireTimer; // �߻� ���� �ӵ�

    //�� �߻� ���� ����
    private float delaySpeed; // �߻� ���� �ӵ�
    private float count; // �߻� Ƚ��
    private bool isHorizontalAttack = true; // ���� ���� ����

    //�� ���� ���� ����
    public int maxFallingSkills = 10;  // �ִ� �������� ��ų ����
    public float slugBossHealth = 100f; // ������ ü��
    public float slugAttackDamage = 10f; // ������ ���� ������



    void Start()
    {
        coolTime = 0f; // ��ų ��Ÿ�� �ʱ�ȭ
        fireTimer = 0f; // �߻� Ÿ�̸� �ʱ�ȭ
    }

    void Update()
    {
        if (slugBossHealth <= 0)
        {
           ActivateWideAreaSkill();// ���� ü���� 0 ������ �� ��ü ��ų �ߵ�
           return;
        }

        coolTime += Time.deltaTime; // ��ų ��Ÿ�� ����

        if (coolTime > 3.5f) // ��Ÿ���� ������ �������� ��ų ����
        {
            for(int i = 0; i <3; i++)
            {
              Instantiate(fallskill); // �������� ��ų 3�� ���� 
            }
            coolTime = 0; // ��Ÿ�� �ʱ�ȭ
        }
        // 0 - �����ð�
        fireTimer -= Time.deltaTime; // �߻� Ÿ�̸� ����

        if (fireTimer <= 0f) // �߻� Ÿ�̸Ӱ� 0 ������ ���
        {
            if (count < 8) // �߻� Ƚ���� 8���� ���� ���
            {
                delaySpeed -= Time.deltaTime; // �߻� ���� �ӵ� ����

                if (delaySpeed < -0.1f) // �߻� ���� �ӵ��� -0.1f ������ ���
                {
                    count++; // �߻� Ƚ�� ����
                    delaySpeed = 0f; // �߻� ���� �ӵ� �ʱ�ȭ
                    PerformAttack(); // ���� ����
                }
            }
            else
            {
                count = 0; // �߻� Ƚ�� �ʱ�ȭ
                fireTimer = fireInterval; // �߻� Ÿ�̸� �ʱ�ȭ 
                isHorizontalAttack = !isHorizontalAttack; // ���� ���� ����
            }
        }
    }

    void PerformAttack()
    {
        if (isHorizontalAttack)
        {
            // ���� ����: ���� �� ���� �߻�
            Instantiate(skillPrefab, firePointFront1.position, transform.rotation); // ���� �߻�
            Instantiate(skillPrefab, firePointUp1.position, firePointUp1.rotation); // ���� �߻�
            Instantiate(skillPrefab, firePointFront3.position, transform.rotation); // ���� �߻�
        }
        else
        {
            // ���� ����: ���� �� ���� �߻�
            Instantiate(skillPrefab, firePointUp2.position, firePointUp2.rotation); // ���� �߻�
            Instantiate(skillPrefab, firePointFront2.position, transform.rotation); // ���� �߻�
            Instantiate(skillPrefab, firePointUp3.position, firePointUp3.rotation); // ���� �߻�
        }
    }

    void ActivateWideAreaSkill()
    {
        //�� ��ü ��ų ����
        Instantiate(wideareaPreafab, spawnPoint.position, Quaternion.identity);

        CancelInvoke("PerformAttack");
        CancelInvoke("Update");
    }
    public void TakeDamage(float damage)
    {
        slugBossHealth -= damage; // �������� ������ ü�� ����
        if (slugBossHealth <= 0)
        {
            slugBossHealth = 0;  // ü���� 0 ���ϰ� �Ǹ�
            ActivateWideAreaSkill(); // ��ü ��ų �ߵ�
        }
    }
}
