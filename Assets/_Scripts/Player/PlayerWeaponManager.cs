using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public Weapon[] weapons;
    private int currentWeaponIndex = 0;
    [SerializeField] private Weapon currentWeapon;
    private PlayerAimWeapon playerAimWeapon;
    [SerializeField] private float weaponChangeCooldown;
    private Coroutine reloadCoroutine;

    public Action OnWeaponChange;

    void Start()
    {
		playerAimWeapon = GetComponent<PlayerAimWeapon>();
		EquipWeapon(0);
    }

	private void OnEnable()
	{
		
	}

	private void OnDisable()
	{
		
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
            if(currentWeapon.IsReloading())//e�er silah de�i�irken reload at�l�yorsa reload� iptal ediyor
            {
                currentWeapon.StopAllCoroutines();
				WeaponEvents.TriggerReloadCancel();
			}
        }
        currentWeaponIndex = index;
        currentWeapon = weapons[currentWeaponIndex];
		currentWeapon.gameObject.SetActive(true);
		currentWeapon.spriteRenderer.sprite = currentWeapon.weaponData.inGameIcon;
        OnWeaponChange?.Invoke();
	}

    void SwapWeapon()
    {
        currentWeapon.isReloading = false;//silah de�i�meden �nce reload� durdurman�n bir par�as�
        WeaponEvents.TriggerReloadCancel();
        int nextWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
        EquipWeapon(nextWeaponIndex);
		playerAimWeapon.fireTimer = weaponChangeCooldown;

        FindObjectOfType<AmmoUI>().UpdateAmmoUI();
	}

	public void AddNewWeapon(WeaponData weaponData)
	{
		// Silah prefab�n� olu�tur (weaponData i�inde prefab referans� varsa)
		GameObject weaponGO = Instantiate(weaponData.weaponPrefab, transform);
		Weapon newWeapon = weaponGO.GetComponent<Weapon>();
		newWeapon.weaponData = weaponData;

		// Sprite Renderer ata (e�er prefabta atanmad�ysa)
		newWeapon.spriteRenderer = weaponGO.GetComponent<SpriteRenderer>();

		// Silah� listeye ekle
		List<Weapon> tempList = new List<Weapon>(weapons);
		tempList.Add(newWeapon);
		weapons = tempList.ToArray();

		// Yeni silah� direkt ku�anmak istersen:
		EquipWeapon(weapons.Length - 1);
	}


	public void Shoot()
    {
        if(currentWeapon != null && playerAimWeapon.fireTimer < 0 && !currentWeapon.IsReloading() && currentWeapon.GetCurrentClipAmmo() > 0)
        {
            currentWeapon.firePoint = playerAimWeapon.aimGunEndPositionTransform;
            currentWeapon.Fire();
            playerAimWeapon.fireTimer = currentWeapon.weaponData.fireRate;
            print("Weapon manager shoot");
            //currentWeapon.weaponData.currentClipAmmo--; gpt kald�rtt�
        }
    }

    public void Reload()
    {
		if (currentWeapon != null && currentWeapon.CanReload() && reloadCoroutine == null)
		{
			reloadCoroutine = StartCoroutine(ReloadCoroutine());
			WeaponEvents.TriggerReloadStart(currentWeapon.weaponData.reloadTime); // Event �a�r�l�yor
		}
	}

	private IEnumerator ReloadCoroutine()
	{
        currentWeapon.Reload();
		print("Reload ba�lad�...");
		yield return new WaitForSeconds(currentWeapon.weaponData.reloadTime);

		reloadCoroutine = null;
		print("Reload bitti!");
		WeaponEvents.TriggerReloadFinish();
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
