using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public Transform firePoint;
    public GameObject bulletPrefab;


	private void Start()
	{
        weaponData.currentAmmo = weaponData.maxAmmo;
        weaponData.currentClipAmmo = weaponData.clipAmmo;
	}
	public virtual void Fire()
    {
			GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
			bullet.GetComponent<Rigidbody2D>().velocity = firePoint.up * weaponData.bulletSpeed;
            bullet.GetComponent<Bullet>().damage = weaponData.damage;
    }

    public virtual void Reload()
    {
        if(!weaponData.isReloading && weaponData.currentAmmo > 0 && weaponData.currentClipAmmo < weaponData.clipAmmo)
        {
            StartCoroutine(ReloadCoroutine());
            print("Weapon.Reload çalýþýyor");
        }
    }

    private IEnumerator ReloadCoroutine()
    {
		weaponData.isReloading = true;
		yield return new WaitForSeconds(weaponData.reloadTime);

		int ammoNeeded = weaponData.clipAmmo - weaponData.currentClipAmmo;
		if (weaponData.currentAmmo >= ammoNeeded)
        {
            weaponData.currentAmmo -= ammoNeeded;
            weaponData.currentClipAmmo = weaponData.clipAmmo;
        }
        else
        {
            weaponData.currentAmmo -= ammoNeeded;
            weaponData.currentClipAmmo += weaponData.currentAmmo;
        }
        weaponData.isReloading = false;
    }

	public bool CanReload()
	{
		return !weaponData.isReloading && weaponData.currentAmmo > 0 && weaponData.currentClipAmmo < weaponData.clipAmmo;
	}
}
