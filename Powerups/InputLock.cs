using UnityEngine;

public class InputLock : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetNoPersonalInput(2f);
		Despawn();
	}
}
