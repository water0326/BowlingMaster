using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour 
{
	
	public void NextLevel() 
	{
		string sceneName = SceneManager.GetActiveScene().name;
		int index = int.Parse(sceneName.Split(' ')[1]);
		index++;
		SceneCoordinator.Instance.StartCoroutine(SceneCoordinator.Instance.LoadSceneCoroutine("Stage " + index));
	}
	
	public void ReStart()
	{
		Time.timeScale = 1f;
		SceneCoordinator.Instance.StartCoroutine(SceneCoordinator.Instance.LoadSceneCoroutine(SceneManager.GetActiveScene().name));
	}

	public void StageClear()
	{
		GameManager.instance.UI.OpenPopUp("Stage Clear");
	}

	public void StageFailed()
	{
		GameManager.instance.UI.OpenPopUp("Stage Failed");
	}
}
