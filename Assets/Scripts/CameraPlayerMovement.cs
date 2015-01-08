using UnityEngine;

public class CameraPlayerMovement : MonoBehaviour
{
	public float MyTime, cameraOfset;
	private GameManager gameManager;
	public bool victorious;
	void Start()
	{
		victorious = false;
		gameManager = FindObjectOfType<GameManager>();
	}

	// Update is called once per frame
	void Update()
	{
		if (victorious)
		{
			gameObject.transform.position = new Vector3(13, 13, -20);
			gameObject.transform.rotation = Quaternion.Euler(15, 320, 0);
		}
		else
		{
			var player = gameManager.GetLeadingPlayer();
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,
				player.transform.position.z - cameraOfset);
		}
	}
}