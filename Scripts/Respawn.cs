using UnityEngine;

public class Respawn : MonoBehaviour
{
	public bool RespawnNow;
	public GameObject RespawnableObject;
	public float InvulnerableTime = 2;
	public float BlinkFrequency = 0.2f;

	private BoxCollider _objectCollider;
	private bool _timeToRespawn;
	private float _timerSeconds;

	private float _blinkStartFrequency;
	private bool _renederEnabled;

	void Start()
	{
		_blinkStartFrequency = BlinkFrequency;
		_timeToRespawn = false;
		_timerSeconds = 0;
		_renederEnabled = true;
	}

	void Update()
	{
		if (RespawnNow && !_timeToRespawn)
		{
			_objectCollider = RespawnableObject.GetComponent<BoxCollider>();
			_objectCollider.enabled = false;
			_timeToRespawn = true;
		}

		if (_timeToRespawn && _timerSeconds < InvulnerableTime)
		{
			_timerSeconds += Time.deltaTime;

			if (_timerSeconds > _blinkStartFrequency)
			{
				_blinkStartFrequency += BlinkFrequency;
				_renederEnabled = !_renederEnabled;
				RespawnableObject.renderer.enabled = _renederEnabled;
			}
		}
		else if (_timeToRespawn && _timerSeconds > InvulnerableTime)
		{
			Start();
			_objectCollider.enabled = true;
			RespawnableObject.renderer.enabled = true;
			RespawnNow = false;
		}
	}
}
