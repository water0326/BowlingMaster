//Unity
using UnityEngine;
using UnityEngine.UI;

public class UI_StageFailed : MonoBehaviour
{
    [SerializeField] private Button restart;
    [SerializeField] private Button main;

    private void OnEnable()
    {
        restart.onClick.AddListener(GameManager.instance.Stage.ReStart);
    }
}
