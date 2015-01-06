using UnityEngine;
using System.Collections;

public class SwedishLine : MonoBehaviour {


	void OnTriggerEnter(Collider playerCollider)
	{
		Application.LoadLevel(0);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
