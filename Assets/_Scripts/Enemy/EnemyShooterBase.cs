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

	protected override void Attack()
	{
		if (timeToFire <= 0f && target != null && canShoot())
		{
			print("B");
			//StartCoroutine(Wait());
		}
		else
		{
			timeToFire -= Time.deltaTime;
		}
	}

	protected abstract void Shoot();

	private IEnumerator Wait()
	{
		print("A");
		spriteRenderer.color = Color.white;
		yield return new WaitForSeconds(0.4f);
		Shoot();
		timeToFire = fireRate;
		spriteRenderer.color = originalColor;
	}
}
