using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAIAgentAllDirection : EnemyShooterBase
{
	[SerializeField] float bulletNumber;
	protected override void Shoot()
	{
		for (int i = 0; i < bulletNumber; i++)
		{
			float spread = i * 360f / bulletNumber ;
			Quaternion bulletRotation = Quaternion.Euler(0, 0, firingPoint.rotation.eulerAngles.z + spread);
			GameObject bullet = Instantiate(enemyBulletPrefab, firingPoint.position, bulletRotation);
			EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
			enemyBullet.SetDamage(bulletDamage);
			enemyBullet.SetSpeed(bulletSpeed);
		}
	}
}
