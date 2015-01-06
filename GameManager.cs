using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngineInternal;

public class GameManager : MonoBehaviour {

	public GameObject MapRoot;


	public static int numberOfPlayers;

	private List<Player> players;
	private List<KeyCode> leftCodes;
	private List<KeyCode> rightCodes;
	private int 
		direction = 1;
	public List<Player> Players
	{
		//NOTE: Gets new players once, then stores it
		get { return players ?? (players = FindObjectsOfType<Player>().ToList()); }
	}

	public List<KeyCode> LeftKeyCodes
	{
		get { return leftCodes ?? (leftCodes = Players.Select(player => player.leftKeyCode).ToList()); }
	}

	public List<KeyCode> RightKeyCodes
	{
		get { return rightCodes ?? (rightCodes = players.Select(player => player.rightKeyCode).ToList()); }
	}

	void Awake()
	{
		if(Application.loadedLevel == 1)
		{
			if(numberOfPlayers < 2)
				numberOfPlayers = 2;
			
			for(int i = 0; i < numberOfPlayers; i++)
			{
				var player = Resources.Load("Prefabs/Player" + (i+1)) as GameObject;
				Vector3 temp = new Vector3 (-4f + i*2f, 0.75f, 2.5f);
				Instantiate(player, temp, Quaternion.identity);
			}
		}
	}
	
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	Player FindLeadingPlayer()
	{
		Player leadingPlayer = null;
		foreach (var player in Players)
		{
			if (leadingPlayer == null)
				leadingPlayer = player;
			if (direction>0)
			{
				if (leadingPlayer.transform.position.z < player.transform.position.z)
					leadingPlayer = player;
			}
			else
			{
				if (leadingPlayer.transform.position.z > player.transform.position.z)
					leadingPlayer = player;
			}
		}
		return leadingPlayer;
	}
	
	private void ReverseAllPlayers(float pivotPointX, float time)
	{
		direction *= -1;
		foreach (var player in Players)
		{
			player.SetReverseDirection();
		}
		var go = new GameObject("PivotPoint");
		go.transform.position = new Vector3(pivotPointX,MapRoot.transform.position.y,MapRoot.transform.position.y);
		MapRoot.transform.parent = go.transform;

		var ht = new Hashtable {{"y", .5}, {"time", time}};
		iTween.RotateBy(go,ht);
		MapRoot.transform.parent = null;
		Destroy(go);
	}
	public void IhitReverseAll(float pos)
	{
		ReverseAllPlayers(pos, 0.5f);
	}

	public Player GetLeadingPlayer()
	{
		return FindLeadingPlayer();
	}


	
}
