using UnityEngine;
using System.Collections;

public class Root : MonoBehaviour {

	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetRoot(2f);
		Despawn();
	}
	void Despawn()
	{
		Destroy(gameObject);
	}
}
