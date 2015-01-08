using UnityEngine;

public class InputLock : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("FX/Powerups/Flask", gameObject);
		playerCollider.GetComponent<Player>().SetNoPersonalInput(2f);
		Despawn();
	}
}
