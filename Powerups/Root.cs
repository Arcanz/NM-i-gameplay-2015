using UnityEngine;

public class Root : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetRoot(2f);
		Despawn();
	}
}
