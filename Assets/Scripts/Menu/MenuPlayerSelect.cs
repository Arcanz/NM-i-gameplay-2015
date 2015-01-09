using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerSelect : MonoBehaviour
{
    public List<GameObject> Menus;
    public GameObject HowToPlay;

    void Start()
    {
        HowToPlay.SetActive(false);
    }

    public void SetNumberOfPlayers(int numberOfPlayers)
    {
		AudioManager.PlaySound("FX/Interface/Bloop");
		GameManager.NumberOfPlayers = numberOfPlayers;
        foreach (var menu in Menus)
            menu.SetActive(false);
        HowToPlay.SetActive(true);
    }

    public void LoadNextLevel()
    {
        AudioManager.PlaySound("FX/Interface/Bloop");
        Application.LoadLevel(1);
    }

    public void ExitPressed()
    {
        Application.Quit();
    }
}
