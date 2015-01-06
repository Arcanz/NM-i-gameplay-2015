using UnityEngine;
using System.Collections;

public class ReverseInput : MonoBehaviour {

	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetReversePersonalInput(2f);
		Despawn();
	}
	void Despawn()
	{
		Destroy(gameObject);
	}
}
