using UnityEngine;

public class Stage : MonoBehaviour
{
    [Header("스테이지 별 볼 선택 가능 횟수 (별도 설정)")]
    [SerializeField] private int[] selectableCount;

    [Header("스테이지 별 공을 굴릴 수 있는 횟수 (별도 설정)")]
    [SerializeField] private int rollCount;

    [Header("공 선택 팝업")]
    [SerializeField] private GameObject ballSelectPopUp;

    [Header("별점 컷 배열")]
    [SerializeField] private int[] starScores;

    private int startPinCount;

    private void Start()
    {
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
