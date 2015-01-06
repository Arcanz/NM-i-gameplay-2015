using UnityEngine;

public class FallingFloor : MonoBehaviour {

	void OnTriggerEnter()
	{
		gameObject.rigidbody.useGravity = true;
		gameObject.rigidbody.isKinematic = false;
	}
	
	void Update()
	{
		if(gameObject.transform.position.y < -5)
			Destroy(gameObject);
	}
}
