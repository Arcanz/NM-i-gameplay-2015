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
		Fabric.EventManager.Instance.PostEvent(eventName);
	}
	private static void playAudioWithPosition(string eventName, GameObject ob)
	{
		Debug.Log("Triggered sound:" + eventName);
		Debug.Log("At position:" + ob);
		
		//AUDIO: with position
		Fabric.EventManager.Instance.PostEvent(eventName, ob);
	}

	public static bool FabricLoaded {get { return Fabric.EventManager.Instance; }}


	public static void PlaySound(string n)
	{
		LoadFabric();
		if (FabricLoaded)
			playAudio(n);
	}

	public static void PlaySound(string n, GameObject ob)
	{
		LoadFabric();
		if (FabricLoaded)
			playAudioWithPosition(n, ob);
	}

	public static void StopSound(string n)
	{
		Fabric.EventManager.Instance.PostEvent(n, Fabric.EventAction.StopAll);
	}

	public static void FadeOutMusic(string n) {
		// fade out the music!
		Fabric.Component component = Fabric.FabricManager.Instance.GetComponentByName(n);
		if (component != null) {
			component.FadeOut(0.1f, 0.5f);
		}
    }

	public static void LoadFabric()
	{
		if (FabricLoaded)
			return;
		Application.LoadLevelAdditive("Audio");
	}
}
