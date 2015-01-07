using UnityEngine;
using System.Collections;

public class fabricaudiotest : MonoBehaviour
{
    /*
	private bool FabricLoaded = false;
	// Use this for initialization
	void Start () {
		Debug.Log("Trying to find fabric");
		if(!Fabric.EventManager.Instance)
			LoadFabric();
		else
		{
			Debug.Log("Fabric loaded");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!FabricLoaded)
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
					FabricLoaded = true;
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
	}*/
}
