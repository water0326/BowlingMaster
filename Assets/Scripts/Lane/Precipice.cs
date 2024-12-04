using System.Collections;
using UnityEngine;

public class Precipice : MonoBehaviour
{
    [Header("크기가 감소하는 데 걸리는 시간")]
    [SerializeField] private float shrinkDuration = 2f;

    [Header("이동 속도 감소 비율")]
    [SerializeField] private float decreaseSpeedRate = 0.05f;

    [Header("라운드 종료를 관리하는 FinishLine 컴포넌트")]
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

        StartCoroutine(Shrink(collision));
    }

    IEnumerator Shrink(Collider2D collision)
    {
        Vector3 originalScale = collision.transform.localScale;
        float currentTime = 0f;

        while (currentTime < shrinkDuration)
        {
            // 시간에 따라 크기 감소
            collision.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, currentTime / shrinkDuration);
            currentTime += Time.deltaTime;
            yield return null; 
        }

        // 크기를 완전히 0으로 설정
        collision.transform.localScale = Vector3.zero;

        if (collision.CompareTag("Ball")) finishLine.DeadBall(collision);

        StopAllCoroutines();
    }
}
