using UnityEngine;

public class WolfTrap : MonoBehaviour
{
	[SerializeField] private float trapDuration = 5f;
	private bool isUsed = false;
	[SerializeField] Animator anim;

	private void OnEnable()
	{
		anim = GetComponent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!isUsed && collision.CompareTag("Player"))
		{
			PlayerMovement player = collision.GetComponent<PlayerMovement>();
			if (player != null)
			{
				anim.SetTrigger("Activated");

				WolfTrapEffect effect = new WolfTrapEffect(player, trapDuration, this);
				player.AddStatusEffect(effect);
				isUsed = true; // Tekrar kullanýlmasýný engelle
			}
		}
	}
	public void PlayRelaseAnimation()
	{
		anim.SetTrigger("Relased");
	}
}
