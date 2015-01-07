using UnityEngine;

public class ReverseInput : IPickupable 
{
	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetReversePersonalInput(2f);
		Despawn();
	}
	void Update()
	{
		gameObject.transform.Rotate(0,2,0);
	}
}
