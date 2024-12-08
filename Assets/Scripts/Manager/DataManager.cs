using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	private static DataManager instance;

	public static DataManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<DataManager>();
				if (instance == null)
				{
					GameObject singletonObject = new GameObject();
					instance = singletonObject.AddComponent<DataManager>();
					singletonObject.name = typeof(DataManager).ToString() + " (Singleton)";
				}
			}
			return instance;
		}
	}

	private int clearedStage;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}

	public void SaveClearedStage(int stage)
	{
		int currentClearedStage = PlayerPrefs.GetInt("ClearedStage", 0);
		
		if (stage > currentClearedStage)
		{
			clearedStage = stage;
			PlayerPrefs.SetInt("ClearedStage", clearedStage);
			PlayerPrefs.Save();
		}
	}

	public int LoadClearedStage()
	{
		clearedStage = PlayerPrefs.GetInt("ClearedStage", 0);
		return clearedStage;
	}
}
