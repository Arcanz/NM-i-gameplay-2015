using UnityEngine;
using System.Collections;
public class Root : IPickupable
{

	bool notOpened = false;
	float timer;
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("FX/Powerups/Bear-Trap", gameObject);
		animation.Play("LockRoot");
		playerCollider.GetComponent<Player>().SetRoot(1.5f);
		notOpened = true;
		AudioManager.PlaySound("FX/Powerups/Bear-Trap_reset", gameObject);
	}

	void Update()
	{
		if(notOpened)
			timer += Time.deltaTime;
		if(timer > 2f)
		{
			Despawn();
		}
	}
}
