using UnityEngine;

public class LaneController : MonoBehaviour
{
    private DeckSlot deckSlot;

    private Stage stage;

    private Transform ballLuncher;

    private CameraController cameraController;

    //Current Roll Count
    [SerializeField] private int remainRollCount;

    private void Awake()
    {
        deckSlot = FindObjectOfType<DeckSlot>();
        stage = FindObjectOfType<Stage>();
        cameraController = FindObjectOfType<CameraController>();


        ballLuncher = deckSlot.transform.GetChild(0);

        remainRollCount = stage.GetRollCount();
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
        generatedBall.transform.position = ballLuncher.position;

        cameraController.Init(generatedBall.transform);

        cameraController.StartMove();
    }

    public void DecreaseRollCount()
    {
        remainRollCount--;
    }

    public int GetRemainRollCount()
    {
        return remainRollCount;
    }
}
