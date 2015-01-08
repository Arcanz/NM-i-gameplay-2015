using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    private GameManager gameManager;
    public bool Activated;
    public GameObject game;
    public List<Text> PlayerScore;
    public List<Text> PlayerScoreLight;
    public List<GameObject> PlayerHolders;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
            game.SetActive(false);
        }

        for (var i = gameManager.Players.Count; i < PlayerHolders.Count; i++)
            PlayerHolders[i].SetActive(false);
    }

    void Update()
    {
        for (var i = 0; i < gameManager.Players.Count; i++)
        {
            PlayerScore[i].text = gameManager.Players[i].Score.ToString();
            PlayerScoreLight[i].text = gameManager.Players[i].Score.ToString();
        }
    }

    public void ReturnToMainMenu()
    {
        Application.LoadLevel(0);
    }
}
