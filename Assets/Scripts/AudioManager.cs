using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	bool isFabricLoaded = false;

	public void Start()
	{
		// load the AudioManager
		// this probably should be someplace else!
		if (!Fabric.EventManager.Instance)
		{
			LoadFabric();
		}
		else
			Debug.Log("Fabric already loaded");
	}

	void Update()
	{
		// this probably can be implemented better with a CoRoutine and yield, but I couldn't figure out how! -- Jory
		// has Fabric finished loading?
		if (!isFabricLoaded)
		{
			if (Application.isLoadingLevel)
			{
				Debug.Log("Level is still loading, so wait on trying to play the music!");
			}
			else
			{
				// has Fabric been loaded?
				if (Fabric.EventManager.Instance)
				{
					isFabricLoaded = true;
					Debug.Log("Fabric loaded");
					// then start playing the menu music!
					//Fabric.EventManager.Instance.PostEvent("MX_Menu_lp", gameObject);
				}
				else
				{
					Debug.Log("Fabric hasn't loaded yet!");
				}
			}
		}
	}
	void LoadFabric()
	{
		Application.LoadLevelAdditive("Audio");
	}
}
