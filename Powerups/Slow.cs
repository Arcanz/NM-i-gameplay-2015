using UnityEngine;

public class Slow : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetSlow(2f);
		Despawn();
	}
}
