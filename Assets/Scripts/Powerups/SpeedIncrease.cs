using UnityEngine;

public class SpeedIncrease : IPickupable
{
	void OnTriggerEnter(Collider playerCollider)
	{
		playerCollider.GetComponent<Player>().SetBoost(2f);
		Despawn();
	}

    void Update()
    {
        float offset = Time.time * 1f;
        renderer.material.mainTextureOffset = new Vector2(0,-offset);
    }
}
