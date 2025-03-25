using System.Collections;
using UnityEngine;

public class SniperEnemy : EnemyShooterBase
{
	[Header("Sniper Settings")]
	public LineRenderer laserLine;
	public float laserLength = 10f;
	public float aimDuration = 0.4f; // Niþan alma süresi
	private bool isAiming = false;
	[SerializeField] private LayerMask lazerLayerMask;

	protected override void Start()
	{
		base.Start();
		laserLine.enabled = true;
		laserLine.startColor = Color.red;
		laserLine.endColor = Color.red;
		laserLine.positionCount = 2;
		//StartCoroutine(SniperRoutine());
	}

	protected override void Update()
	{
		base.Update();
		UpdateLaser();
		if (canShoot() && !isAiming) StartCoroutine(SniperRoutine());
	}

	private void UpdateLaser()
	{
		// Lazerin baþlangýç noktasý düþmanýn silah noktasý
		Vector3 start = firingPoint.position + new Vector3(0,0,0);
		Vector3 direction = transform.up; // Sað yön bakýþ yönü olur

		// Lazerin nereye kadar gittiðini kontrol etmek için raycast kullan
		RaycastHit2D hit = Physics2D.Raycast(start, direction, laserLength, lazerLayerMask);

		Vector3 end = hit.collider ? hit.point : start + direction * laserLength;

		laserLine.SetPosition(0, start);
		laserLine.SetPosition(1, end);
	}

	private IEnumerator SniperRoutine()
	{			
			isAiming = true;
			laserLine.startColor = Color.yellow;
			laserLine.endColor = Color.yellow;

			yield return new WaitForSeconds(aimDuration);

			Shoot();
			

			laserLine.startColor = Color.red;
			laserLine.endColor = Color.red;

			yield return new WaitForSeconds(fireRate);
			isAiming = false;	
	}
	protected override void Shoot()
	{
		// Burada normal sniper mermisini instantiate edebilirsin
		Quaternion bulletRotation = Quaternion.Euler(0, 0, 0);
		GameObject bullet = Instantiate(enemyBulletPrefab, firingPoint.position,transform.rotation);
		EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
		enemyBullet.SetDamage(bulletDamage);
		enemyBullet.SetSpeed(bulletSpeed);
	}
}
