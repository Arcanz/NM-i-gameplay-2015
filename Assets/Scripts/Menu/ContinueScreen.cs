using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueScreen : MonoBehaviour
{
    private GameManager manager;
    public List<Text> PlayerNameText;
    public List<Text> PlayerKeysText;
    public float TimeToGameStart = 0.5f;
    private int green;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        for (var i = 0; i < manager.Players.Count; i++)
            PlayerKeysText[i].text = manager.Players[i].leftKeyCode + " / " + manager.Players[i].rightKeyCode;

        for (var i = manager.Players.Count; i < 4; i++)
            PlayerNameText[i].gameObject.SetActive(false);
    }

    // Update is called once per frame
	void Update () 
    {
	    for (var i = 0; i < manager.Players.Count; i++)
	        if (Input.GetKeyDown(manager.Players[i].leftKeyCode) || Input.GetKeyDown(manager.Players[i].rightKeyCode))
	        {
	            PlayerNameText[i].color = Color.green;
	            PlayerKeysText[i].color = Color.green;
            }

	    foreach (var text in PlayerNameText)
	        if (text.color == Color.green)
	            green++;

	    if (green == manager.Players.Count)
        {
//			AudioManager.PlaySound("StartRace");
            StartCoroutine(StartGame());
	    }
	    green = 0;
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(TimeToGameStart);
        manager.GameStarted = true;
        gameObject.SetActive(false);
		AudioManager.StopSound("MX/Menu");
		AudioManager.PlaySound("MX/BanjoAttack");
    }

}
