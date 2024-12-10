using System.Collections;

using UnityEngine;

public class Pin : MonoBehaviour
{
	[Header("���� �������� ������µ� �ɸ��� �ð�")]
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
			SoundManager.Instance.PlaySFXFromPath("Audio/SFX/PinFall");

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
