using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SceneCoordinator : MonoBehaviour
{
	private static SceneCoordinator _instance;
	[SerializeField] private GameObject managerPrefab;
	[SerializeField] private GameObject screenShield;
	[SerializeField] private float fadeDuration = 1.0f;

	public static SceneCoordinator Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<SceneCoordinator>();
				if (_instance == null)
				{
					GameObject prefab = Resources.Load<GameObject>("Prefabs/Manager/SceneCoordinator");
					GameObject go = Instantiate(prefab);
					_instance = go.GetComponent<SceneCoordinator>();
				}
			}
			return _instance;
		}
	}

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (_instance != this)
		{
			Destroy(gameObject);
		}
	}
	
	public void LoadScene(string sceneName)
	{
		StartCoroutine(LoadSceneCoroutine(sceneName));
	}
	

	private IEnumerator LoadSceneCoroutine(string sceneName)
	{
		yield return StartCoroutine(FadeOut());

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
		yield return StartCoroutine(FadeIn());
	}

	private IEnumerator FadeOut()
	{
		CanvasGroup canvasGroup = screenShield.GetComponent<CanvasGroup>();
		if (canvasGroup == null)
		{
			canvasGroup = screenShield.AddComponent<CanvasGroup>();
		}

		screenShield.SetActive(true);
		float elapsedTime = 0f;
		while (elapsedTime < fadeDuration)
		{
			canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		canvasGroup.alpha = 1;
	}

	private IEnumerator FadeIn()
	{
		CanvasGroup canvasGroup = screenShield.GetComponent<CanvasGroup>();
		float elapsedTime = 0f;
		while (elapsedTime < fadeDuration)
		{
			canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		canvasGroup.alpha = 0;
		screenShield.SetActive(false);
	}
}
