using UnityEngine;

public class ReverseDirection : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetTempReverseDurection(2f);
		Despawn();
	}
}
