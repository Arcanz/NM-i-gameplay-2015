using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Menu CurrentMenu;

	bool themeSongPlaying = false;

    public void Start()
    {
		AudioManager.LoadFabric();
        ShowMenu(CurrentMenu);
    }

	void Update()
	{
		if (!themeSongPlaying)
		{
			if (AudioManager.FabricLoaded)
			{
				themeSongPlaying = true;
				AudioManager.PlaySound("MX/Menu");

			}
		}
	}

    public void ShowMenu(Menu menu)
    {
        if (CurrentMenu != null)
            CurrentMenu.IsOpen = false;

        CurrentMenu = menu;
        CurrentMenu.IsOpen = true;

    }
}
