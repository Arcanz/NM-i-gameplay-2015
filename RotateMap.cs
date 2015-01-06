using UnityEngine;
using System.Collections;

public class RotateMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void RotateMapDirection()
	{
		var htScale = new Hashtable();
		htScale.Add("Space", Space.World);
		htScale.Add("y", 180);
		htScale.Add("easeType", iTween.EaseType.linear);
		htScale.Add("time", 0.5);
	
		
		iTween.RotateTo(gameObject, htScale);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
