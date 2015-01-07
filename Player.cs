using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameManager gameManager;

	public  float BoostSpeed = 15f, DefaultSpeed = 10f, SlowSpeed = 5f;
	public float 
		ForwardSpeed = 10f,
		SidewayMoveAmount = 1f,
		TurnSpeed = 0.1f,
		ImmunityBlinkSpeed = .5f,
		PreviousZpos;

	[HideInInspector]
	public int Score { get { return hurdleScore + distanceScore; } }

    private int distanceScore,
        hurdleScore;

    public int ID;
    public SkinnedMeshRenderer Rend;
    public bool alive;
	private Collider col;

	#region Timers and duration

	//Speed modifier timers
	private float 
		speedModifierTimer = -1,
		speedModifierDuration = 0;

	//Input modifier timers
	private float 
		inputModifierTimer = -1,
		inputModifierDuration = 0;

	//Immunity timers
	private float 
		enviromentImmunityTimer = -1,
		enviromentImmunityDuration = 0;

	//Reverse control
	private float
		reverseControlTimer = -1,
		reverseControlDuration = 0;

	//Personal input
	private float
		noPersonalInputTimer = -1,
		noPersonalInputDuration = 0;

	private float
		tempReverseDirectionTimer = -1,
		tempReverseDirectionDuration = 0;

	#endregion

	private bool
		affectedByOthers = true,
		enviromentImmunity = false,
		personalInput = true;

	private float blinkTimer = 0;

	
	private int 
		moveDirection = 1, 
		controlDirection = 1;

	public List<KeyCode> OLKeyCodes;
	public List<KeyCode> ORKeyCodes;

	public KeyCode leftKeyCode;
	public KeyCode rightKeyCode;

	void Start()
	{
		if (gameManager == null)
		{
			gameManager = FindObjectOfType<GameManager>(); 
		}
        Rend = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
		col = gameObject.collider;

		OLKeyCodes = gameManager.LeftKeyCodes;
		ORKeyCodes = gameManager.RightKeyCodes;
	}

	// Update is called once per frame
	void Update ()
	{
		#region "Powerup" timers
		//No personal input duration
		if (noPersonalInputTimer >= 0)
		{
			noPersonalInputTimer += Time.deltaTime;
			if (noPersonalInputTimer >= noPersonalInputDuration)
			{
				personalInput = true;
				noPersonalInputTimer = -1;
			}
		}
		//Reverse control duration
		if (reverseControlTimer >= 0)
		{
			reverseControlTimer += Time.deltaTime;
			if (reverseControlTimer >= reverseControlDuration)
			{
				controlDirection *= -1;
				reverseControlTimer = -1;
			}
		}

		//Enviroment immunity duration
		if (enviromentImmunityTimer >= 0)
		{
			enviromentImmunityTimer += Time.deltaTime;
			if (enviromentImmunityTimer >= enviromentImmunityDuration)
			{
				enviromentImmunity = false;
				col.enabled  = true;
				Rend.enabled = true;
				enviromentImmunityTimer = -1;
			}
		}

		//Other player input immunity duration
		if (inputModifierTimer >= 0)
		{
			inputModifierTimer += Time.deltaTime;
			if (inputModifierTimer >= inputModifierDuration)
			{
				affectedByOthers = true;
				inputModifierTimer = -1;
			}
		}

		//Speed modified duration
		if (speedModifierTimer >= 0)
		{
			speedModifierTimer += Time.deltaTime;
			if (speedModifierTimer >= speedModifierDuration)
			{
				ForwardSpeed = DefaultSpeed;
				speedModifierTimer = -1;
			}
		}

		//Reverse direction duration
		if (tempReverseDirectionTimer >= 0)
		{
			tempReverseDirectionTimer += Time.deltaTime;
			if (tempReverseDirectionTimer >= tempReverseDirectionDuration)
			{
				TurnPlayerAround(TurnSpeed);
				tempReverseDirectionTimer = -1;
			}
        }
        #endregion

		if (enviromentImmunity)
		{
			if (Time.time - blinkTimer > ImmunityBlinkSpeed)
			{
				Rend.enabled = !Rend.enabled;
				blinkTimer = Time.time;
			}
		}

		if (affectedByOthers)
			handleOtherPlayersInputMovement();

		if (personalInput)
		{
			//Moving right?
			if (Input.GetKeyDown(rightKeyCode))
				horizontalMove(SidewayMoveAmount, controlDirection);

			//Moving left?
			if (Input.GetKeyDown(leftKeyCode))
				horizontalMove(-SidewayMoveAmount, controlDirection);
		}
		var pos = (int) transform.position.z;
		if (gameManager.Direction > 0)
		{
			if (pos > PreviousZpos)
			{
				distanceScore++;
				PreviousZpos = pos;
			}
		}
		else {
			if (pos < PreviousZpos)
			{
				distanceScore++;
				PreviousZpos = pos;
			}
		}

		PreviousZpos = transform.position.z;
		moveForward(Time.deltaTime);

	}

	private void handleOtherPlayersInputMovement()
	{
		//Others pushing left?
		if (OLKeyCodes.Where(keycode => keycode != leftKeyCode).Where(Input.GetKeyDown).Any())
		{
			horizontalMove(SidewayMoveAmount, controlDirection);
			return;
		}

		//Others pushing right?
		if (ORKeyCodes.Where(keyCode => keyCode != rightKeyCode).Where(Input.GetKeyDown).Any())
		{
			horizontalMove(-SidewayMoveAmount, controlDirection);
		}
	}

	private void moveForward(float time)
	{
		transform.position = new Vector3(transform.position.x, transform.position.y ,transform.position.z + ForwardSpeed * moveDirection * time);
	}
	private void horizontalMove(float amount, int direction)
	{
		var tmp = transform.position;
		if(tmp.x + amount*direction < -5.5f || tmp.x + amount*direction > 4.3f)
			return;
			
		transform.position = new Vector3(tmp.x + (amount*direction), tmp.y, tmp.z);
	}

	private void TurnPlayerAround(float time)
	{
		moveDirection *= -1;
		var ht = new Hashtable {{"y", .5}, {"time", time}};
        iTween.RotateBy(gameObject, ht);
		hurdleScore = gameManager.HurdleHitScore;
	}

	#region Enviroment/Powerup effects
	public void SetNoPersonalInput(float time)
	{
		noPersonalInputTimer = 0;
		noPersonalInputDuration = time;
		personalInput = false;
		hurdleScore += gameManager.HurdleHitScore;
	}
	public void SetSlow(float time)
	{
		speedModifierTimer = 0;
		ForwardSpeed = SlowSpeed;
		speedModifierDuration = time;
		hurdleScore = gameManager.HurdleHitScore;
	}

	public void SetRoot(float time)
	{
		StartCoroutine(StartAnimation("Walking", time));
		animation.Play("Idle");
		speedModifierTimer = 0;
		ForwardSpeed = 0;
		speedModifierDuration = time;
		//Ignore input from others
		SetNoPersonalInput(time);
		SetOtherInputImmunity(time);
		hurdleScore += gameManager.HurdleHitScore;
	}
	public IEnumerator StartAnimation(string animationName, float time)
	{
		yield return new WaitForSeconds(time);
		animation.Play(animationName);
	}
	
	public void SetOtherInputImmunity(float time)
	{
		inputModifierTimer = 0;
		inputModifierDuration = time;
		affectedByOthers = false;
		hurdleScore += gameManager.HurdleHitScore;
	}

	public void SetTempReverseDurection(float time)
	{
		tempReverseDirectionTimer = 0;
		tempReverseDirectionDuration = time;
		TurnPlayerAround(TurnSpeed);
		hurdleScore += gameManager.HurdleHitScore;
	}

	public void SetEnviromentalImmunity(float time)
	{
		if (col.enabled == false || Rend.enabled == false)
		{
			Debug.LogError("Renderer or collider not enabled");
			return;
		}

		enviromentImmunity = true;
		enviromentImmunityTimer = 0;
		enviromentImmunityDuration = time;
		col.enabled = false;
		hurdleScore += gameManager.HurdleHitScore;
	}

	public void SetReversePersonalInput(float time)
	{
		if (reverseControlTimer < 0)
			controlDirection *= -1;
		reverseControlDuration = time;
		reverseControlTimer = 0;
	}

	public void SetDirection(int dir)
	{
		moveDirection = dir;
		controlDirection = dir;
	}

	public void SetReverseAll(Vector3 powerupPos)
	{
		gameManager.IhitReverseAll(powerupPos.z);
		hurdleScore += gameManager.HurdleHitScore;
	}

	public void SetReverseDirection()
	{
		TurnPlayerAround(TurnSpeed);
		controlDirection *= -1;
		hurdleScore += gameManager.HurdleHitScore;
	}

	public void SetBoost(float time)
	{
		animation.Play("Fall to slide");
		StartCoroutine(StartAnimation("Slide", 0.5f));
		StartCoroutine(StartAnimation("Rise for slide", 1f));
		speedModifierTimer = 0;
		ForwardSpeed = BoostSpeed;
		speedModifierDuration = time;

		hurdleScore += gameManager.HurdleHitScore;

		Debug.Log("HitBoost");
		StartCoroutine(StartAnimation("Walking", time));

	}

    public void SetDead()
    {
        gameManager.KillPlayer(this);
    }
    #endregion
}
