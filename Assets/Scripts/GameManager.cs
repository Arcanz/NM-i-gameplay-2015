using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject MapRoot;
	public static int NumberOfPlayers;
	public int HurdleHitScore;
    public bool GameStarted;

	public float 
		BoostSpeed = 15f, 
		DefaultSpeed = 10f, 
		SlowSpeed = 5f,
		SidewayMoveAmount = 1f,
		SidewayInfluenceAMount = 0.5f,
		TurnSpeed = 0.1f,
		ImmunityBlinkSpeed = .5f,
		LowBoundry = -5.5f,
		HighBoundry = 4.3f,
		RespawnImunityTime = 1;

    public int NumberOfDeadPlayers
    {
        get { return Players.Count(player => !player.alive); }
    }
    public int NumberOfAlivePlayers
    {
        get { return Players.Count(player => player.alive); }
    }
    public List<Player> AlivePlayers
    {
        get { return Players.Where(p => p.alive).ToList(); }
    }

	private List<Player> players;
	private List<KeyCode> leftCodes;
	private List<KeyCode> rightCodes;

	[HideInInspector]
	public int Direction { get { return direction; } }

	private int direction = 1;
    public bool turning;

	public List<Player> Players
	{
		//NOTE: Gets new players once, then stores it
		get { return players ?? (players = FindObjectsOfType<Player>().ToList().OrderBy(p => p.ID).ToList()); }
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
		AudioManager.LoadFabric();
		if (Application.loadedLevel == 1)
		{
			if (NumberOfPlayers < 2)
				NumberOfPlayers = 2;
			for (var i = 0; i < NumberOfPlayers; i++)
			{
				var player = Resources.Load("Prefabs/Player" + i) as GameObject;
			    if (player != null)
			    {
			        player.GetComponent<Player>().ID = i;
			        player.GetComponent<Player>().alive = true;
			        var temp = new Vector3 (-4f + i*5f, 0.75f, 2.5f);
                    GameObject o = Instantiate(player, temp, Quaternion.Euler(0, -90, 0)) as GameObject;
			        o.name = "Player" + i;
			    }
			}
		}
	}

    public void IhitReverseOtherInput(float time, int ID)
    {
        foreach (var player in Players.Where(player => player.ID != ID))
            player.SetReversePersonalInput(time);
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
        if(turning)
            return;

	    turning = true;
		direction *= -1;
		foreach (var player in Players)
		{
            player.SetReverseDirection();
            player.SetRoot(time);
            player.SetEnviromentalImmunity(time);
        }
        var go = new GameObject("PivotPoint");
        go.transform.position = new Vector3(MapRoot.transform.position.x, MapRoot.transform.position.y, pivotPointZ);
        
		MapRoot.transform.parent = go.transform;

		var ht = new Hashtable {
        {"y", .5}, 
        {"time", time},
        {"oncomplete", "TweenFinished"}
        };
		iTween.RotateBy(go, ht);
    }

    public void TweenFinished()
    {
        turning = false;
    }

    public void IhitReverseAll(float pos)
	{
		ReverseAllPlayers(pos, 0.5f);
	}

	public Player GetLeadingPlayer()
	{
		return FindLeadingPlayer();
	}

    private void Update()
    {
        if (GameStarted)
        {
            foreach (var player in AlivePlayers)
            {
                var viewPos = FindObjectOfType<Camera>().camera.WorldToViewportPoint(player.transform.position);
                if (viewPos.x < 0 || viewPos.x > 1)
                    KillPlayer(player);
            }

        if (NumberOfAlivePlayers <= 1)
            RespawnPlayers();
        }
    }

    private void RespawnPlayers()
    {
        foreach (var player in Players.Where(player => !player.alive))
        {
            var posZ = AlivePlayers.Count > 0 ? AlivePlayers.First().transform.position.z : FindObjectOfType<Camera>().camera.transform.position.z;
            player.transform.position = new Vector3(-4 + (player.ID*2), 0.75f, posZ);
            player.ForwardSpeed = DefaultSpeed;
            player.alive = true;
            player.SetEnviromentalImmunity(RespawnImunityTime);
            player.SetOtherInputImmunity(RespawnImunityTime);
            player.SetDirection(direction);
        }
    }

    public void KillPlayer(Player player)
    {
        player.ForwardSpeed = 0;
        iTween.MoveBy(player.gameObject, new Vector3(0, 30, 0), 1f);
        StartCoroutine(MovePlayer(player));
        StartCoroutine(SetPlayerAsDead(player));
    }

    IEnumerator MovePlayer(Player player)
    {
        yield return new WaitForSeconds(.3f);
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + (100 * direction), player.transform.position.z);
    }

    IEnumerator SetPlayerAsDead(Player player)
    {
        yield return new WaitForSeconds(1.5f);
        player.alive = false;
    }
}
