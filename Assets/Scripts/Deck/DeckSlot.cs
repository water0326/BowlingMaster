//System
using System.Collections.Generic;

//Unity
using UnityEngine;

public class DeckSlot : MonoBehaviour
{
	//Queue : use to ball deck
	public Queue<GameObject> ballDeck { get; private set; }

	private void Awake()
	{
		//New Queue
		ballDeck = new Queue<GameObject>();
	}

	public void AddToDeck(GameObject _ball)
	{
		ballDeck.Enqueue(_ball);
	}

	public GameObject TakeOffDeck()
	{
		return ballDeck.Dequeue();
	}
}
