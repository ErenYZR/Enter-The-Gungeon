using UnityEngine;

public abstract class EnemyShooterBase : EnemyBase
{
	[SerializeField] protected GameObject enemyBulletPrefab;
	[SerializeField] protected int bulletDamage;
	[SerializeField] protected float bulletSpeed;
	[SerializeField] protected Transform firingPoint;
	[SerializeField] protected float fireRate;
	protected float timeToFire;

	protected override void Attack()
	{
		if (timeToFire <= 0f && target != null)
		{
			Shoot();
			timeToFire = fireRate;
		}
		else
		{
			timeToFire -= Time.deltaTime;
		}
	}

	protected abstract void Shoot();
}
