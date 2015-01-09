using UnityEngine;

public class Slow : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("SpeedSlow", gameObject);
		playerCollider.GetComponent<Player>().SetSlow(2f);
		Trigger();
	}
	void Update()
	{
		if (triggered)
		{
			Fabric.Component comp = Fabric.FabricManager.Instance.GetComponentByName("SpeedSlow");
			if (comp != null)
			{
				if (comp.IsPlaying() == false)
					Despawn();
			}
		}

	}
}
