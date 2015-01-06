﻿using UnityEngine;
using System.Collections;

public class ReverseRunDirection : MonoBehaviour {
	
	public GameObject map;

	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetReverseAll(gameObject.transform.position);
		map.GetComponent<RotateMap>().RotateMapDirection();
		Despawn();
	}
	void Despawn()
	{
		Destroy(gameObject);
	}
}