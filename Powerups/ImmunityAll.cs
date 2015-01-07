using UnityEngine;

public class ImmunityAll : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetEnviromentalImmunity(2f);
		Despawn();
	}
}
