using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleWeapon : Weapon
{
    public MeeleWeaponData meeleWeaponData;
	[SerializeField] private Animator animator;


	private void Awake()
	{
		animator = GetComponent<Animator>();

		if(meeleWeaponData.animatiorController != null)
		{
			animator.runtimeAnimatorController = meeleWeaponData.animatiorController;
		}
	}

	public override void Fire()
	{
		animator.SetTrigger("Attack");
	}
}
