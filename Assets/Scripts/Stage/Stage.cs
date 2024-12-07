using System.Collections;
using UnityEngine;

public class Stage : MonoBehaviour
{
	[Header("�������� �� �� ���� ���� Ƚ�� (���� ����)")]
	[SerializeField] private int[] selectableCount;

	[Header("�������� �� ���� ���� �� �ִ� Ƚ�� (���� ����)")]
	[SerializeField] private int rollCount;

	[Header("�� ���� �˾�")]
	[SerializeField] private GameObject ballSelectPopUp;

	[Header("���� �� �迭")]
	[SerializeField] private int[] starScores;

	[Header("카메라")]
	[SerializeField] private CameraController cameraController;

	private int startPinCount;

	

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

	public void UpdateStartPinCount(int _count)
	{
		startPinCount = _count;
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
