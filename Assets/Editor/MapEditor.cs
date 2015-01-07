using UnityEngine;
using UnityEditor;
using System.Collections;

public class MapEditor : EditorWindow {
	
	GameObject block;
	int MapLength;
	int MapWidth;
	public float BlockSize = 2.6f;

	[MenuItem ("Window/MapEditor")]
	
	public static void  ShowWindow () {
		EditorWindow.GetWindow(typeof(MapEditor));
	}
	void Start(){
		
	}
	void OnGUI()
	{
		BlockSize = EditorGUILayout.FloatField("Block size", BlockSize);
		MapLength = EditorGUILayout.IntField("Map Length", MapLength);
		MapWidth = EditorGUILayout.IntField("Map Width", MapWidth);
		
		if(GUILayout.Button("Build Map"))
		{
			BuildStuff(MapLength, MapWidth);
		}
		
	}
	
	void BuildStuff(int length, int width)
	{
		block = Resources.Load("Prefabs/normalTile") as GameObject;
		float xpos = 0;
		float zpos = 0;
		
		Debug.Log("Button clicked");
			
		for(int x = 0; x < length; x++)
		{
			Debug.Log(x);
			Debug.Log(length);
			int rand1 = Random.Range(0,4);
			float rotation = 0;
			
			switch(rand1)
			{
			case 1: rotation = 0f; break;
			case 2: rotation = 90f; break;
			case 3: rotation = 180f; break;
			case 4: rotation = -90f; break;
			}
			
			GameObject o = Instantiate(block, new Vector3(0,0,xpos),Quaternion.Euler(0,rotation,0)) as GameObject;
			
			
			for(int y = 0; y < width; y++)
			{
				int rand2 = Random.Range(0,4);
				float rotation2 = 0;
				
				switch(rand2)
				{
				case 1: rotation2 = 0f; break;
				case 2: rotation2 = 90f; break;
				case 3: rotation2 = 180f; break;
				case 4: rotation2 = -90f; break;
				}
				
			GameObject p = Instantiate(block, new Vector3(zpos,0,xpos),Quaternion.Euler(0,rotation2,0)) as GameObject;
				zpos += 2.6f;
			}
			zpos = 0;
			xpos += 2.6f;
		}
		
	}
}
