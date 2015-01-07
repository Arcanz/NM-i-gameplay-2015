using UnityEngine;

public class Death : IPickupable
{

    private void OnTriggerEnter(Collider collider)
    {
        collider.gameObject.GetComponent<Player>().SetDead();
        Despawn();
    }
}
