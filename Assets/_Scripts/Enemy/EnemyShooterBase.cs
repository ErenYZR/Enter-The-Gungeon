using System.Collections;
using UnityEngine;

public abstract class EnemyShooterBase : EnemyBase
{
	[SerializeField] protected GameObject enemyBulletPrefab;
	[SerializeField] protected int bulletDamage;
	[SerializeField] protected float bulletSpeed;
	[SerializeField] protected Transform firingPoint;
	[SerializeField] protected float fireRate;
	protected float timeToFire;
	protected bool isFiring = false;


	protected override void Update()
	{
		base.Update();
		if (canShoot()) RotateTowardsTarget();
		Attack();
		
	}
	protected override void Attack()
	{
		if (timeToFire <= 0f && target != null && canShoot() && !isFiring)
		{
			print("B");
			StartCoroutine(Wait());
		}
		else
		{
			timeToFire -= Time.deltaTime;
		}
	}

	protected abstract void Shoot();

	private IEnumerator Wait()
	{
		isFiring = true;
		print("A");
		spriteRenderer.color = Color.white;
		yield return new WaitForSeconds(0.4f);
		Shoot();
		timeToFire = fireRate;
		spriteRenderer.color = originalColor;
		isFiring = false;
	}
}
