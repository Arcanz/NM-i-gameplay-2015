using UnityEngine;
using System.Collections;

public class CameraPlayerMovement : MonoBehaviour
{
    public float MyTime;
	private GameManager gameManager;
	public GameObject camera;
	
	void Start()
	{
		gameManager = FindObjectOfType<GameManager>(); 
		//MoveCamera();
	}
	
	void MoveCamera()
	{
		var player = gameManager.GetLeadingPlayer();
		var newPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, player.gameObject.transform.position.z);
		
		var htScale = new Hashtable();
		htScale.Add("Space", Space.World);
		htScale.Add("z", newPos.z);
		htScale.Add("easeType", iTween.EaseType.linear);
		htScale.Add("time", MyTime);
		htScale.Add("oncomplete", "MoveCamera");
		
		iTween.MoveTo(gameObject, htScale);
	}
    // Update is called once per frame
	void Update ()
	{    
		var player = gameManager.GetLeadingPlayer();
	  	//LeanTween.moveZ(gameObject, player.transform.position.z, MyTime);
		//var player = gameManager.GetLeadingPlayer();
		//var test = player.transform.position.z;
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, player.transform.position.z);
	   	
	   	//Debug.Log(newPos);
	   	
		//iTween.MoveTo(camera, newPos, MyTime);
		
	    
	   // Debug.Log(renderer.isVisible ? "Visible" : "Invisible");
	}
}
