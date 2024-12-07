using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallIndicator : MonoBehaviour
{
	[SerializeField] private DeckController deckController;
	[SerializeField] [ReadOnly] private Queue<GameObject> ballDeck = null;
	
	[SerializeField] private Image currentBallIndicator;
	[SerializeField] private Image nextBallIndicator;
	
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (ballDeck == null)
		{
			FetchBallDeck();
		}
		UpdateBallIndicator();
	}
	
	private void FetchBallDeck()
	{
		if (deckController.isDeckReady)
		{
			ballDeck = deckController.deck.ballDeck;
		}
	}

	private void UpdateBallIndicator()
	{
		if (ballDeck == null)
		{
			currentBallIndicator.color = new Color(1, 1, 1, 0);
			nextBallIndicator.color = new Color(1, 1, 1, 0);
			return;
		}

		if (ballDeck.Count > 0)
			{
				currentBallIndicator.color = new Color(1, 1, 1, 1);
				GameObject currentBall = ballDeck.Peek();
				currentBallIndicator.sprite = currentBall.GetComponent<SpriteRenderer>().sprite;
			}
		else
		{
			currentBallIndicator.color = new Color(1, 1, 1, 0);
		}
		
		if (ballDeck.Count > 1)
		{
			nextBallIndicator.color = new Color(1, 1, 1, 1);
			GameObject nextBall = ballDeck.ToArray()[1];
			nextBallIndicator.sprite = nextBall.GetComponent<SpriteRenderer>().sprite;
		}
		else
		{
			nextBallIndicator.color = new Color(1, 1, 1, 0);
		}

		
		
	}
}
