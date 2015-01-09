using UnityEngine;

public class Death : IPickupable
{
    private void OnTriggerEnter(Collider collider)
    {
		AudioManager.PlaySound("FX/Powerups/Bomb", gameObject);
	    var ob = Resources.Load("Prefabs/Detonator/Detonator-Ignitor");
	    Instantiate(ob, gameObject.transform.position, Quaternion.identity);
        collider.gameObject.GetComponent<Player>().SetDead();
	    Trigger();
    }

	void Update()
	{
		if (triggered)
		{
			Fabric.Component comp = Fabric.FabricManager.Instance.GetComponentByName("FX/Powerups/Bomb");
			if (comp != null)
			{
				if(comp.IsPlaying() == false)
					Despawn();
			}
		}

	}
}
