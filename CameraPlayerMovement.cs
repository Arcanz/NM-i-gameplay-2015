using UnityEngine;

public class CameraPlayerMovement : MonoBehaviour
{

    public GameObject Player1;
    public GameObject Player2;
    public GameObject MainCamera;
    public int Time;
    public int Reverse = 1;

    // Update is called once per frame
	void Update ()
	{
        if (Player1.transform.position.x * Reverse > Player2.transform.position.x * Reverse)
	    {
            iTween.MoveTo(MainCamera, new Vector3(Player1.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z), Time);
	    }
	    else
        {
            iTween.MoveTo(MainCamera, new Vector3(Player2.transform.position.x, Player2.transform.position.y, Player2.transform.position.z - 10), Time);
        }

	   // Debug.Log(renderer.isVisible ? "Visible" : "Invisible");
	}
}
