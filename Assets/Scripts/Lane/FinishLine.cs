//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private LaneController lane;

    private CameraController cameraController;

    private PinChecker pinChecker;

    [SerializeField] private float initDelay = 1f;

    private void Start()
    {
        lane = FindObjectOfType<LaneController>();
        cameraController = FindObjectOfType<CameraController>();
        pinChecker = FindObjectOfType<PinChecker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Only Check Ball
        if (!collision.tag.Contains("Ball")) return;

        DeadBall(collision);
    }

    public void DeadBall(Collider2D collision)
    {
        StartCoroutine(InitCoroutine(collision));
    }

    IEnumerator InitCoroutine(Collider2D collision)
    {
        yield return new WaitForSeconds(initDelay);

        cameraController.UndoMove();

        if(collision != null) Destroy(collision.gameObject);

        lane.GenerateBall();

        pinChecker.CheckPin();
    }
}
