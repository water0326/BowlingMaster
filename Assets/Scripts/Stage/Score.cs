using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Stage stage;
    private PinChecker pinChecker;

    private int currentScore;

    [Header("ÇÉ Á¡¼ö")]
    [SerializeField] private int pinScore = 100;

    [Header("´Ù¼ö ÇÉ ÃÖ¼Ò °¹¼ö")]
    [SerializeField] private int minCount = 2;

    [Header("´Ù¼ö ÇÉ Ãß°¡ Á¡¼ö ºñÀ²")]
    [SerializeField] private float multiScoreRate = 1.5f;

    [Header("º® Ãæµ¹ È½¼ö")]
    [SerializeField] private int collisionScore = 20;

    private int lastPin = 0;

    private int starNum = -1;

    private void Start()
    {
        stage = FindObjectOfType<Stage>();
        pinChecker = FindObjectOfType<PinChecker>();

        starNum = -1;
    }

    public void UpdateScore()
    {
        int pinCount = stage.GetStartPinCount() - pinChecker.GetPinCount();

        if (lastPin != pinCount && pinCount - lastPin > minCount)
        {
            int newCount = pinCount - lastPin;

            lastPin = pinCount;

            currentScore += (int)(newCount * pinScore * multiScoreRate);

            multiScoreRate -= 0.2f;
        }

        for (int i = 0; i < stage.GetStarCount(); i++)
        {
            if (stage.GetStarScore(i) <= currentScore)
            {
                starNum = i;
            }
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public int GetStarNum()
    {
        return starNum;
    }
    
    public bool IsClear()
    {
        if (starNum == -1) return false;
        else return true;
    }

}
