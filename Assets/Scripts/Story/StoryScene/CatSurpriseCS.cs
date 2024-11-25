using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSurpriseCS : CutsceneEvent
{
	[System.Serializable]
	public class DialogData
	{
		public string text;
		public Transform target;
		public Vector2 textPosOffset;
		public float dialogDuration;
	}

	[SerializeField] private Dialog dialog;
	[SerializeField] private List<DialogData> dialogSequence;
	
	public override IEnumerator Execute()
	{
		foreach (var dialogData in dialogSequence)
		{
			StartCoroutine(dialog.AnimateText(
				dialogData.text, 
				dialogData.target, 
				dialogData.textPosOffset, 
				dialogData.dialogDuration
			));
			yield return new WaitForSeconds(dialogData.dialogDuration);
		}
	}
}
