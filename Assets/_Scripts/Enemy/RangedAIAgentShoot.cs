using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAIAgentShoot : EnemyShooterBase
{
	protected override void Shoot()
	{
		if (timeToFire <= 0f && canShoot())
		{
			GameObject bullet = Instantiate(enemyBulletPrefab, firingPoint.position, transform.rotation);
			EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
			enemyBullet.SetDamage(bulletDamage);
			enemyBullet.SetSpeed(bulletSpeed);
			timeToFire = fireRate;
		}
		else
		{
			timeToFire -= Time.deltaTime;
		}
	}
}
