using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
	public TextMeshProUGUI ammoText; // UI'deki metin
	public Image ammoSprite;
	private PlayerWeaponManager weaponManager;

	void Start()
	{
		weaponManager = FindObjectOfType<PlayerWeaponManager>();
		UpdateAmmoUI(); // Ba�lang��ta UI'yi g�ncelle

		if(weaponManager != null)
		{
			weaponManager.OnWeaponChange += UpdateAmmoUI;
		}

		WeaponEvents.OnReloadFinish += UpdateAmmoUI;
	}

	private void OnDestroy()
	{
		//Memory Leak �nlemek i�in event'leri ��kart
		if (weaponManager != null)
		{
			weaponManager.OnWeaponChange -= UpdateAmmoUI;
		}
		WeaponEvents.OnReloadFinish -= UpdateAmmoUI;
	}

	void Update()
	{
		UpdateAmmoUI();
	}

	public void UpdateAmmoUI()
	{
		if (weaponManager != null && weaponManager.weapons.Length > 0)
		{
			Weapon currentWeapon = weaponManager.weapons[weaponManager.GetCurrentWeaponIndex()];

			if(currentWeapon is not MeeleWeapon)
			{
				ammoText.text = $"{currentWeapon.GetCurrentClipAmmo()} / {currentWeapon.GetCurrentAmmo()}";
				ammoSprite.sprite = currentWeapon.weaponData.ammoIcon;

			}
			else if(currentWeapon is MeeleWeapon meeleWeapon)
			{
				ammoText.text = $"{meeleWeapon.GetCurrentDurability()}";
				ammoSprite.sprite = currentWeapon.weaponData.ammoIcon;
			}

		}
	}
}
