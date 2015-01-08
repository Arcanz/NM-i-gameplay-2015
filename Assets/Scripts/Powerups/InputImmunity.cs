using UnityEngine;

public class InputImmunity : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("InputSheild", gameObject);
		playerCollider.GetComponent<Player>().SetOtherInputImmunity(2f);
		Trigger();
	}
	void Update()
	{
		gameObject.transform.Rotate(0,2,0);

		if (triggered)
		{
			Fabric.Component comp = Fabric.FabricManager.Instance.GetComponentByName("InputSheild");
			if (comp != null)
			{
				if(comp.IsPlaying() == false)
					Despawn();
			}
		}

	}
}
	
