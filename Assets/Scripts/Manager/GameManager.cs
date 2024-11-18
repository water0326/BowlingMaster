//Unity
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager instance;

    public bool isPause;

    private StageManager stage;
    private UIManager ui;
    private SoundManager sound;

    public StageManager Stage { get { return instance.stage; } }
    public UIManager UI { get { return instance.ui; } }

    public SoundManager Sound { get { return instance.sound; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        stage = GetComponent<StageManager>();
        ui = GetComponent<UIManager>();
    }

    public void Pause()
    {
        isPause = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPause = false;
        Time.timeScale = 1f;
    }
}
