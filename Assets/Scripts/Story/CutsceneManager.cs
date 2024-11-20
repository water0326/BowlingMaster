using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;

    private Queue<CutsceneEvent> eventQueue;
    private bool isPlaying;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayCutscene(List<CutsceneEvent> events)
    {
        if (isPlaying) return;

        isPlaying = true;
        eventQueue = new Queue<CutsceneEvent>(events);
        StartCoroutine(PlayNextEvent());
    }

    private IEnumerator PlayNextEvent()
    {
        while (eventQueue.Count > 0)
        {
            CutsceneEvent currentEvent = eventQueue.Dequeue();
            yield return StartCoroutine(currentEvent.Execute());
        }

        EndCutscene();
    }

    private void EndCutscene()
    {
        isPlaying = false;
        // 플레이어 제어 복구 등 추가 작업
    }
}
