using UnityEngine;

public class WolfTrapEffect : StatusEffect
{
	private int requiredClicks = 5; // Kurtulmak için gereken sað týklama sayýsý
	private int currentClicks = 0;

	public WolfTrapEffect(PlayerMovement player, float duration) : base(player, duration) { }

	public override void ApplyEffect()
	{
		player.SetMovementState(false); // Oyuncuyu durdur
		Debug.Log("Oyuncu Wolf Trap'e yakalandý!");
	}

	public override void RemoveEffect()
	{
		player.SetMovementState(true); // Oyuncuyu serbest býrak
		Debug.Log("Oyuncu Wolf Trap'ten kurtuldu!");
	}

	public override void UpdateEffect(float deltaTime)
	{
		base.UpdateEffect(deltaTime);

		if (Input.GetMouseButtonDown(1)) // Sað týklama ile kurtulmaya çalýþ
		{
			currentClicks++;
			Debug.Log("Kurtulmak için kalan týklama: " + (requiredClicks - currentClicks));

			if (currentClicks >= requiredClicks)
			{
				RemoveEffect();
				player.RemoveStatusEffect(this);
			}
		}
	}
}
