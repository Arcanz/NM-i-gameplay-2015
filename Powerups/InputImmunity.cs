using UnityEngine;

public class InputImmunity : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetOtherInputImmunity(2f);
		Despawn();
	}
}
	
