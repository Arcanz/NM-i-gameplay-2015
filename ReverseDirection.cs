using UnityEngine;
using System.Collections;

public class ReverseDirection : MonoBehaviour {

	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetTempReverseDurection(2f);
		Despawn();
	}
	void Despawn()
	{
		Destroy(gameObject);
	}
}
