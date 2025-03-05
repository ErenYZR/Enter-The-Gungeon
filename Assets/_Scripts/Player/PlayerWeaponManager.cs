using System.Collections;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public Weapon[] weapons;
    private int currentWeaponIndex = 0;
    private Weapon currentWeapon;
    private PlayerAimWeapon playerAimWeapon;
    [SerializeField] float weaponChangeCooldown;
    private Coroutine reloadCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        EquipWeapon(0);
        playerAimWeapon = GetComponent<PlayerAimWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwapWeapon();
        }
        print(currentWeapon.name);

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void EquipWeapon(int index)
    {
        if(currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);
            if(reloadCoroutine != null)
            {
                StopCoroutine(reloadCoroutine);
                currentWeapon.weaponData.isReloading = false;
                reloadCoroutine = null;
            }
        }
        currentWeaponIndex = index;
        currentWeapon = weapons[currentWeaponIndex];
        currentWeapon.gameObject.SetActive(true);
	}

    void SwapWeapon()
    {
        int nextWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
        EquipWeapon(nextWeaponIndex);
		playerAimWeapon.fireTimer = weaponChangeCooldown;
	}

    public void Shoot()
    {
        if(currentWeapon != null && playerAimWeapon.fireTimer < 0 && !currentWeapon.weaponData.isReloading && currentWeapon.weaponData.currentClipAmmo>0)
        {
            currentWeapon.firePoint = playerAimWeapon.aimGunEndPositionTransform;
            currentWeapon.Fire();
            playerAimWeapon.fireTimer = currentWeapon.weaponData.fireRate;
            print("Weapon manager shoot");
            currentWeapon.weaponData.currentClipAmmo--;
        }
    }

    public void Reload()
    {
		if (currentWeapon != null && currentWeapon.CanReload() && reloadCoroutine == null)
		{
			reloadCoroutine = StartCoroutine(ReloadCoroutine());
		}
	}

	private IEnumerator ReloadCoroutine()
	{
		currentWeapon.weaponData.isReloading = true;
		print("Reload baþladý...");
		yield return new WaitForSeconds(currentWeapon.weaponData.reloadTime);

		int ammoNeeded = currentWeapon.weaponData.clipAmmo - currentWeapon.weaponData.currentClipAmmo;

		if (currentWeapon.weaponData.currentAmmo >= ammoNeeded)
		{
			currentWeapon.weaponData.currentAmmo -= ammoNeeded;
			currentWeapon.weaponData.currentClipAmmo = currentWeapon.weaponData.clipAmmo;
		}
		else
		{
			currentWeapon.weaponData.currentClipAmmo += currentWeapon.weaponData.currentAmmo;
			currentWeapon.weaponData.currentAmmo = 0;
		}

		currentWeapon.weaponData.isReloading = false;
		reloadCoroutine = null;
		print("Reload bitti!");
	}

}
