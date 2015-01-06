using UnityEngine;
using System.Collections;

public class ReverseDirection : MonoBehaviour {

	bool havePunched = false;
	float timer;
	void OnTriggerEnter(Collider playerCollider)
	{
		animation.Play("Punch");
		playerCollider.GetComponent<Player>().SetTempReverseDurection(2f);
		havePunched = true;
	}
	void Despawn()
	{
		Destroy(gameObject);
	}
	void Update()
	{
		if(havePunched)
			timer += Time.deltaTime;
		if(timer > 1)
			Despawn();
		
	}
}
