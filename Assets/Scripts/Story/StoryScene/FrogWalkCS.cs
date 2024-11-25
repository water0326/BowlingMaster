using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogWalkCS : CutsceneEvent
{
	[SerializeField] private Transform frog;
	[SerializeField] private Animator animator;
	[SerializeField] private Vector2 startPos;
	[SerializeField] private Vector2 endPos;
	[SerializeField] private float walkDuration;
	
	public override IEnumerator Execute()
	{
		float time = 0;
		animator.SetBool("isWalking", true);
		while(time < walkDuration)
		{
			time += Time.deltaTime;
			frog.position = Vector2.Lerp(startPos, endPos, time / walkDuration);
			yield return null;
		}
		animator.SetBool("isWalking", false);
	}
	
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(startPos, endPos);
	}
	
}
