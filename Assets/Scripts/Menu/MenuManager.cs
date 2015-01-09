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
				AudioManager.PlaySound("FX/Amb/Waves-Light");

			}
		}
		float shouldIPlay = Mathf.RoundToInt(Random.Range(0f, 0.3f) * 1000);

		if (shouldIPlay == 1)		//>AudioManager.PenguinSquackChance)
			AudioManager.PlaySound("FX/General/Penguin-Squawk", gameObject);
	}

    public void ShowMenu(Menu menu)
    {
        if (CurrentMenu != null)
            CurrentMenu.IsOpen = false;

        CurrentMenu = menu;
        CurrentMenu.IsOpen = true;

    }
}
