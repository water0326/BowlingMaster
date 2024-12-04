using System.Collections;

using UnityEngine;

public class Pin : MonoBehaviour
{
    [Header("핀이 쓰러지고 사라지는데 걸리는 시간")]
    [SerializeField] private float destroyDelay = 1f;

    private PinChecker pinChecker;
    private bool isColl = false;

    private Rigidbody2D rigid;

    private void Start()
    {
        pinChecker = FindObjectOfType<PinChecker>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FallDown();
    }

    public void FallDown()
    {
        if (!isColl)
        {
            isColl = true;

            pinChecker.DecreasePinCount();
            StartCoroutine(DestroyPin());
        }
    }

    IEnumerator DestroyPin()
    {
        yield return new WaitForSeconds(destroyDelay);

        Destroy(gameObject);
    }
}
