using UnityEngine;

[CreateAssetMenu(fileName ="NewWeapon", menuName ="Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
	public string weaponName;
	public int damage;
	public float fireRate;
	public float bulletSpeed;
	public int maxAmmo;
	public int currentAmmo;
	public int clipAmmo;
	public int currentClipAmmo;
	public int reloadTime;
	public bool isReloading;
}
