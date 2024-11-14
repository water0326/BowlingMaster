using UnityEngine;

public class DeckController : MonoBehaviour
{
    [Header("Ball Object ¹è¿­")]
    [SerializeField] private GameObject[] balls;

    [Header("Deck Slot ÄÄÆ÷³ÍÆ®")]
    [SerializeField] private DeckSlot deck;

    [Header("Stage ÄÄÆ÷³ÍÆ®")]
    [SerializeField] private Stage stage;

    private int[] currentBallCount;
    private int currentMaxSelectableCount;



    private void Start()
    {
        InitCurrentCount();
    }

    private void InitCurrentCount()
    {
        currentBallCount = new int[balls.Length];

        for (int i = 0; i < currentBallCount.Length; i++)
        {
            currentBallCount[i] = stage.GetSelectableCount(i);
        }

        currentMaxSelectableCount = stage.GetRollCount();
    }

    /// <summary>
    /// Add Ball
    /// </summary>
    /// <param name="_index"></param>
    /// <returns></returns>
    public bool AddBall(int _index)
    {
        if (currentBallCount[_index] > 0 && currentMaxSelectableCount > 0)
        {
            currentBallCount[_index]--;
            currentMaxSelectableCount--;

            deck.AddToDeck(balls[_index]);
        }

        if (CheckMaxSelect()) return true;
        else return false;
    }

    /// <summary>
    /// Delete Ball
    /// </summary>
    /// <param name="_index"></param>
    public void DeleteBall(int _index)
    {
        currentBallCount[_index]++;
        currentMaxSelectableCount++;

        deck.TakeOffDeck();
    }

    public bool CheckMaxSelect()
    {
        if (currentMaxSelectableCount == 0) return true;
        else return false;
    }

    public bool CheckBallMax(int _index)
    {
        if (currentBallCount[_index] == 0) return true;
        else return false;
    }

    public int GetCurrentBallCount(int _index)
    {
        return currentBallCount[_index];
    }

    public int GetCurrentMaxSelectableCount()
    {
        return currentMaxSelectableCount;
    }

}
