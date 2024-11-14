using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDragging;
    private bool isMove;

    //Touch Positions
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    //Sensitivity
    public float sensitivity = 5f;

    private void OnEnable()
    {
        isDragging = false;
        isMove = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Call HandleTouchInput Method
        HandleTouchInput();
    }

    /// <summary>
    /// To Detect Handle Touch Input
    /// </summary>
    void HandleTouchInput()
    {
        if (Input.touchCount > 0 && !isMove)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                //Touch Began
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    isDragging = true;
                    break;

                //Touch End
                case TouchPhase.Ended:
                    if (isDragging)
                    {
                        isDragging = false;
                        endTouchPosition = touch.position;

                        LaneController laneController = FindObjectOfType<LaneController>();
                        laneController.DecreaseRollCount();

                        //Call to RollBall Method
                        RollBall();
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// Roll Ball
    /// </summary>
    void RollBall()
    {
        isMove = true;

        //Compute Swipe Direction
        Vector2 swipeDirection = endTouchPosition - startTouchPosition;

        //Block Back Swipe & Horizontal Swipe
        if (swipeDirection.y <= 0) return;

        //Set Force Direction;
        Vector2 forceDirection = new Vector2(swipeDirection.x, swipeDirection.y);

        //Add force to ball
        rb.AddForce(forceDirection * sensitivity);
    }
}
