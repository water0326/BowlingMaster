using UnityEngine;

public class CurveBall : Ball
{
	private bool canCurve = false;

	private Vector2 curveStart;
	private Vector2 curveEnd;

	private bool curveDragging = false;
	
	private bool isSkilling = false;

	[Header("Ŀ�� ������ �ð�")]
	[SerializeField] private float curveDelay = 0.3f;

	protected override void Update()
	{
		base.Update();
		CurveReady();
		CurveInput();
		if(isSkilling)
		{
			rb.velocity = Vector2.zero;
		}
	}

	private void CurveReady()
	{
		if (!DetectSkill()) return;

		canSkill = false;

		//Stop
		isSkilling = true;
		rb.velocity = Vector3.zero;

		animator.SetBool("isRoll", false);

		Invoke("CurveDelay", curveDelay);
	}

	private void CurveDelay()
	{
		canCurve = true;
	}

	private void CurveInput()
	{
		if (Input.touchCount > 0 && canCurve)
		{
			Touch touch = Input.GetTouch(0);

			switch (touch.phase)
			{
				//Touch Began
				case TouchPhase.Began:
					curveStart = touch.position;
					curveDragging = true;
					break;

				//Touch End
				case TouchPhase.Ended:
					if (curveDragging)
					{
						curveDragging = false;
						curveEnd = touch.position;
						isSkilling = false;
						//Call to RollBall Method
						CurveRoll();
					}
					break;
			}
		}
	}

	private void CurveRoll()
	{
		//Compute Swipe Direction
		Vector2 swipeDirection = curveEnd - curveStart;

		if (swipeDirection.y <= 0) return;

		canCurve = false;

		animator.SetBool("isRoll", true);

		float force = swipeDirection.magnitude;
		if (force < minSpeed) force = minSpeed;
		else if (force > maxSpeed) force = maxSpeed;   

		//Set Force Direction
		Vector2 forceDirection = new Vector2(swipeDirection.x, swipeDirection.y);

		//Add force to ball
		rb.AddForce(forceDirection * force * sensitivity);
	}
}
