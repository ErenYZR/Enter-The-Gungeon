using UnityEngine;

public class Shotgun : Weapon
{
	public ShotgunData shotgunData;


	public override void Fire()
	{
		for (int i = 0; i < shotgunData.pelletCount; i++)
		{
			float spread = Random.Range(-shotgunData.spreadAngle / 2, shotgunData.spreadAngle / 2);
			Quaternion bulletRotation = Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + spread);
			GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
			bullet.GetComponent<Rigidbody2D>().velocity = bulletRotation * Vector2.up * weaponData.bulletSpeed;
		}
		currentClipAmmo -= 1;
	}
}