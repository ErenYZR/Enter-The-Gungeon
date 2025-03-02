using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAIAgentShoot : EnemyShooterBase
{
	protected override void Shoot()
	{
		if (timeToFire <= 0f && canShoot())
		{
			Instantiate(enemyBulletPrefab, firingPoint.position, transform.rotation);
			timeToFire = fireRate;
		}
		else
		{
			timeToFire -= Time.deltaTime;
		}
	}
}
