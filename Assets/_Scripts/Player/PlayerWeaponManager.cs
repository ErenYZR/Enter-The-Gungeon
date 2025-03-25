using System.Collections;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public Weapon[] weapons;
    private int currentWeaponIndex = 0;
    [SerializeField] private Weapon currentWeapon;
    private PlayerAimWeapon playerAimWeapon;
    [SerializeField] private float weaponChangeCooldown;
    private Coroutine reloadCoroutine;

    void Start()
    {
		playerAimWeapon = GetComponent<PlayerAimWeapon>();
		EquipWeapon(0);
    }

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
                reloadCoroutine = null;
            }
        }
        currentWeaponIndex = index;
        currentWeapon = weapons[currentWeaponIndex];
		currentWeapon.gameObject.SetActive(true);
		currentWeapon.spriteRenderer.sprite = currentWeapon.weaponData.inGameIcon;
	}

    void SwapWeapon()
    {
        int nextWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
        EquipWeapon(nextWeaponIndex);
		playerAimWeapon.fireTimer = weaponChangeCooldown;

        FindObjectOfType<AmmoUI>().UpdateAmmoUI();
	}

    public void Shoot()
    {
        if(currentWeapon != null && playerAimWeapon.fireTimer < 0 && !currentWeapon.IsReloading() && currentWeapon.GetCurrentClipAmmo() > 0)
        {
            currentWeapon.firePoint = playerAimWeapon.aimGunEndPositionTransform;
            currentWeapon.Fire();
            playerAimWeapon.fireTimer = currentWeapon.weaponData.fireRate;
            print("Weapon manager shoot");
            //currentWeapon.weaponData.currentClipAmmo--; gpt kaldýrttý
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
        currentWeapon.Reload();
		print("Reload baþladý...");
		yield return new WaitForSeconds(currentWeapon.weaponData.reloadTime);

		reloadCoroutine = null;
		print("Reload bitti!");
        FindObjectOfType<AmmoUI>().UpdateAmmoUI();
	}

    public void RefillCurrentWeaponAmmo()
    {
        currentWeapon.RefillCurrentWeaponAmmo();
    }

    public void GetCurrentAmmo()
    {
        currentWeapon.GetCurrentAmmo();
    }

    public bool GunIsFull()
    {
        return currentWeapon.GunIsFull();
    }

    public int GetCurrentWeaponIndex()
    {
        return currentWeaponIndex;
    }

}
