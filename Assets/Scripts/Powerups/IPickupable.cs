using UnityEngine;

public class IPickupable : MonoBehaviour 
{
    protected void Despawn()
    {
        Destroy(gameObject);
    }
}
