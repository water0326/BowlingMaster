using UnityEngine;

public class Cloud : MonoBehaviour
{
    
    [SerializeField] private float moveRange = 2.0f; // 이동 범위 (위아래)
    [SerializeField] private float speed = 1.0f;     // 이동 속도
    [SerializeField] private float offset = 0f;     // 이동 속도

    private Vector3 startPosition; // 구름의 초기 위치
    private float moveDirection = 1.0f; // 구름이 움직일 방향 (위/아래)

    void Start()
    {
        // 구름의 초기 위치 저장
        startPosition = transform.position;
    }

    void Update()
    {
        // 위아래로 움직이는 구름의 위치 계산
        float newYPosition = startPosition.y + Mathf.Sin(Time.time * speed + offset) * moveRange;

        // 새로운 위치 적용
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }
}
