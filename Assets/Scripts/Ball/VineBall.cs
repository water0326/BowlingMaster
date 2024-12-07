using UnityEngine;
using System.Collections;

public class VineBall : Ball
{
    [Header("덩굴 프리팹")]
    [SerializeField] private GameObject vinePrefab;

    [Header("덩굴 생성 거리")]
    [SerializeField] private float vineDistance = 1.0f;

    private bool isVine = false;
    private Vector3 lastVinePosition;

    protected override void Update()
    {
        base.Update();
    }

    private void StartVine()
    {
        if (!isVine)
        {
            isVine = true;
            lastVinePosition = transform.position;
            
            // 덩굴을 즉시 �성
            GameObject vine = Instantiate(vinePrefab, transform.position, Quaternion.identity);
            vine.transform.up = rb.velocity.normalized;
            
            StartCoroutine(VineCoroutine());
        }
    }

    private void StopVine()
    {
        isVine = false;
    }

    IEnumerator VineCoroutine()
    {
        while (isVine)
        {
            float distanceMoved = Vector3.Distance(lastVinePosition, transform.position);
            if (distanceMoved >= vineDistance)
            {
                GameObject vine = Instantiate(vinePrefab, transform.position, Quaternion.identity);
                vine.transform.up = rb.velocity.normalized;
                lastVinePosition = transform.position;
            }
            yield return null; // 매 프레임마다 체크
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Precipice")) StartVine();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Precipice")) StopVine();
    }
}
