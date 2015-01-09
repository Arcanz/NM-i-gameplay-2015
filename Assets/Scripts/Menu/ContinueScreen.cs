using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueScreen : MonoBehaviour
{
    private GameManager manager;
    public List<GameObject> HolderObject;
    public List<Text> PlayerNameText;
    public List<Text> PlayerKeysTextLight;
    public List<Text> PlayerKeyTextDark;
    public GameObject ScoreDisplayer;
    public GameObject Panel;
    public List<Text> NumberCounter;
    public GameObject Counter;

    private bool hasRun;
    public float TimeToGameStart = 0.5f;
    private int green;

    void Start()
    {
        if (ScoreDisplayer != null)
            ScoreDisplayer.SetActive(false);
        else
        {
            ScoreDisplayer = GameObject.Find("ScoreDisplayer");
            ScoreDisplayer.SetActive(false);
        }
        manager = FindObjectOfType<GameManager>();
        for (var i = 0; i < manager.Players.Count; i++)
        {
            PlayerKeysTextLight[i].text = manager.Players[i].leftKeyCode + " / " + manager.Players[i].rightKeyCode;
            PlayerKeyTextDark[i].text = manager.Players[i].leftKeyCode + " / " + manager.Players[i].rightKeyCode;
        }
        for (var i = manager.Players.Count; i < 4; i++)
            HolderObject[i].gameObject.SetActive(false);
    }

    // Update is called once per frame
	void Update () 
    {
        if(!hasRun)
        {
            for (var i = 0; i < manager.Players.Count; i++)
                if (Input.GetKeyDown(manager.Players[i].leftKeyCode) || Input.GetKeyDown(manager.Players[i].rightKeyCode))
                {
                    PlayerNameText[i].color = Color.green;
                    PlayerKeysTextLight[i].color = Color.green;
                }

            foreach (var text in PlayerNameText)
                if (text.color == Color.green)
                    green++;

            if (green == manager.Players.Count)
            {
                StartCoroutine(StartGame());
                hasRun = true;
            }
            green = 0;
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(TimeToGameStart);
        ScoreDisplayer.SetActive(true);
		AudioManager.StopSound("MX/Menu");
        Panel.SetActive(false);
        AudioManager.PlaySound("FX/Race-Start/Countdown-Three");
        Counter.SetActive(true);
        StartCoroutine(Countdown3());
    }

    IEnumerator Countdown3()
    {
        yield return new WaitForSeconds(0.8f);
        AudioManager.PlaySound("FX/Race-Start/Countdown-Two");
        foreach (var text in NumberCounter)
            text.text = "2";
        NumberCounter[1].color = new Color(1, 0.50f, 0.01f);
        StartCoroutine(Countdown2());
    }
    IEnumerator Countdown2()
    {
        yield return new WaitForSeconds(0.8f);
        AudioManager.PlaySound("FX/Race-Start/Countdown-One");
        foreach (var text in NumberCounter)
            text.text = "1";
        NumberCounter[1].color = Color.yellow;
        StartCoroutine(Countdown1());
    }
    IEnumerator Countdown1()
    {
        yield return new WaitForSeconds(0.8f);
        AudioManager.PlaySound("FX/Race-Start/Countdown-GO");
        foreach (var text in NumberCounter)
            text.text = "GO";
        NumberCounter[1].color = Color.green;
        
        StartCoroutine(CountdownGo());
    }
    IEnumerator CountdownGo()
    {
        yield return new WaitForSeconds(0.5f);
        manager.GameStarted = true;
        AudioManager.PlaySound("MX/BanjoAttack");
        foreach (var text in NumberCounter)
            text.text = "";
        Counter.SetActive(false);
    }
}
