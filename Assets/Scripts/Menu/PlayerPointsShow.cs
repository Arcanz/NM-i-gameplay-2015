﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPointsShow : MonoBehaviour {

    private GameManager manager;
    public List<Image> PlayerHolder;
    public List<Text> PlayerScoreText;

	// Use this for initialization
	void Start () {
        manager = FindObjectOfType<GameManager>();

        for (var i = manager.Players.Count; i < 4; i++)
            PlayerHolder[i].gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    for (var i = 0; i < manager.Players.Count; i++)
	        PlayerScoreText[i].text = manager.Players[i].Score.ToString();
	}
}