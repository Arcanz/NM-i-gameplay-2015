using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fabric;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameManager gameManager;

    private GameObject objectOverHead;
    private float overHeadPosition = 4;
	
	[HideInInspector]
	public float
		ForwardSpeed = 10f,
		PreviousZpos;


    public int myScore;
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

	    ForwardSpeed = gameManager.DefaultSpeed;
		OLKeyCodes = gameManager.LeftKeyCodes;
		ORKeyCodes = gameManager.RightKeyCodes;
	}

	// Update is called once per frame
	void Update ()
	{
	    myScore = Score;
		if (Time.time%AudioManager.PenguinSquackInterval <= 0.1f)
		{
			if(Random.Range(0f, 1f)>AudioManager.PenguinSquackChance)
				AudioManager.PlaySound("PenguinSquack", gameObject);
		}
		if (Time.time%AudioManager.PengquinStepInterval <= 0.1f)
		{
//			AudioManager.PlaySound("PenguinStep");
		}
	    if (gameManager.GameStarted)
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
	            StopSliding();
	            speedModifierTimer += Time.deltaTime;
	            if (speedModifierTimer >= speedModifierDuration)
	            {
					if(ForwardSpeed == 0)
					{

					}
	                ForwardSpeed = gameManager.DefaultSpeed;
	                speedModifierTimer = -1;
	            }
	        }

	        //Reverse direction duration
	        if (tempReverseDirectionTimer >= 0)
	        {
	            tempReverseDirectionTimer += Time.deltaTime;
	            if (tempReverseDirectionTimer >= tempReverseDirectionDuration)
	            {
	                TurnPlayerAround(gameManager.TurnSpeed);
	                tempReverseDirectionTimer = -1;
	            }
	        }

	        #endregion

	        if (enviromentImmunity)
	        {
	            if (Time.time - blinkTimer > gameManager.ImmunityBlinkSpeed)
	            {
	                Rend.enabled = !Rend.enabled;
                    blinkTimer = Time.time;
	            }
	        }

	        if (affectedByOthers)
	            handleOtherPlayersInputMovement();

	        if (personalInput)
	        {
	            if (Input.GetKeyDown(rightKeyCode) || Input.GetKeyDown(leftKeyCode))
	                StopSliding();
	            //Moving right?
	            if (Input.GetKeyDown(rightKeyCode))
	                horizontalMove(gameManager.SidewayMoveAmount, controlDirection);

	            //Moving left?
	            if (Input.GetKeyDown(leftKeyCode))
	                horizontalMove(-gameManager.SidewayMoveAmount, controlDirection);
	        }
	        var pos = (int) transform.position.z;
	        if (gameManager.Direction > 0)
	        {
	            if (pos > PreviousZpos)
	            {
//					AudioManager.PlaySound("ScoreIncrease");
	                distanceScore++;
	                PreviousZpos = pos;
	            }
	        }
	        else
	        {
	            if (pos < PreviousZpos)
	            {
//					AudioManager.PlaySound("ScoreIncrease");
	                distanceScore++;
	                PreviousZpos = pos;
	            }
	        }

	        PreviousZpos = transform.position.z;
	        moveForward(Time.deltaTime);
	    }
	}

	void FixedUpdate()
	{
		var p = transform.position;
		if (transform.position.x > gameManager.HighBoundry)
		{
			transform.position = new Vector3(gameManager.HighBoundry, p.y, p.z);
			StopSliding();
		}
		else if (transform.position.x < gameManager.LowBoundry)
		{
			transform.position = new Vector3(gameManager.LowBoundry, p.y, p.z);
			StopSliding();
		}

	}

	public void StopSliding()
	{
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}

	private void handleOtherPlayersInputMovement()
	{
		//Others pushing left?
		if (OLKeyCodes.Where(keycode => keycode != leftKeyCode).Where(Input.GetKeyDown).Any())
		{
			horizontalMove(gameManager.SidewayInfluenceAMount, controlDirection);
			return;
		}

		//Others pushing right?
		if (ORKeyCodes.Where(keyCode => keyCode != rightKeyCode).Where(Input.GetKeyDown).Any())
		{
			horizontalMove(-gameManager.SidewayInfluenceAMount, controlDirection);
		}
	}

	private void moveForward(float time)
	{
		transform.position = new Vector3(transform.position.x, transform.position.y ,transform.position.z + ForwardSpeed * moveDirection * time);
	}
	private void horizontalMove(float amount, int direction)
	{
		var tmp = transform.position;
		if (tmp.x + amount * direction < gameManager.LowBoundry || tmp.x + amount * direction > gameManager.HighBoundry)
			return;
		transform.position = new Vector3(tmp.x + (amount*direction), tmp.y, tmp.z);
	}

	private void TurnPlayerAround(float time)
	{
		moveDirection *= -1;
		var ht = new Hashtable {{"y", .5}, {"time", time}};
        iTween.RotateBy(gameObject, ht);
		hurdleScore += gameManager.HurdleHitScore;
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
		ForwardSpeed = gameManager.SlowSpeed;
		speedModifierDuration = time;
		hurdleScore += gameManager.HurdleHitScore;
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
		StopSliding();
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
		TurnPlayerAround(gameManager.TurnSpeed);
		hurdleScore += gameManager.HurdleHitScore;
	}

	public void SetEnviromentalImmunity(float time)
	{
		enviromentImmunity = true;
		enviromentImmunityTimer = 0;
		enviromentImmunityDuration = time;
		col.enabled = false;
		hurdleScore += gameManager.HurdleHitScore;
        spawnObject("ShieldImmunityAll", time);
	}

    private void spawnObject(string name, float time)
    {
        objectOverHead = Resources.Load("Prefabs/" + name) as GameObject;
        if (objectOverHead != null)
        {
            var obj = Instantiate(objectOverHead, new Vector3(transform.position.x, transform.position.y + overHeadPosition, transform.position.z), Quaternion.Euler(0, -90, 0)) as GameObject;
            obj.transform.parent = transform;
            Destroy(obj, time);
        }
    }

    public void SetReversePersonalInput(float time)
	{
		if (reverseControlTimer < 0)
			controlDirection *= -1;
		reverseControlDuration = time;
		reverseControlTimer = 0;
        spawnObject("birds", time);
	}

    public void SetReverseOthersInput(float time)
    {
        gameManager.IhitReverseOtherInput(time, ID);
    }

	public void SetDirection(int dir)
	{
		moveDirection = dir;
		controlDirection = dir;
	}

	public void SetReverseAll(Vector3 powerupPos)
	{
	    if (!gameManager.turning)
	    {
	        gameManager.IhitReverseAll(powerupPos.z);
	        hurdleScore += gameManager.HurdleHitScore;
	    }
	}

	public void SetReverseDirection()
	{
		TurnPlayerAround(gameManager.TurnSpeed);
		controlDirection *= -1;
		hurdleScore += gameManager.HurdleHitScore;
	}

	public void SetBoost(float time)
	{
		animation.Play("Fall to slide");
		StartCoroutine(StartAnimation("Slide", 0.5f));
		StartCoroutine(StartAnimation("Rise for slide", 1f));
		speedModifierTimer = 0;
		ForwardSpeed = gameManager.BoostSpeed;
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
