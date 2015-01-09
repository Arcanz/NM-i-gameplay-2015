using UnityEngine;

public class ImmunityAll : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("FX/Powerups/Immunity-All", gameObject);
		playerCollider.GetComponent<Player>().SetEnviromentalImmunity(2f);
		Trigger();
	}
	
	void Update()
	{
		gameObject.transform.Rotate(0,2,0);

		if (triggered)
		{
			Fabric.Component comp = Fabric.FabricManager.Instance.GetComponentByName("FX/Powerups/Immunity-All");
			if (comp != null)
			{
				if(comp.IsPlaying() == false)
					Despawn();
			}
		}

	}
}
