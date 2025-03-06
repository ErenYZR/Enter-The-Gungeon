using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public Transform firePoint;
    public GameObject bulletPrefab;

    [SerializeField] private int currentAmmo;
	[SerializeField] private int currentClipAmmo;
	[SerializeField] private bool isReloading;


	private void Start()
	{
        currentAmmo = weaponData.maxAmmo;
        currentClipAmmo = weaponData.clipAmmo;
	}
	public virtual void Fire()
    {
        if (isReloading || currentClipAmmo <= 0) return;

		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		bullet.GetComponent<Rigidbody2D>().velocity = firePoint.up * weaponData.bulletSpeed;
        bullet.GetComponent<Bullet>().damage = weaponData.damage;
        currentClipAmmo -= 1;
    }

    public virtual void Reload()
    {
        if(!isReloading && currentAmmo > 0 && currentClipAmmo < weaponData.clipAmmo)
        {
            StartCoroutine(ReloadCoroutine());
            print("Weapon.Reload çalýþýyor");
        }
    }

    private IEnumerator ReloadCoroutine()
    {
		isReloading = true;
		yield return new WaitForSeconds(weaponData.reloadTime);

		int ammoNeeded = weaponData.clipAmmo - currentClipAmmo;
		if (currentAmmo >= ammoNeeded)
        {
            currentAmmo -= ammoNeeded;
            currentClipAmmo = weaponData.clipAmmo;
        }
        else
        {
            currentClipAmmo += currentAmmo;
			currentAmmo = 0;
		}
        isReloading = false;
    }

	public bool CanReload()
	{
		return !isReloading && currentAmmo > 0 && currentClipAmmo < weaponData.clipAmmo;
	}

    public int GetCurrentAmmo() => currentAmmo;
    public int GetCurrentClipAmmo() => currentClipAmmo;
    public bool IsReloading() => isReloading;
}
