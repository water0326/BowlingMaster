using UnityEngine;

public class LaneController : MonoBehaviour
{
	private DeckSlot deckSlot;

	private Stage stage;

	private Transform ballLuncher;

	private CameraController cameraController;

	private UI_CommonHeader commonHeader;

	private int remainRollCount;
	
	[SerializeField] private Vector3 ballLauncherOffset;

	private void Awake()
	{
		deckSlot = FindObjectOfType<DeckSlot>();
		stage = FindObjectOfType<Stage>();
		cameraController = FindObjectOfType<CameraController>();
		commonHeader = FindObjectOfType<UI_CommonHeader>();


		ballLuncher = deckSlot.transform;

		remainRollCount = stage.GetRollCount();

		commonHeader.UpdateRollCount(remainRollCount);
	}

	/// <summary>
	/// Generate Ball
	/// </summary>
	public void GenerateBall()
	{
		if (remainRollCount <= 0) return;

		//Take Off Ball in Current Deck
		GameObject newball = deckSlot.TakeOffDeck();

		//Generate ball
		GameObject generatedBall = Instantiate(newball);

		//Init Position
		generatedBall.transform.position = ballLuncher.position + ballLauncherOffset;

		cameraController.Init(generatedBall.transform);

		cameraController.StartMove();
	}

	public void DecreaseRollCount()
	{
		remainRollCount--;
		commonHeader.UpdateRollCount(remainRollCount);
	}

	public int GetRemainRollCount()
	{
		return remainRollCount;
	}
}
