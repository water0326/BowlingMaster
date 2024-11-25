using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatChatAndMovingCS : CutsceneEvent
{
	private Animator animator;

	private void Awake()
	{
		animator = target.GetComponent<Animator>();
	}
	
	[SerializeField] private Vector2 startPos;
	[SerializeField] private Vector2 endPos;
	[SerializeField] private float dialogDuration;
	[SerializeField] private float moveDuration;
	[SerializeField] private string text;
	[SerializeField] private Dialog dialog;
	[SerializeField] private Transform target;
	[SerializeField] private Vector2 textPosOffset;

	public override IEnumerator Execute()
	{
		float elapsedTime = 0f;
		Vector2 currentPos = startPos;
		animator.SetBool("isWalking", true);

		// Start dialog animation
		StartCoroutine(dialog.AnimateText(text, target, textPosOffset, dialogDuration));

		// Move from start to end position
		while (elapsedTime < moveDuration)
		{
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / moveDuration;
			currentPos = Vector2.Lerp(startPos, endPos, t);
			target.transform.position = currentPos;
			yield return null;
		}

		// Ensure we end exactly at the target position
		target.transform.position = endPos;
		animator.SetBool("isWalking", false);
	}

	private void OnDrawGizmosSelected()
	{
		// Draw line between start and end positions
		Gizmos.color = Color.green;
		Gizmos.DrawLine(startPos, endPos);

		// Draw spheres at start and end positions
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(startPos, 0.2f);
		
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(endPos, 0.2f);

		// Draw text offset position
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere((Vector3)textPosOffset + target.position, 0.1f);
	}
}
