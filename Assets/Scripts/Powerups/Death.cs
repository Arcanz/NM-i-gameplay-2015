using UnityEngine;

public class Death : IPickupable
{

    private void OnTriggerEnter(Collider collider)
    {
		AudioManager.PlaySound("FX/Powerups/Bomb", gameObject);
        collider.gameObject.GetComponent<Player>().SetDead();
        Despawn();
    }
}
