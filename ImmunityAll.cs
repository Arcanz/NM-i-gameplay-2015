using UnityEngine;
using System.Collections;

public class ImmunityAll : MonoBehaviour {

	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetEnviromentalImmunity(2f);
		Despawn();
	}
	void Despawn()
	{
		Destroy(gameObject);
	}
}
