using System.Collections;
using UnityEngine;

public class Precipice : MonoBehaviour
{
    [Header("ũ�Ⱑ �����ϴ� �� �ɸ��� �ð�")]
    [SerializeField] private float shrinkDuration = 2f;

    [Header("�̵� �ӵ� ���� ����")]
    [SerializeField] private float decreaseSpeedRate = 0.05f;

    [Header("���� ���Ḧ �����ϴ� FinishLine ������Ʈ")]
    [SerializeField] private FinishLine finishLine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Exit Condition 1 : Vine and Trigger
        if (collision.tag.Contains("Vine")|| collision.isTrigger) return;

        Ball ball = collision.GetComponent<Ball>();

        //Exit Condition 2 : Ball on Vine
        if (ball != null) if (ball.onVine) return;

        Rigidbody2D rigid = collision.GetComponent<Rigidbody2D>();

        if (rigid != null)
        {
            rigid.velocity *= decreaseSpeedRate;
        }

        if (!IsInvoking("Shrink")) StartCoroutine(Shrink(collision));
    }

    IEnumerator Shrink(Collider2D collision)
    {
        Vector3 originalScale = collision.transform.localScale;
        float currentTime = 0f;

        while (currentTime < shrinkDuration)
        {
            // �ð��� ���� ũ�� ����
            collision.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, currentTime / shrinkDuration);
            currentTime += Time.deltaTime;
            yield return null; 
        }

        // ũ�⸦ ������ 0���� ����
        collision.transform.localScale = Vector3.zero;
        if(finishLine == null) 
        {
        	finishLine = FindObjectOfType<FinishLine>();
        }
        if (collision.CompareTag("Ball")) finishLine.DeadBall(collision);

        StopAllCoroutines();
    }
}
