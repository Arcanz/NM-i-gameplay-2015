using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Awake ()
	{
		players = FindObjectsOfType<Player>().ToList();
	}

	private List<Player> players;
	private List<KeyCode> leftCodes;
	private List<KeyCode> rightCodes; 

	public List<KeyCode> LeftKeyCodes
	{
		get
		{
			if(leftCodes==null)
				leftCodes = players.Select(player => player.leftKeyCode).ToList();
			return leftCodes;
		}
	}

	public List<KeyCode> RightKeyCodes
	{
		get { return rightCodes ?? (rightCodes = players.Select(player => player.rightKeyCode).ToList()); }
	} 

	// Update is called once per frame
	void Update () {
	
	}
}
