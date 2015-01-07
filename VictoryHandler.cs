using UnityEngine;
using System.Collections;

public class VictoryHandler : MonoBehaviour {
	private GameObject Podium, FirstPlacePlayer, SecondPlacePlayer, ThirdPlacePlayer, ForthPlacePlayer;
	private Vector3 FirstPlacePos, SecondPlacePos, ThirdPlacePos, ForthPlacePos;
	// Use this for initialization
	void Start () {
		Podium = Resources.Load("Prefabs/VictoryPodium") as GameObject;
		
		FirstPlacePos = new Vector3(-5.25f, 6f, -4.6f);
		SecondPlacePos = new Vector3(-6.8f, 3.8f, -5.9f); 
		ThirdPlacePos = new Vector3(-3.8f, 3.2f, -3.4f);
		ForthPlacePos = new Vector3(-1f, 2.25f, -1f);
	}
	
	void OnTriggerEnter(Collider playerCollider)
	{
		Instantiate(Podium, new Vector3(0,1,0),Quaternion.Euler(0,-130,0));
	}
	void FindPlayerRank()
	{
		
	}
	void PlacePlayersAtPodium()
	{
		FirstPlacePlayer.transform.position = FirstPlacePos;
		SecondPlacePlayer.transform.position = SecondPlacePos;
		ThirdPlacePlayer.transform.position = ThirdPlacePos;
		ForthPlacePlayer.transform.position = ForthPlacePos;
	}
	// Update is called once per frame
	void Update () 
	{
	
	}
}
