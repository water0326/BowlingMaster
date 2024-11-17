using UnityEngine;
using UnityEngine.UI;

public class UI_CommonHeader : MonoBehaviour
{
    [SerializeField] private Text rollCountText;
    [SerializeField] private Text pinCountText;

    public void UpdateRollCount(int _count)
    {
        rollCountText.text = _count.ToString();
    }

    public void UpdatePinCount(int _count)
    {
        pinCountText.text = _count.ToString();
    }
}
