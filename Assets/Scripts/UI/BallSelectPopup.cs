//System
using System.Collections.Generic;

//Unity
using UnityEngine;
using UnityEngine.UI;

//TMPro
using TMPro;

public class BallSelectPopup : MonoBehaviour
{
    [Header("현재 추가할 수 있는 공의 갯수를 보여주기 위한 TMP 배열")]
    [SerializeField] private TextMeshProUGUI[] ballCountTMPs;

    [Header("공 추가 버튼 배열")]
    [SerializeField] private Button[] addBallButtons;

    [Header("취소 버튼")]
    [SerializeField] private Button deleteButton;
        
    [Header("이전 추가한 공을 저장하기 위한 인덱스 스택")]
    [SerializeField] private Stack<int> indexStack;

    [Header("남은 선택 가능한 공의 갯수를 보여주기 위한 TMP")]
    [SerializeField] private TextMeshProUGUI remainCount;

    private DeckController deckController;

    private void OnEnable()
    {
        deckController = FindObjectOfType<DeckController>();

        indexStack = new Stack<int>();

        UpdateCountTMPs();
        UpdateRemainCount();
    }

    /// <summary>
    /// Update All Count Text Mesh Pro
    /// </summary>
    private void UpdateCountTMPs()
    {
        for (int i = 0; i < ballCountTMPs.Length; i++)
        {
            ballCountTMPs[i].text = deckController.GetCurrentBallCount(i).ToString();
        }
    }

    private void UpdateRemainCount()
    {
        if (!deckController.CheckMaxSelect())
            remainCount.text = "선택 가능 공의 갯수 : " + deckController.GetCurrentMaxSelectableCount().ToString();
        else remainCount.text = "모든 공을 선택하였습니다.";
    }

    /// <summary>
    /// Add ball
    /// </summary>
    /// <param name="_index"></param>
    public void AddBall(int _index)
    {
        if (deckController.CheckMaxSelect() || deckController.CheckBallMax(_index)) return;

        bool isMax = deckController.AddBall(_index);

        UpdateCountTMPs();
        UpdateRemainCount();

        indexStack.Push(_index);

        if (isMax) DeactiveAddButtons();
    }

    /// <summary>
    /// Delete Ball
    /// </summary>
    public void DeleteBall()
    {
        if (indexStack.Count == 0) return;

        int index = indexStack.Pop();

        deckController.DeleteBall(index);

        UpdateCountTMPs();
        UpdateRemainCount();

        ActiveAddButtons();
    }

    /// <summary>
    /// Start Game : Generate Ball and Close Popup
    /// </summary>
    public void StartGame()
    {
        //if not enough select count, return method
        if (!deckController.CheckMaxSelect()) return;

        LaneController laneController = FindObjectOfType<LaneController>();

        laneController.GenerateBall();

        Destroy(gameObject);
    }

    private void ActiveAddButtons()
    {
        for (int i = 0; i < addBallButtons.Length; i++)
        {
            addBallButtons[i].interactable = true;
        }
    }

    private void DeactiveAddButtons()
    {
        for (int i = 0; i < addBallButtons.Length; i++)
        {
            addBallButtons[i].interactable = false;
        }
    }
}
