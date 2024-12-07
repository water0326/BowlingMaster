using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public bool onVine;

	[Header("�ּ� �巡�� ����")]
	[SerializeField] protected float minSpeed = 200f;

	[Header("�ִ� �巡�� ����")]
	[SerializeField] protected float maxSpeed = 500f;

	//Sensitivity
	[Header("�巡�� �ΰ���")]
	[SerializeField] protected float sensitivity = 0.5f;
	
	[Header("Arrow Event")]
	[SerializeField] protected OnArrowEvent onArrowEvent;
	
	[SerializeField] private GameObject skipButton;

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
	
	private Coroutine checkVelocityCoroutine;

	private void OnEnable()
	{
		isDragging = false;
		isMove = false;
		canSkill = false;
		isDead = false;
		FinishLine finishLine = FindObjectOfType<FinishLine>();
		finishLine.currentBall = this.gameObject;
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
					onArrowEvent.RaiseEvent(true);
					break;

				//Touch End
				case TouchPhase.Ended:
					if (isDragging)
					{
						isDragging = false;
						endTouchPosition = touch.position;

						LaneController laneController = FindObjectOfType<LaneController>();
						laneController.DecreaseRollCount();
						onArrowEvent.RaiseEvent(false);

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
		float angle = Vector2.Angle(swipeDirection, Vector2.up);
		if (angle >= 70f) return;

		isMove = true;

		Invoke("MoveDelay", moveDelay);
		Invoke("ActiveSkipButton", 2f);

		animator.SetBool("isRoll", true);

		//Constraint Min Speed and Max Speed
		float force = swipeDirection.magnitude;
		if (force < minSpeed) force = minSpeed;
		else if (force > maxSpeed) force = maxSpeed;   

		//Set Force Direction
		Vector2 forceDirection = new Vector2(swipeDirection.x, swipeDirection.y);

		//Add force to ball
		rb.AddForce(forceDirection * force * sensitivity);
	}

	void ActiveSkipButton() 
	{
		FinishLine finishLine = FindObjectOfType<FinishLine>();
		finishLine.skipButton.SetActive(true);
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
			if(checkVelocityCoroutine == null)
			{
				checkVelocityCoroutine = StartCoroutine(CheckVelocity(col));
			}
			
		}
	}
	
	private IEnumerator CheckVelocity(Collider2D col)
	{
		float elapsedTime = 0f;
		float checkDuration = 3f;
		
		while(elapsedTime < checkDuration)
		{
			if(rb.velocity.y >= 0f)
			{
				checkVelocityCoroutine = null;
				yield break;
			}

			elapsedTime += Time.deltaTime;
			yield return null;
		}
		isDead = true;

		FinishLine finishLine = FindObjectOfType<FinishLine>();

		finishLine.DeadBall(col);
	}
	
	
	
	protected void ForceDeadBall()
	{
		isDead = true;
		FinishLine finishLine = FindObjectOfType<FinishLine>();
		finishLine.DeadBall(col);
	}
	
	protected void HideBall()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.color = new Color(1, 1, 1, 0);
	}
}
