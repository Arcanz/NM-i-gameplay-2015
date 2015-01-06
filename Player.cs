using System;
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
		immunityBlinkSpeed = .5f;

	private Collider col;
	private Renderer rend;

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
		if (gameManager == null) return;
		rend = gameObject.renderer;
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
				col.enabled = true;
				rend.enabled = true;
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

		if (enviromentImmunity)
		{
			if (Time.time - blinkTimer > immunityBlinkSpeed)
			{
				rend.enabled = !rend.enabled;
				blinkTimer = Time.time;
			}

		}
		#endregion

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
			return;
		}
	}

	private void moveForward(float time)
	{
		transform.position = new Vector3(transform.position.x, transform.position.y ,transform.position.z + ForwardSpeed * moveDirection * time);
	}
	private void horizontalMove(float amount, int direction)
	{
		var tmp = transform.position;
		transform.position = new Vector3(tmp.x + (amount*direction), tmp.y, tmp.z);
	}

	private void TurnPlayerAround(float time)
	{
		//TODO: itween the fuckers
		iTween.RotateBy(gameObject,new Vector3(0,180,0),time);
		moveDirection *= -1;
	}

	public void SetNoPersonalInput(float time)
	{
		noPersonalInputTimer = 0;
		noPersonalInputDuration = time;
		personalInput = false;
	}
	public void SetSlow(float time)
	{
		speedModifierTimer = 0;
		ForwardSpeed = SlowSpeed;
		speedModifierDuration = time;
		Debug.Log("Hitslow");
	}

	public void SetRoot(float time)
	{
		speedModifierTimer = 0;
		ForwardSpeed = 0;
		speedModifierDuration = time;
		//Ignore input from others
		SetNoPersonalInput(time);
		SetOtherInputImmunity(time);
	}

	public void SetOtherInputImmunity(float time)
	{
		inputModifierTimer = 0;
		inputModifierDuration = time;
		affectedByOthers = false;
	}

	public void SetTempReverseDurection(float time)
	{
		tempReverseDirectionTimer = 0;
		tempReverseDirectionDuration = time;
		TurnPlayerAround(TurnSpeed);
	}

	public void SetEnviromentalImmunity(float time)
	{
		if (col.enabled == false || rend.enabled == false)
		{
			Debug.LogError("Renderer or collider not enabled");
			return;
		}

		enviromentImmunity = true;
		enviromentImmunityTimer = 0;
		enviromentImmunityDuration = time;
		col.enabled = false;
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
		gameManager.IhitReverseAll(powerupPos.x);
	}

	public void SetReverseDirection()
	{
		TurnPlayerAround(TurnSpeed);
		controlDirection *= -1;
	}

	public void SetBoost(float time)
	{
		speedModifierTimer = 0;
		ForwardSpeed = BoostSpeed;
		speedModifierDuration = time;
		Debug.Log("HitBoost");
	}
}
