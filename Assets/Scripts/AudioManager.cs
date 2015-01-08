using UnityEngine;
using System.Collections;

public static class AudioManager
{

	public static float 
		PenguinSquackInterval = 1f,
		PenguinSquackChance = 1f, // 0-1 = 0-100% chance
		PengquinStepInterval = 0.3f; 
	private static void playAudio(string eventName)
	{
		Debug.Log("Triggered sound:" + eventName);

		//AUDIO: without position
	}
	private static void playAudioWithPosition(string eventName, Vector3 position)
	{
		Debug.Log("Triggered sound:" + eventName);
		Debug.Log("At position:" + position);
		
		//AUDIO: with position
	}

	public static bool FabricLoaded {get { return Fabric.EventManager.Instance; }}


	public static void PlaySound(string n)
	{
		LoadFabric();
		if (FabricLoaded)
			playAudio(n);
	}

	public static void PlaySound(string n, Vector3 p)
	{
		LoadFabric();
		if (FabricLoaded)
			playAudioWithPosition(n, p);
	}


	public static void LoadFabric()
	{
		if (FabricLoaded)
			return;
		Application.LoadLevelAdditive("Audio");
	}
}
