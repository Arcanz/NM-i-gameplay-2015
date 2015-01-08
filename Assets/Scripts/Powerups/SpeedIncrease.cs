using UnityEngine;

public class SpeedIncrease : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("FX/Powerups/Speed-Increase", gameObject);
		playerCollider.GetComponent<Player>().SetBoost(2f);
		Trigger();
	}
	void Update()
	{
		if (triggered)
		{
			Fabric.Component comp = Fabric.FabricManager.Instance.GetComponentByName("FX/Powerups/Speed-Increase");
			if (comp != null)
			{
				if (comp.IsPlaying() == false)
					Despawn();
			}
		}

	}
}
