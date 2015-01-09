using UnityEngine;

public class ReverseInput : IPickupable 
{
	void OnTriggerEnter(Collider playerCollider)
	{
		AudioManager.PlaySound("FX/Powerups/Reverse-Input", gameObject);
		playerCollider.GetComponent<Player>().SetReverseOthersInput(2f);
        HideMe();
	}

    void HideMe()
    {
        gameObject.collider.enabled = false;
        var something = GetComponentsInChildren<MeshRenderer>();
        foreach (var vari in something)
        {
            vari.enabled = false;
            
        }
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
