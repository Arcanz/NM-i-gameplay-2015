using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject MapRoot;
	public static int NumberOfPlayers;
    public List<Player> DeadPlayers;
    public float RespawnImunityTime = 1;

	private List<Player> players;
	private List<KeyCode> leftCodes;
	private List<KeyCode> rightCodes;
	private int direction = 1;

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
			if(NumberOfPlayers < 2)
				NumberOfPlayers = 2;
			
			for(var i = 0; i < NumberOfPlayers; i++)
			{
				var player = Resources.Load("Prefabs/Player" + i) as GameObject;
			    if (player != null)
			    {
			        player.GetComponent<Player>().ID = i;
			        var temp = new Vector3 (-4f + i*2f, 0.75f, 2.5f);
			        Instantiate(player, temp, Quaternion.Euler(0, -90, 0));
			    }
			}
		}
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
	
	private void ReverseAllPlayers(float pivotPointZ, float time)
	{
		direction *= -1;
		foreach (var player in Players)
		{
			player.SetReverseDirection();
            player.SetEnviromentalImmunity(time);
            player.SetRoot(time);
        }
        var go = new GameObject("PivotPoint");
        go.transform.position = new Vector3(MapRoot.transform.position.x, MapRoot.transform.position.y, pivotPointZ);
        
		MapRoot.transform.parent = go.transform;

		var ht = new Hashtable {{"y", .5}, {"time", time}};
		iTween.RotateBy(go, ht);
    }
	
    public void IhitReverseAll(float pos)
	{
		ReverseAllPlayers(pos, 0.5f);
	}

	public Player GetLeadingPlayer()
	{
		return FindLeadingPlayer();
	}

    void Update()
    {
        if (DeadPlayers.Count >= NumberOfPlayers - 1)
        {
            foreach (var player in Players)
            {
                bool alive = true;
                foreach (var deadPlayer in DeadPlayers)
                {
                    if (deadPlayer.ID == player.ID)
                        alive = false;
                }
                if (alive)
                {
                    RespawnPlayer(player);
                    return;
                }
            }
        }
    }

    private void RespawnPlayer(Player loneSurvivor)
    {
        foreach (var deadPlayer in DeadPlayers)
        {
            deadPlayer.transform.position = new Vector3(-4 + (deadPlayer.ID * 2), +0.75f, loneSurvivor.transform.position.z);
            deadPlayer.SetDirection(direction);
            deadPlayer.ForwardSpeed = deadPlayer.DefaultSpeed;
            deadPlayer.SetEnviromentalImmunity(RespawnImunityTime);
            deadPlayer.SetOtherInputImmunity(RespawnImunityTime);
        }
        DeadPlayers.Clear();
    }

    public void KillPlayer(int ID)
    {
        foreach (var player in Players.Where(player => player.ID == ID))
        {
            player.ForwardSpeed = 0;
            player.SetOtherInputImmunity(2);
            iTween.MoveBy(player.gameObject, new Vector3(0, 40, 0), 0.5f);
            StartCoroutine(MovePlayer(player));
            StartCoroutine(WaitAndRespawn(player));
        }
    }
    IEnumerator MovePlayer(Player player)
    {
        yield return new WaitForSeconds(.3f);
        player.transform.position = new Vector3(-100 * direction, -100 * direction, -100 * direction);
    }

    IEnumerator WaitAndRespawn(Player player)
    {
        yield return new WaitForSeconds(1.5f);
        DeadPlayers.Add(player);
    }
}
