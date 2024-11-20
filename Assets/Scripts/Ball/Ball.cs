using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("최소 드래그 길이")]
    [SerializeField] protected float minSpeed = 200f;

    [Header("최대 드래그 길이")]
    [SerializeField] protected float maxSpeed = 500f;

    //Sensitivity
    [Header("드래그 민감도")]
    [SerializeField] protected float sensitivity = 0.5f;

    protected Rigidbody2D rb;
    protected CircleCollider2D col;
    protected SpriteRenderer spriteRenderer;

    private bool isDragging;
    private bool isDead;

    protected bool isMove;
    protected bool canSkill;

    private float moveDelay = 0.5f;

    //Touch Positions
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    protected Animator animator;

    private void OnEnable()
    {
        isDragging = false;
        isMove = false;
        canSkill = false;
        isDead = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        //Call HandleTouchInput Method
        HandleTouchInput();

        RotateDirection();

        DeadBall();
    }

    protected bool DetectSkill()
    {
        if (Input.touchCount > 0 && isMove && canSkill) return true;
        return false;
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
        //Compute Swipe Direction
        Vector2 swipeDirection = endTouchPosition - startTouchPosition;

        //Block Back Swipe & Horizontal Swipe
        if (swipeDirection.y <= 0) return;

        isMove = true;

        Invoke("MoveDelay", moveDelay);

        animator.SetBool("isRoll", true);

        //Constraint Min Speed and Max Speed
        if (swipeDirection.y < minSpeed) swipeDirection.y = minSpeed;
        else if (swipeDirection.y > maxSpeed) swipeDirection.y = maxSpeed;   

        //Set Force Direction
        Vector2 forceDirection = new Vector2(swipeDirection.x, swipeDirection.y);

        //Add force to ball
        rb.AddForce(forceDirection * sensitivity);
    }

    /// <summary>
    /// Skill Delay Method
    /// </summary>
    void MoveDelay()
    {
        canSkill = true;
        isMove = true;
    }

    private void RotateDirection()
    {
        transform.up = rb.velocity.normalized;
    }

    public void DeadBall()
    {
        if (rb.velocity.y < 0f && isMove && !isDead)
        {
            isDead = true;

            FinishLine finishLine = FindObjectOfType<FinishLine>();

            finishLine.DeadBall(col);
        }
    }
    protected void HideBall()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0);
    }
}
