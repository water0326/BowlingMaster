using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
	
	[SerializeField] GameObject dialogObject;
	[SerializeField] TMP_Text dialogText;
	[SerializeField] Transform target;
	[SerializeField] float totalTime = 1f;
	private Vector2 textPosOffset;

	public void MoveToTarget()
	{
		if (dialogObject == null) return;
		
		// Get the RectTransform of the dialog
		RectTransform rectTransform = dialogObject.GetComponent<RectTransform>();
		
		// Convert world position to screen position
		Vector2 screenPoint = Camera.main.WorldToScreenPoint(target.position + (Vector3)textPosOffset);
		
		// Set the position
		rectTransform.position = screenPoint;
	}
	void Update()
	{
		if(target != null)
		{
			dialogObject.SetActive(true);
			MoveToTarget();
		}
		else {
			dialogObject.SetActive(false);
		}
	}

	public IEnumerator AnimateText(string text, Transform target, Vector2 textPosOffset, float duration)
	{
		// Replace literal "\n" with actual newline character
		text = text.Replace("\\n", "\n");

		if(duration < totalTime) totalTime = duration - 0.2f;	
		Invoke(nameof(StopDisplay), duration);
		this.target = target;
		this.textPosOffset = textPosOffset;
		dialogObject.SetActive(true);
		
		dialogText.text = "";
		// text를 \n을 기준으로 분리하여 처리
		string[] lines = text.Split('\n');
		foreach (string line in lines)
		{
			foreach (char letter in line.ToCharArray())
			{
				float intervalTime = totalTime / text.Length;
				dialogText.text += letter;
				yield return new WaitForSeconds(intervalTime);
			}
			// 줄바꿈 추가
			if (line != lines[lines.Length - 1]) // 마지막 줄이 아닌 경우에만 줄바꿈 추가
			{
				dialogText.text += "\n";
			}
		}
	}
	public void StopDisplay() {
		this.target = null;
	}
}
