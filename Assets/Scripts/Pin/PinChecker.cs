//Unity
using UnityEngine;

public class PinChecker : MonoBehaviour
{
    [SerializeField] private LaneController laneController;

    [SerializeField] private UI_CommonHeader commonHeader;

    private Pin[] pins;

    private int pinCount;

    private void Start()
    {
        pins = FindObjectsOfType<Pin>();

        pinCount = pins.Length;

        commonHeader.UpdatePinCount(pinCount);
    }

    public void CheckPin()
    {
        if (pinCount == 0)
        {
            GameManager.instance.Stage.StageClear();
        } 

        if (pinCount > 0 && laneController.GetRemainRollCount() == 0)
        {
            GameManager.instance.Stage.StageFailed();
        }
    }

    public void DecreasePinCount()
    {
        if (pinCount > 0)
        {
            pinCount--;
            commonHeader.UpdatePinCount(pinCount);
        }
    }
}
