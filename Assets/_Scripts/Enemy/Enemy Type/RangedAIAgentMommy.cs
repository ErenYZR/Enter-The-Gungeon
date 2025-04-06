using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAIAgentMommy : EnemyShooterBase
{
	[SerializeField] private float spreadAngle;

	protected override void Shoot()
	{
		if (timeToFire <= 0f && canShoot())
		{
			for (int i = 0; i < 2; i++)
			{

				float spread = Random.Range(-spreadAngle / 2, spreadAngle / 2);
				Quaternion bulletRotation = Quaternion.Euler(0, 0, firingPoint.rotation.eulerAngles.z + (Mathf.Pow(-1,i)* spreadAngle / 2f));//i 1'ken negatif 2 iken pozitif olacak bu sayede + ve - spreadangle/2 açýlarýný elde etmiþ olacaðýz
				GameObject bullet = Instantiate(enemyBulletPrefab, firingPoint.position, bulletRotation);
				EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
				enemyBullet.SetDamage(bulletDamage);
				enemyBullet.SetSpeed(bulletSpeed);
			}
			timeToFire = fireRate;
		}
		else
		{
			timeToFire -= Time.deltaTime;
		}
	}
}
