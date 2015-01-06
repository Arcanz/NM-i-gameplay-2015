using UnityEngine;
using System.Collections;

public class SpeedIncrease : MonoBehaviour {

	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetBoost(2f);
		Despawn();
	}
	void Despawn()
	{
		Destroy(gameObject);
	}
}
