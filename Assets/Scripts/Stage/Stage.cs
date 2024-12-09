using System.Collections;
using UnityEngine;

public class Stage : MonoBehaviour
{
	[Header("공 선택 가능 갯수")]
	[SerializeField] private int[] selectableCount;

	[Header("공 굴리는 횟수")]
	[SerializeField] private int rollCount;

	[Header("공 선택 횟수")]
	[SerializeField] private GameObject ballSelectPopUp;

	[Header("별점")]
	[SerializeField] private int[] starScores;

	[Header("카메라")]
	[SerializeField] private CameraController cameraController;

	[Header("Score Script")]
	[SerializeField] private Score score;

	private int startPinCount;

	private void Awake()
	{
		score = FindObjectOfType<Score>();
        starScores = new int[3];
    }

	private void Start()
	{
		StartCoroutine(WaitForCamera());
	}

	private IEnumerator WaitForCamera() {
		yield return new WaitUntil(() => cameraController.isReady);
		Instantiate(ballSelectPopUp);
	}

	public int GetSelectableCount(int _index)
	{
		return selectableCount[_index];
	}

	public int GetRollCount()
	{
		return rollCount;
	}

	public int GetStarScore(int _index)
	{
		return starScores[_index];
	}

	public void UpdateStartPinCountAndScore(int _count)
	{
		startPinCount = _count;

        //최고점
        starScores[2] = score.GetPinScore() * startPinCount;
        starScores[1] = (int)(score.GetPinScore() * startPinCount * 0.5);
        starScores[0] = (int)(score.GetPinScore() * startPinCount * 0.1);
	}

	public int GetStartPinCount()
	{
		return startPinCount;
	}

	public int GetStarCount()
	{
		return starScores.Length;
	}
}
