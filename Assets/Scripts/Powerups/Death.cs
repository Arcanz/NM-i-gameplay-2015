using UnityEngine;

public class Death : IPickupable
{

    private void OnTriggerEnter(Collider collider)
    {
		AudioManager.PlaySound("BombExplosion", transform.position);
        collider.gameObject.GetComponent<Player>().SetDead();
        Despawn();
    }
}
