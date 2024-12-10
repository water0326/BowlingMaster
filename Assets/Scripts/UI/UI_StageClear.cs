//Unity
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class UI_StageClear : MonoBehaviour
{
	[SerializeField] private Button nextStage;
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

		main.onClick.AddListener(GameManager.instance.Stage.ToMain);
		nextStage.onClick.AddListener(GameManager.instance.Stage.NextLevel);

		score = FindObjectOfType<Score>();

		for (int i = 0; i < score.GetStarNum(); i++)
		{
			starImages[i].sprite = starON;
		}

		scoreText.text = score.GetCurrentScore().ToString();

		string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		if (int.TryParse(System.Text.RegularExpressions.Regex.Match(sceneName, @"\d+").Value, out int stageNumber))
		{
			DataManager.Instance.SaveClearedStage(stageNumber);
			
			// 다음 씬이 있는지 확인
			int nextSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
			if (nextSceneIndex >= UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
			{
				nextStage.interactable = false; // 다음 씬이 없으면 버튼 비활성화
			}
		}
	}
}
