using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAIAgentShoot : EnemyShooterBase
{
	[SerializeField] private int pelletCount;
	[SerializeField] private float spreadAngle;

	protected override void Shoot()
	{
		if (timeToFire <= 0f && canShoot())
		{
			for (int i = 0; i < pelletCount; i++)
			{
				float spread = Random.Range(-spreadAngle / 2, spreadAngle / 2);
				Quaternion bulletRotation = Quaternion.Euler(0, 0, firingPoint.rotation.eulerAngles.z + spread);
				Instantiate(enemyBulletPrefab, firingPoint.position, bulletRotation);
			}
			timeToFire = fireRate;
		}
		else
		{
			timeToFire -= Time.deltaTime;
		}
	}
}
