using UnityEngine;

public class MenuPlayerSelect : MonoBehaviour 
{
    public void SetNumberOfPlayers(int numberOfPlayers)
    {
		GameManager.numberOfPlayers = numberOfPlayers;
		Application.LoadLevel(1);
    }

    public void ExitPressed()
    {
        Application.Quit();
    }
}
