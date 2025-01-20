using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScripts : MonoBehaviour
{
    public Transform target; // �̵��� ��ǥ ����
    public float duration = 5f; // �̵��� �ɸ� �ð�

    void Start()
    {
        StartCoroutine(MoveOverTime(target.position, duration));
    }

    IEnumerator MoveOverTime(Vector3 destination, float time)
    {
        Vector3 start = transform.position; // ���� ��ġ
        float elapsed = 0f; // ��� �ð�

        while (elapsed < time)
        {
            transform.position = Vector3.Lerp(start, destination, elapsed / time); // ���������� �̵�
            elapsed += Time.deltaTime; // ��� �ð� ����
            yield return null; // ���� �����ӱ��� ���
        }

        transform.position = destination; // ��Ȯ�� ���� �������� �̵�
    }
}
