using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneManager : MonoBehaviour
{
	[SerializeField] private GameObject gameStartButton;
	[SerializeField] private GameObject stageSelector;
	
	private void Awake() {
		stageSelector.SetActive(false);
		gameStartButton.SetActive(true);
	}
	
	public void OnGameStartButtonClicked()
	{
		stageSelector.SetActive(true);
		gameStartButton.SetActive(false);
	}
}
