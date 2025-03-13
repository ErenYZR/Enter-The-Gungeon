using UnityEngine;

[CreateAssetMenu(fileName ="NewWeapon", menuName ="Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
	public string weaponName;
	public Sprite inventoryIcon;
	public Sprite inGameIcon;
	public int damage;
	public float fireRate;
	public float bulletSpeed;
	public int maxAmmo;
	public int clipAmmo;
	public float reloadTime;
	public float spreadAngle;
}
