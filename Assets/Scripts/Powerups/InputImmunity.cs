using UnityEngine;

public class InputImmunity : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetOtherInputImmunity(2f);
		Despawn();
	}
	void Update()
	{
		gameObject.transform.Rotate(0,2,0);
	}
}
	
