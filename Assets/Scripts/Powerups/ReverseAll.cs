using UnityEngine;

public class ReverseAll : IPickupable 
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("ReverseMap", gameObject);
        playerCollider.GetComponent<Player>().SetReverseAll(gameObject.transform.position);
		Trigger();
	}
	void Update()
	{
		gameObject.transform.Rotate(0,0,2);

		if (triggered)
		{
			Fabric.Component comp = Fabric.FabricManager.Instance.GetComponentByName("ReverseMap");
			if (comp != null)
			{
				if(comp.IsPlaying() == false)
					Despawn();
			}
		}

	}
}
