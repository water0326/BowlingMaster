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
    [SerializeField] private Text[] ballCountTexts;

    [Header("공 추가 버튼 배열")]
    [SerializeField] private Button[] addBallButtons;

    [Header("취소 버튼")]
    [SerializeField] private Button deleteButton;

    [Header("확인 버튼")]
    [SerializeField] private Button confirmButton;

    [Header("남은 선택 가능한 공의 갯수를 보여주기 위한 TMP")]
    [SerializeField] private TextMeshProUGUI remainCount;

    [Header("선택한 공을 보여주기 위한 이미지 배열")]
    [SerializeField] private Image[] ballImages;

    private int imageIndex;

    [Header("볼링공 스프라이트 배열")]
    [SerializeField] private Sprite[] ballSprites;

    private Stack<int> indexStack;

    private DeckController deckController;

    private void OnEnable()
    {
        deckController = FindObjectOfType<DeckController>();

        confirmButton.interactable = false;

        indexStack = new Stack<int>();

        for(int i = 0; i < ballImages.Length; i++)
        {
            ballImages[i].gameObject.SetActive(false);
        }

        imageIndex = -1;

        UpdateCountTMPs();
        UpdateRemainCount();
    }

    /// <summary>
    /// Update All Count Text Mesh Pro
    /// </summary>
    private void UpdateCountTMPs()
    {
        for (int i = 0; i < ballCountTexts.Length; i++)
        {
            ballCountTexts[i].text = deckController.GetCurrentBallCount(i).ToString() + "개 남음";
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

        imageIndex++;
        ballImages[imageIndex].gameObject.SetActive(true);
        ballImages[imageIndex].sprite = ballSprites[_index];

        UpdateCountTMPs();
        UpdateRemainCount();

        indexStack.Push(_index);

        if (isMax)
        {
            confirmButton.interactable = true;
            DeactiveAddButtons();
        }
    }

    /// <summary>
    /// Delete Ball
    /// </summary>
    public void DeleteBall()
    {
        if (indexStack.Count == 0) return;

        int index = indexStack.Pop();

        deckController.DeleteBall(index);


        ballImages[imageIndex].gameObject.SetActive(false);
        imageIndex--;

        UpdateCountTMPs();
        UpdateRemainCount();

        ActiveAddButtons();

        confirmButton.interactable = false;
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
