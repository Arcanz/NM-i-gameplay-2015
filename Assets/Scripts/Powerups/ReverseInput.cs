﻿using UnityEngine;

public class ReverseInput : IPickupable 
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("ReverseInput", gameObject);
		playerCollider.GetComponent<Player>().SetReversePersonalInput(2f);
		Despawn();
	}
	void Update()
	{
		gameObject.transform.Rotate(0,2,0);
	}
}
