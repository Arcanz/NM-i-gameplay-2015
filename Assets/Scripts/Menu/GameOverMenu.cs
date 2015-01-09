using System.Collections.Generic;
using System.Linq;
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
    public List<Text> PlayerNameLight;
    public List<Text> PlayerNameDark;
 
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
        var sorted = gameManager.Players.OrderByDescending(p => p.Score).ToList();
        for (var i = 0; i < gameManager.Players.Count; i++)
        {
            PlayerScore[i].text = sorted[i].Score.ToString();
            PlayerScoreLight[i].text = sorted[i].Score.ToString();
            PlayerNameDark[i].text = "Player " + (sorted[i].ID + 1) + "";
            PlayerNameLight[i].text = "Player " + (sorted[i].ID + 1) + "";
        }
    }

    public void ReturnToMainMenu()
    {
        Application.LoadLevel(0);
    }
}
