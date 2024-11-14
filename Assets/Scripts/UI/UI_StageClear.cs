//Unity
using UnityEngine;
using UnityEngine.UI;

public class UI_StageClear : MonoBehaviour
{
    [SerializeField] private Button nextStage;
    [SerializeField] private Button restart;
    [SerializeField] private Button main;

    private void OnEnable()
    {
        restart.onClick.AddListener(GameManager.instance.Stage.ReStart);
    }
}
