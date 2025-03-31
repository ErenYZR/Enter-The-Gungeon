using UnityEngine;

public abstract class StatusEffect
{
	protected PlayerMovement player;
	protected float duration;

	public StatusEffect(PlayerMovement player, float duration)
	{
		this.player = player;
		this.duration = duration;
	}

	public abstract void ApplyEffect();
	public abstract void RemoveEffect();

	public virtual void UpdateEffect(float deltaTime)
	{
		duration -= deltaTime;
		if (duration <= 0)
		{
			RemoveEffect();
			player.RemoveStatusEffect(this);
		}
	}
}
