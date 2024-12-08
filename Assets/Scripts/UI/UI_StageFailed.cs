//Unity
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_StageFailed : MonoBehaviour
{
    [SerializeField] private Button restart;
    [SerializeField] private Button main;

    private void OnEnable()
    {
        Time.timeScale = 0f;

        restart.onClick.AddListener(GameManager.instance.Stage.ReStart);
		main.onClick.AddListener(GameManager.instance.Stage.ToMain);
    }
}
