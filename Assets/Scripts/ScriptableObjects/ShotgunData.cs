using UnityEngine;

[CreateAssetMenu(fileName = "NewShotgun", menuName = "Weapons/Shotgun Data")]
public class ShotgunData : WeaponData
{
	public int pelletCount; // Kaç mermi saçýlýyor?
	public float spreadAngle; // Saçýlma açýsý
}
