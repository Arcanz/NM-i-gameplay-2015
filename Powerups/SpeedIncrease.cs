using UnityEngine;

public class SpeedIncrease : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetBoost(2f);
		Despawn();
	}
}
