﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPointsShow : MonoBehaviour {

    private GameManager manager;
    public List<Image> PlayerHolder;
    public List<Text> PlayerScoreText;
    public List<Text> PlayerScoreTextLight;
    public bool victory;

	// Use this for initialization
	void Start ()
	{
	    victory = false;
        manager = FindObjectOfType<GameManager>();

        for (var i = manager.Players.Count; i < PlayerHolder.Count; i++)
            PlayerHolder[i].gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (manager.GameStarted || victory)
	    {
            for (var i = 0; i < manager.Players.Count; i++)
	        {
                PlayerScoreText[i].text = manager.Players[i].Score.ToString();
                PlayerScoreTextLight[i].text = manager.Players[i].Score.ToString();
	        }
	        victory = false;
	    }
	}
}
