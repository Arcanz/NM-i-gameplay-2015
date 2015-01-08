using UnityEngine;

public class SpeedIncrease : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("SpeedIncrease", gameObject);
		playerCollider.GetComponent<Player>().SetBoost(2f);
		Despawn();
	}
}
