using UnityEngine;

public class Death : IPickupable
{

    private void OnTriggerEnter(Collider collider)
    {
		AudioManager.PlaySound("BombExplosion", gameObject);
        collider.gameObject.GetComponent<Player>().SetDead();
        Despawn();
    }
}
