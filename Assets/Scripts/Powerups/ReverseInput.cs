using UnityEngine;

public class ReverseInput : IPickupable 
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("ReverseInput", gameObject);
		playerCollider.GetComponent<Player>().SetReverseOthersInput(2f);
		Trigger();
	}
	void Update()
	{
		gameObject.transform.Rotate(0,2,0);
		if (triggered)
		{
			Fabric.Component comp = Fabric.FabricManager.Instance.GetComponentByName("ReverseInput");
			if (comp != null)
			{
				if (comp.IsPlaying() == false)
					Despawn();
			}
		}

	}
}
