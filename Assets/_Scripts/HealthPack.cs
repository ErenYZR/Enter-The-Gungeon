using UnityEngine;

public class HealthPack : MonoBehaviour, IObtainable
{
	public int healthAmount = 25;

	public void Obtain(GameObject player)
	{
		Health health = player.GetComponent<Health>();
		if (health != null && !health.IsFullHealth())
		{
			health.Heal(healthAmount);
			Destroy(gameObject);
		}
	}
}