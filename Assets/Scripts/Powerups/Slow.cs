using UnityEngine;

public class Slow : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("SpeedSlow", gameObject);
		playerCollider.GetComponent<Player>().SetSlow(2f);
		Despawn();
	}
}
