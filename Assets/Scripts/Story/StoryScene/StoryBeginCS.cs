using System.Collections;
using TMPro;
using UnityEngine;

public class StoryBeginCS : CutsceneEvent
{
    [SerializeField] private TMP_Text narration;
    [SerializeField] private string displayText;
    [SerializeField] private float typingSpeed = 0.1f; // 텍스트 한 글자당 딜레이 시간 (초 단위)
    [SerializeField] private float delayAfter = 0.5f;
    [SerializeField] private float fadeOutDuration = 1.0f; // 텍스트가 페이드 아웃되는 시간 (초 단위)

    public override IEnumerator Execute()
    {
        narration.text = ""; // 초기 텍스트를 비워줌

        // 텍스트를 한 글자씩 출력하는 루프
        for (int i = 0; i < displayText.Length; i++)
        {
            narration.text += displayText[i]; // 한 글자씩 추가
            yield return new WaitForSeconds(typingSpeed); // 딜레이
        }

        // 모든 텍스트 출력 후 잠시 대기
        yield return new WaitForSeconds(delayAfter);

        // 텍스트 페이드 아웃 시작
        yield return StartCoroutine(FadeOutText());
    }

    // 텍스트 페이드 아웃 코루틴
    private IEnumerator FadeOutText()
    {
        Color originalColor = narration.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            narration.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // 페이드 아웃 후 완전히 투명하게 설정
        narration.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}