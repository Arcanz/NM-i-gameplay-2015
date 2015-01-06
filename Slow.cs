using UnityEngine;
using System.Collections;

public class Slow : MonoBehaviour {

	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetSlow(2f);
		Despawn();
	}
	void Despawn()
	{
		Destroy(gameObject);
	}
}
