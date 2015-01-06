using UnityEngine;
using System.Collections;

public class InputImmunity : MonoBehaviour {

	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetOtherInputImmunity(2f);
		Despawn();
	}
	void Despawn()
	{
		Destroy(gameObject);
	}
}
