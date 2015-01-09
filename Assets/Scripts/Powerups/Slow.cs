using UnityEngine;

public class Slow : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("FX/Powerups/Speed-Slow", gameObject);
		playerCollider.GetComponent<Player>().SetSlow(2f);
		Trigger();
	}
	void Update()
	{
		if (triggered)
		{
			Fabric.Component comp = Fabric.FabricManager.Instance.GetComponentByName("FX/Powerups/Speed-Slow");
			if (comp != null)
			{
				if (comp.IsPlaying() == false)
					Despawn();
			}
		}

	}
}
