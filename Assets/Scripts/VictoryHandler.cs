using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VictoryHandler : MonoBehaviour {
	private GameObject Podium, FirstPlacePlayer, SecondPlacePlayer, ThirdPlacePlayer, ForthPlacePlayer;
    private GameManager manager;
    private CameraPlayerMovement cameraScript;
    private List<Player> players, rankPlayers;
    private List<Vector3> PlayerPodiumPos;
    private int Score;
    public GameObject gameOverMenu;

	// Use this for initialization
	void Start () {
		Podium = Resources.Load("Prefabs/VictoryPodium") as GameObject;
	    PlayerPodiumPos = new List<Vector3>();
        PlayerPodiumPos.Add(new Vector3(-1.3f, 6.6f, -4.6f)); // First Place
		PlayerPodiumPos.Add(new Vector3(-3.14f, 4.6f, -6.1f)); // Second
		PlayerPodiumPos.Add(new Vector3(0.7f, 3.8f, -3.2f)); // Third
		PlayerPodiumPos.Add(new Vector3(3.5f, 2.85f, -0.5f)); // Forth

	    if (gameOverMenu == null)
            gameOverMenu = GameObject.Find("GameOverScoreboardboard");
	    manager = FindObjectOfType<GameManager>();
	    cameraScript = FindObjectOfType<Camera>().GetComponent<CameraPlayerMovement>();
	    players = manager.Players;
	}
	
	void OnTriggerEnter(Collider playerCollider)
	{
	    manager.GameStarted = false;
		Instantiate(Podium, new Vector3(4,1f,0),Quaternion.Euler(0,-130,0));
        PlacePlayersAtPodium();
	    cameraScript.victorious = true;
        gameOverMenu.SetActive(true);
        foreach (var player in manager.Players)
        {
            player.SetRoot(1000f);
        }
	}
	
	void PlacePlayersAtPodium()
	{
        rankPlayers = players.OrderBy(p => p.Score).ToList();
	    for (int i = 0; i < rankPlayers.Count; i++)
	    {
	        string tempName = rankPlayers[i].name;
	        var tempPlayer = Resources.Load("Prefabs/" + tempName);
	        GameObject o = Instantiate(Resources.Load("Prefabs/" + tempName), PlayerPodiumPos[i], Quaternion.Euler(0, 50, 0)) as GameObject;
	        o.collider.enabled = false;
	    }
	}
	// Update is called once per frame
	void Update () 
	{
	
	}
}
