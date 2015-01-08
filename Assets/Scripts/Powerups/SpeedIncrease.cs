using UnityEngine;

public class SpeedIncrease : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("FX/Powerups/Speed-Increase", gameObject);
		playerCollider.GetComponent<Player>().SetBoost(2f);
		Despawn();
	}
}
