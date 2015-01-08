using UnityEngine;

public class InputLock : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("FX/Powerups/Flask", gameObject);
		playerCollider.GetComponent<Player>().SetNoPersonalInput(2f);
		Despawn();
	}
	void Update()
	{
		if (triggered)
		{
			Fabric.Component comp = Fabric.FabricManager.Instance.GetComponentByName("FX/Powerups/Flask");
			if (comp != null)
			{
				if (comp.IsPlaying() == false)
					Despawn();
			}
		}

	}
}
