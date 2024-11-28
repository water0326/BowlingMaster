using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCS : CutsceneEvent
{
    [System.Serializable]
    public class DialogueData
    {
        public string text;
        public Transform speaker;
        public Vector2 textPosOffset;
        public float dialogDuration;
    }

    [SerializeField] private Dialog dialog;  // 왼쪽 대화창
    [SerializeField] private List<DialogueData> dialogueSequence;
    
    private void OnDrawGizmosSelected()
    {
        if (dialogueSequence == null) return;
        
        foreach (var dialogData in dialogueSequence)
        {
            if (dialogData.speaker == null) continue;
            
            Vector3 speakerPos = dialogData.speaker.position;
            Vector3 textPos = speakerPos + new Vector3(dialogData.textPosOffset.x, dialogData.textPosOffset.y, 0);
            
            // Draw line from speaker to text position
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(speakerPos, textPos);
            
            // Draw sphere at text position
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(textPos, 0.3f);
        }
    }
    
    public override IEnumerator Execute()
    {
        foreach (var dialogData in dialogueSequence)
        {
            
            StartCoroutine(dialog.AnimateText(
                dialogData.text, 
                dialogData.speaker, 
                dialogData.textPosOffset, 
                dialogData.dialogDuration
            ));
            //
            yield return new WaitForSeconds(dialogData.dialogDuration);
        }
    }
}
