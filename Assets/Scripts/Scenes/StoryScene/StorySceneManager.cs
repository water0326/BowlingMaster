using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySceneManager : MonoBehaviour
{
    [SerializeField] private List<CutsceneEvent> cutsceneEvents;

    private void Start() {
        CutsceneManager.Instance.PlayCutscene(cutsceneEvents);
    }
}
