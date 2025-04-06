using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAIAgent1 : EnemyShooterBase
{
	private Rigidbody2D targetRB;
	protected override void Start()
	{
		base.Start();
		targetRB = target.GetComponent<Rigidbody2D>();
	}

	protected override void Shoot() 
	{
		float distance = Vector2.Distance(target.position, transform.position);
		float timeToHit = distance / bulletSpeed;

		Vector2 targetVelocity = targetRB.velocity; // Oyuncunun hýzý
		Vector2 predictedPosition = (Vector2)target.position + targetVelocity * timeToHit; // Gelecekteki konum

		Vector2 direction2 = predictedPosition - (Vector2)transform.position;

		float angle2 = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg;
		Quaternion bulletRotation = Quaternion.Euler(0, 0, angle2-90);


		if (timeToFire <= 0f && canShoot())
		{
			GameObject bullet = Instantiate(enemyBulletPrefab, firingPoint.position, bulletRotation);
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
