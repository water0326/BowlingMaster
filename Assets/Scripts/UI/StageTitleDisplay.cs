using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageTitleDisplay : MonoBehaviour
{
	private TMP_Text stageTitle;
	
	private void Awake() {
		stageTitle = GetComponent<TMP_Text>();
	}
	
	public void SetStageTitle(string _title) {
		stageTitle.text = _title;
	}
	
	public void FadeIn() 
	{
		StartCoroutine(FadeInTitle());
	}
	
	public void FadeOut() {
		StartCoroutine(FadeOutTitle());
	}

	private IEnumerator FadeInTitle()
	{
		stageTitle.alpha = 0f;
		float elapsedTime = 0f;
		float fadeDuration = 1f;
		
		while (elapsedTime < fadeDuration)
		{
			stageTitle.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		
		stageTitle.alpha = 1f;
		}
	
	private IEnumerator FadeOutTitle()
	{
		stageTitle.alpha = 1f;
		float elapsedTime = 0f;
		float fadeDuration = 1f;
		
		while (elapsedTime < fadeDuration)
		{
			stageTitle.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		
		stageTitle.alpha = 0f;
	}
}
