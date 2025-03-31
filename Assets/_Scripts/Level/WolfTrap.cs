using UnityEngine;

public class WolfTrap : MonoBehaviour
{
	[SerializeField] private float trapDuration = 5f;
	private bool isUsed = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!isUsed && collision.CompareTag("Player"))
		{
			PlayerMovement player = collision.GetComponent<PlayerMovement>();
			if (player != null)
			{
				WolfTrapEffect effect = new WolfTrapEffect(player, trapDuration);
				player.AddStatusEffect(effect);
				isUsed = true; // Tekrar kullanýlmasýný engelle
			}
		}
	}
}
