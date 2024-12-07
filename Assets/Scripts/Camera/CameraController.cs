using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
	private bool cameraMove = false;
	public bool isReady = false;

	private float distance;

	[SerializeField] private Transform target;
	[SerializeField] private GameObject finishLine;
	[SerializeField] private float animationDuration = 2f;
	
	[SerializeField] private StageTitleDisplay stageTitleDisplay;


	private void Update()
	{
		if (cameraMove)
		{
			transform.position = new Vector3(transform.position.x, target.position.y + distance, transform.position.z);
		}
	}

	public void Init(Transform _target)
	{
		target = _target;

		distance = transform.position.y - target.position.y;
	}
	
	private void Start() {
		
		Vector3 startPos = finishLine.transform.position;
		startPos.z = transform.position.z;
		transform.position = startPos;
		
		StartCoroutine(OverviewAnimation());
	}
	
	private IEnumerator OverviewAnimation() {
		
		stageTitleDisplay.SetStageTitle(SceneManager.GetActiveScene().name);
		stageTitleDisplay.FadeIn();
		yield return new WaitForSeconds(1.5f);
		stageTitleDisplay.FadeOut();
		
		Vector3 startPos = finishLine.transform.position;
		startPos.z = transform.position.z;
		transform.position = startPos;
		float elapsedTime = 0f;
		Vector3 endPos = new Vector3(0f, 0f, transform.position.z);
		
		while (elapsedTime < animationDuration) {
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / animationDuration;
			transform.position = Vector3.Lerp(startPos, endPos, t);
			yield return null;
		}
		transform.position = endPos;
		
		yield return new WaitForSeconds(1f);
		
		isReady = true;
	}

	public void StartMove()
	{
		cameraMove = true;
	}

	public void UndoMove()
	{
		cameraMove = false;

		transform.position = new Vector3(0f, 0f, transform.position.z);
	}
}
