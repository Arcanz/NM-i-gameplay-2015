using UnityEngine;

public class Slow : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("SpeedSlow", transform.position);
		playerCollider.GetComponent<Player>().SetSlow(2f);
		Despawn();
	}
}
