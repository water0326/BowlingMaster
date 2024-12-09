using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
	public void PopUpSettingUI() {
		if (FindObjectOfType<UI_Option>() == null)
		{
			Time.timeScale = 0f;
			GameManager.instance.UI.OpenPopUp("Option");
		}
	}    
}
