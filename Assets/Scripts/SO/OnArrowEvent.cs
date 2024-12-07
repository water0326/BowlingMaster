using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "OnArrowEvent", menuName = "Event/OnArrowEvent")]
public class OnArrowEvent : ScriptableObject
{
	public UnityEvent<Vector2> onArrowMove;
	public UnityEvent<bool> onArrowToggle;

	public void RaiseEvent(Vector2 position)
	{
		if (onArrowMove != null)
		{
			onArrowMove.Invoke(position);
		}
	}
	public void RaiseEvent(bool isActive)
	{
		if (onArrowToggle != null)
		{
			onArrowToggle.Invoke(isActive);
		}
	}

	public void AddListener(UnityAction<Vector2> listener)
	{
		if (onArrowMove != null)
		{
			onArrowMove.AddListener(listener);
		}
	}
	public void AddListener(UnityAction<bool> listener)
	{
		if (onArrowToggle != null)
		{
			onArrowToggle.AddListener(listener);
		}
	}
	

	public void RemoveListener(UnityAction<Vector2> listener)
	{
		if (onArrowMove != null)
		{
			onArrowMove.RemoveListener(listener);
		}
	}
	public void RemoveListener(UnityAction<bool> listener)
	{
		if (onArrowToggle != null)
		{
			onArrowToggle.RemoveListener(listener);
		}
	}
}
