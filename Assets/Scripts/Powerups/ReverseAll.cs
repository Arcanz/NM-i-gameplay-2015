using UnityEngine;

public class ReverseAll : IPickupable 
{
	void OnTriggerEnter(Collider playerCollider)
	{
        playerCollider.GetComponent<Player>().SetReverseAll(gameObject.transform.position);
	    Despawn();
	}
}
