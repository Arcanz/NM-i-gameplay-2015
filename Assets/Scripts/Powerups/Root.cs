using UnityEngine;
using System.Collections;
public class Root : IPickupable
{

	bool notOpened = false;
	float timer;
	void OnTriggerEnter(Collider playerCollider)
	{
		
		animation.Play("LockRoot");
		playerCollider.GetComponent<Player>().SetRoot(1.5f);
		notOpened = true;
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
