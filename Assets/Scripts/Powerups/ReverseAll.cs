using UnityEngine;

public class ReverseAll : IPickupable 
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("ReverseMap", gameObject);
        playerCollider.GetComponent<Player>().SetReverseAll(gameObject.transform.position);
	    Despawn();
	}
	void Update()
	{
		gameObject.transform.Rotate(0,0,2);
	}
}
