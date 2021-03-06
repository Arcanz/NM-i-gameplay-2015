﻿using UnityEngine;


public class ReverseDirection : IPickupable
{

	bool havePunched = false;
	float timer;

	void OnTriggerEnter(Collider playerCollider)
	{
		if (havePunched)
	        return;
		AudioManager.PlaySound("FX/Powerups/Boxing-Glove", gameObject);
		if(animation != null)
			animation.Play("Punch");
			
		playerCollider.GetComponent<Player>().SetTempReverseDurection(2f);
		havePunched = true;
	}

	void Update()
	{
		if(havePunched)
			timer += Time.deltaTime;
		if(timer > 1)
			Despawn();
		
	}
}
