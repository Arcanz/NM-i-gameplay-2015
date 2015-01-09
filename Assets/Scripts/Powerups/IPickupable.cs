using UnityEngine;

public class IPickupable : MonoBehaviour
{
	public bool triggered = false;


	protected void Trigger()
	{
		triggered = true;
		Hide();
	}
	protected void Hide()
	{
		renderer.enabled = false;
		collider.enabled = false;
	}
    protected void Despawn()
    {
        Destroy(gameObject);
    }
}
