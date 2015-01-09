using UnityEngine;

public class InputImmunity : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("FX/Powerups/Immunity-Input", gameObject);
		playerCollider.GetComponent<Player>().SetOtherInputImmunity(2f);
		Trigger();
	}
	void Update()
	{
		gameObject.transform.Rotate(0,2,0);

		if (triggered)
		{
			Fabric.Component comp = Fabric.FabricManager.Instance.GetComponentByName("FX/Powerups/Immunity-Input");
			if (comp != null)
			{
				if(comp.IsPlaying() == false)
					Despawn();
			}
		}

	}
}
	
