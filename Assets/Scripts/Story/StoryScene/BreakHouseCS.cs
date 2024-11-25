using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakHouseCS : CutsceneEvent
{
	[SerializeField] private GameObject house;
	[SerializeField] private Sprite brokenHouse;
	[SerializeField] private GameObject effectPrefab;
	[SerializeField] private Vector2 effectPosition;

	public override IEnumerator Execute() 
	{
		house.GetComponent<SpriteRenderer>().sprite = brokenHouse;
		Instantiate(effectPrefab, effectPosition, Quaternion.identity);
		yield return null;
	}
	
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(effectPosition, 0.5f);
	}
}
