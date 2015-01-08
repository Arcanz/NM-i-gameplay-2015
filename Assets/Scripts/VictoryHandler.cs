using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class VictoryHandler : MonoBehaviour {
	private GameObject Podium, FirstPlacePlayer, SecondPlacePlayer, ThirdPlacePlayer, ForthPlacePlayer;
	private Vector3 FirstPlacePos, SecondPlacePos, ThirdPlacePos, ForthPlacePos;
    private GameManager manager;
    private List<Player> players, rankPlayers;
    private int Score;
	// Use this for initialization
	void Start () {
		Podium = Resources.Load("Prefabs/VictoryPodium") as GameObject;
		
		FirstPlacePos = new Vector3(-5.25f, 6f, -4.6f);
		SecondPlacePos = new Vector3(-6.8f, 3.8f, -5.9f); 
		ThirdPlacePos = new Vector3(-3.8f, 3.2f, -3.4f);
		ForthPlacePos = new Vector3(-1f, 2.25f, -1f);

	    manager = FindObjectOfType<GameManager>();
	    players = manager.Players;
	}
	
	void OnTriggerEnter(Collider playerCollider)
	{
	    manager.GameStarted = false;
		Instantiate(Podium, new Vector3(0,1,0),Quaternion.Euler(0,-130,0));
        PlacePlayersAtPodium();
        foreach (var player in manager.Players)
        {
            player.SetRoot(1000f);
        }
	}
	
	void PlacePlayersAtPodium()
	{
        rankPlayers = players.OrderBy(p => p.Score).ToList();
		rankPlayers[0].transform.position = FirstPlacePos;
        rankPlayers[1].transform.position = SecondPlacePos;
        rankPlayers[2].transform.position = ThirdPlacePos;
        rankPlayers[3].transform.position = ForthPlacePos;
	}
	// Update is called once per frame
	void Update () 
	{
	
	}
}
