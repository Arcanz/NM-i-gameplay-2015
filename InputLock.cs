using UnityEngine;
using System.Collections;

public class InputLock : MonoBehaviour {

	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetNoPersonalInput(2f);
		Despawn();
	}
	void Despawn()
	{
		Destroy(gameObject);
	}
}
