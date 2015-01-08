using UnityEngine;

public class InputLock : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("FlaskLock", transform.position);
		playerCollider.GetComponent<Player>().SetNoPersonalInput(2f);
		Despawn();
	}
}
