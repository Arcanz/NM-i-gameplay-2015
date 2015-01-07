using UnityEngine;

public class ImmunityAll : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetEnviromentalImmunity(2f);
		Despawn();
	}
	
	void Update()
	{
		gameObject.transform.Rotate(0,2,0);
	}
}
