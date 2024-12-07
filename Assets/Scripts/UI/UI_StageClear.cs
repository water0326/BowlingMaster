//Unity
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class UI_StageClear : MonoBehaviour
{
	[SerializeField] private Button nextStage;
	[SerializeField] private Button restart;
	[SerializeField] private Button main;

	[Header("�� �� �̹��� �迭")]
	[SerializeField] private Image[] starImages;

	[Header("�� ���� ��������Ʈ")]
	[SerializeField] private Sprite starON;

	[Header("Score Text")]
	[SerializeField] private Text scoreText;

	private Score score;

	private void OnEnable()
	{
		Time.timeScale = 0f;

		restart.onClick.AddListener(GameManager.instance.Stage.ReStart);
		nextStage.onClick.AddListener(GameManager.instance.Stage.NextLevel);

		score = FindObjectOfType<Score>();

		for (int i = 0; i < score.GetStarNum(); i++)
		{
			starImages[i].sprite = starON;
		}

		scoreText.text = score.GetCurrentScore().ToString();
	}
}
