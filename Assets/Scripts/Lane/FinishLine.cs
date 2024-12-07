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

	private Score score;

	public GameObject skipButton;

	[SerializeField] private float initDelay = 1f;
	
	public GameObject currentBall;

	private void Start()
	{
		lane = FindObjectOfType<LaneController>();
		cameraController = FindObjectOfType<CameraController>();
		pinChecker = FindObjectOfType<PinChecker>();
		score = FindObjectOfType<Score>();
		skipButton.SetActive(false);
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
	
	public void SkipBall() 
	{
		StartCoroutine(InitCoroutine(currentBall.GetComponent<Collider2D>()));
	}

	IEnumerator InitCoroutine(Collider2D collision)
	{
		skipButton.SetActive(false);
		yield return new WaitForSeconds(initDelay);

		cameraController.UndoMove();

		if(collision != null) Destroy(collision.gameObject);

		lane.GenerateBall();

		score.UpdateScore();

		pinChecker.CheckPin();
	}
}
