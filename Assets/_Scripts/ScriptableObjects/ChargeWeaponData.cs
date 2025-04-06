using UnityEngine;

[CreateAssetMenu(fileName = "NewChargeWeapon", menuName = "Weapons/Charge Weapon Data")]
public class ChargeWeaponData : WeaponData
{
	public float chargeTime;
	public RuntimeAnimatorController animatiorController;
}
