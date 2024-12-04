//Unity
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class UI_StageClear : MonoBehaviour
{
    [SerializeField] private Button nextStage;
    [SerializeField] private Button restart;
    [SerializeField] private Button main;

    [Header("별 점 이미지 배열")]
    [SerializeField] private Image[] starImages;

    [Header("별 켜짐 스프라이트")]
    [SerializeField] private Sprite starON;

    [Header("Score Text")]
    [SerializeField] private Text scoreText;

    private Score score;

    private void OnEnable()
    {
        Time.timeScale = 0f;

        restart.onClick.AddListener(GameManager.instance.Stage.ReStart);

        score = FindObjectOfType<Score>();

        for (int i = 0; i < score.GetStarNum(); i++)
        {
            starImages[i].sprite = starON;
        }

        scoreText.text = score.GetCurrentScore().ToString();
    }
}
