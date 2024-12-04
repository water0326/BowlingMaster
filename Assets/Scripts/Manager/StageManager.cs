using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour 
{
    public void ReStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
