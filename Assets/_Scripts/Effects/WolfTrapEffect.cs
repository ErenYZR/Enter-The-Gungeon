using UnityEngine;

public class WolfTrapEffect : StatusEffect
{
	private WolfTrap trap; // Oyuncunun yakaland��� tuzak

	private int requiredClicks = 5; // Kurtulmak i�in gereken sa� t�klama say�s�
	private int currentClicks = 0;

	public WolfTrapEffect(PlayerMovement player, float duration, WolfTrap trap) : base(player, duration) 
	{
		this.trap = trap;
	}

	public override void ApplyEffect()
	{
		player.SetMovementState(false); // Oyuncuyu durdur
		Debug.Log("Oyuncu Wolf Trap'e yakaland�!");
	}

	public override void RemoveEffect()
	{
		player.SetMovementState(true); // Oyuncuyu serbest b�rak
		Debug.Log("Oyuncu Wolf Trap'ten kurtuldu!");

		// WolfTrap animasyonuna eri� ve "Release" animasyonunu oynat
		//WolfTrap trap = player.GetComponentInChildren<WolfTrap>(); // Veya ba�ka bir referans y�ntemi
		if (trap != null)
		{
			trap.PlayRelaseAnimation();
		}
	}

	public override void UpdateEffect(float deltaTime)
	{
		base.UpdateEffect(deltaTime);

		if (Input.GetMouseButtonDown(1)) // Sa� t�klama ile kurtulmaya �al��
		{
			currentClicks++;
			Debug.Log("Kurtulmak i�in kalan t�klama: " + (requiredClicks - currentClicks));

			if (currentClicks >= requiredClicks)
			{
				RemoveEffect();
				player.RemoveStatusEffect(this);
			}
		}
	}
}
