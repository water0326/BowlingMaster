using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
	[SerializeField] private OnArrowEvent onArrowEvent;
	private Vector2 startPosition;
	private Vector2 endPosition;
	private bool isActive;
	
	void OnEnable()
	{
		onArrowEvent.AddListener(OnArrowToggle);
		onArrowEvent.RaiseEvent(false);
	}
	private void OnDisable() {
		onArrowEvent.RemoveListener(OnArrowToggle);
	}
	
	void OnArrowToggle(bool isActive)
	{
		this.isActive = isActive;
		if (isActive)
		{
			startPosition = Input.mousePosition;
			GetComponent<Image>().color = new Color(1, 1, 1, 1);
		}
		else
		{
			GetComponent<Image>().color = new Color(1, 1, 1, 0);
		}
	}
	
	private void Update() {
		if (isActive)
		{
			GetComponent<RectTransform>().position = startPosition;
			endPosition = Input.mousePosition;
			
			float distance = Vector2.Distance(startPosition, endPosition);
			GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, distance * 0.25f);
			Vector2 direction = (endPosition - startPosition).normalized;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0, 0, angle - 90);
		}
	}
}
