using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class StageButton
{
	public Button button;
	public string sceneName;
}

public class StageSelect : MonoBehaviour
{
	
	[SerializeField] private StageButton[] stageButtons;
	[SerializeField] private GameObject buttonPrefab;
	[SerializeField] private GameObject gridLayout;

	void Start()
	{
		int clearedStage = DataManager.Instance.LoadClearedStage();

		for (int i = 0; i < stageButtons.Length; i++)
		{
			int index = i;
			stageButtons[i].button.onClick.AddListener(() => LoadStage(index));

			if (i > clearedStage)
			{
				stageButtons[i].button.interactable = false;
			}
		}
	}

	private void LoadStage(int index)
	{
		if (index < stageButtons.Length)
		{
			SceneCoordinator.Instance.LoadScene(stageButtons[index].sceneName);
		}
	}

	void Update()
	{
		
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(StageSelect))]
	public class StageSelectEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			StageSelect stageSelect = (StageSelect)target;

			if (GUILayout.Button("Add Stage Button"))
			{
				AddStageButton(stageSelect);
			}
		}

		private void AddStageButton(StageSelect stageSelect)
		{
			if (stageSelect.buttonPrefab != null && stageSelect.gridLayout != null)
			{
				GameObject newButton = (GameObject)PrefabUtility.InstantiatePrefab(stageSelect.buttonPrefab, stageSelect.gridLayout.transform);
				Undo.RegisterCreatedObjectUndo(newButton, "Create Stage Button");

				List<StageButton> stageButtonList = new List<StageButton>(stageSelect.stageButtons);
				StageButton newStageButton = new StageButton
				{
					button = newButton.GetComponent<Button>(),
					sceneName = $"Stage {stageButtonList.Count + 1}"
				};
				stageButtonList.Add(newStageButton);
				stageSelect.stageButtons = stageButtonList.ToArray();

				TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
				if (buttonText != null)
				{
					buttonText.text = (stageButtonList.Count).ToString();
				}
			}
			else
			{
				Debug.LogWarning("Button Prefab or Grid Layout is not assigned.");
			}
		}
	}
#endif
}
