using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public SpriteRenderer spriteRenderer;

    [SerializeField] protected int currentAmmo;
	[SerializeField] protected int currentClipAmmo;
	[SerializeField] public bool isReloading;

    private Coroutine reloadCoroutine;

	private void Start()
	{
        currentAmmo = weaponData.maxAmmo;
        currentClipAmmo = weaponData.clipAmmo;
	}
	private void OnEnable()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	public virtual void Fire()
    {
        if (isReloading || currentClipAmmo <= 0) return;

		float spread = Random.Range(-weaponData.spreadAngle / 2, weaponData.spreadAngle / 2);
		Quaternion bulletRotation = Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + spread);

		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
        //bullet.GetComponent<Rigidbody2D>().velocity = firePoint.up * weaponData.bulletSpeed;
        bullet.GetComponent<Bullet>().speed = weaponData.bulletSpeed;
		bullet.GetComponent<Bullet>().damage = weaponData.damage;
        currentClipAmmo -= 1;
    }

    public virtual void Reload()
    {
        if(!isReloading && currentAmmo > 0 && currentClipAmmo < weaponData.clipAmmo)
        {
            reloadCoroutine = StartCoroutine(ReloadCoroutine());
            print("Weapon.Reload çalýþýyor");
        }
    }

    private IEnumerator ReloadCoroutine()
    {
		isReloading = true;
        WeaponEvents.TriggerReloadStart(weaponData.reloadTime);
        float elapsedTime = 0f;

        while(elapsedTime < weaponData.reloadTime)
        {
            elapsedTime += Time.deltaTime;

            if (!gameObject.activeSelf)
            {
                isReloading = false;
                WeaponEvents.TriggerReloadCancel();
                yield break;
            }
            yield return null;
        }
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
        WeaponEvents.TriggerReloadFinish();
    }

    public virtual void RefillCurrentWeaponAmmo()
    {
        currentAmmo = weaponData.maxAmmo;
        currentClipAmmo = weaponData.clipAmmo;
    }

	public bool CanReload()
	{
		return !isReloading && currentAmmo > 0 && currentClipAmmo < weaponData.clipAmmo;
	}

    public bool GunIsFull()
    {
        return currentAmmo == weaponData.maxAmmo;
    }

    public int GetCurrentAmmo() => currentAmmo;
    public int GetCurrentClipAmmo() => currentClipAmmo;
    public bool IsReloading() => isReloading;
}
