using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingBallCS : CutsceneEvent
{
	[SerializeField] private GameObject ball;
	[SerializeField] private Vector2 startPos;
	[SerializeField] private Vector2 endPos;
	[SerializeField] private float throwDuration;
	
	public override IEnumerator Execute()
	{
		ball.SetActive(true);
		ball.transform.position = startPos;
		float time = 0;
		while (time < throwDuration)
		{
			time += Time.deltaTime;
			ball.transform.position = Vector2.Lerp(startPos, endPos, time / throwDuration);
			yield return null;
		}
		ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-300, 0));
		ball.GetComponent<SpriteRenderer>().sortingLayerName = "Object";
	}
	
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(startPos, endPos);
	}
}
