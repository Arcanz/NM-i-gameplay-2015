using UnityEngine;

public class MenuPlayerSelect : MonoBehaviour 
{
    public void SetNumberOfPlayers(int numberOfPlayers)
    {
		GameManager.NumberOfPlayers = numberOfPlayers;
		Application.LoadLevel(1);
    }

    public void ExitPressed()
    {
        Application.Quit();
    }
}
