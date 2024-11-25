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
		if(duration < totalTime) totalTime = duration - 0.2f;
		Invoke(nameof(StopDisplay), duration);
		this.target = target;
		dialogText.text = text;
		this.textPosOffset = textPosOffset;
		dialogObject.SetActive(true);
		
		dialogText.text = "";
		foreach (char letter in text.ToCharArray())
		{
			// Using the totalTime field defined at class level
			float intervalTime = totalTime / text.Length; // Time between each letter
			dialogText.text += letter;
			yield return new WaitForSeconds(intervalTime);
		}
	}
	public void StopDisplay() {
		this.target = null;
	}
}
