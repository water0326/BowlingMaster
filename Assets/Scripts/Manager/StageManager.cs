using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour 
{
	
	public void NextLevel() 
	{
		Time.timeScale = 1f;
		string sceneName = SceneManager.GetActiveScene().name;
		int index = int.Parse(sceneName.Split(' ')[1]);
		index++;
		print("Stage " + index);
		SceneCoordinator.Instance.LoadScene("Stage " + index);
	}
	
	public void ReStart()
	{
		Time.timeScale = 1f;
		SceneCoordinator.Instance.LoadScene(SceneManager.GetActiveScene().name);
	}
	
	public void ToMain()
	{
		Time.timeScale = 1f;
		SceneCoordinator.Instance.LoadScene("Main");
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
