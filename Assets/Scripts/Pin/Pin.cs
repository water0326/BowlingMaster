using System.Collections;

using UnityEngine;

public class Pin : MonoBehaviour
{
    [Header("핀이 쓰러지고 사라지는데 걸리는 시간")]
    [SerializeField] private float destroyDelay = 1f;

    private PinChecker pinChecker;
    private bool isColl = false;

    private void Start()
    {
        pinChecker = FindObjectOfType<PinChecker>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isColl)
        {
            isColl = true;
            pinChecker.DecreasePinCount();
        }

        StartCoroutine(DestroyPin());
    }

    IEnumerator DestroyPin()
    {
        yield return new WaitForSeconds(destroyDelay);

        Destroy(gameObject);
    }
}
