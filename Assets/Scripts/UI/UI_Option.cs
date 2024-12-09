using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Option : MonoBehaviour
{
	[SerializeField] private Button returnButton;
	[SerializeField] private Button restartButton;
	[SerializeField] private Button toMainButton;

	void Start() {
		returnButton.onClick.AddListener(() => {
			GameManager.instance.UI.ClosePopUp(this.gameObject);
			Time.timeScale = 1f;
		});
		restartButton.onClick.AddListener(() => SceneCoordinator.Instance.LoadScene(SceneManager.GetActiveScene().name));
		toMainButton.onClick.AddListener(() => SceneCoordinator.Instance.LoadScene("Main"));
		
	}
}
