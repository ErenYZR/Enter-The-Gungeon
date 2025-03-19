using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleWeapon : Weapon
{
    public MeeleWeaponData meeleWeaponData;
	[SerializeField] private Animator animator;

	public Transform circleOrigin;
	public float radius;
	public bool isAttacking = false;


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
		isAttacking = true;
		animator.SetTrigger("Attack");
	}

	private void Update()
	{
		if (isAttacking)
		{
			DetectColliders();
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
		Gizmos.DrawWireSphere(position, radius);
	}

	public void DetectColliders()
	{
		StartCoroutine(AttackCoroutine());

		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(circleOrigin.position, radius);
		foreach (Collider2D enemy in hitEnemies)
		{
			enemy.GetComponent<EnemyHealth>()?.TakeDamage(10);
			print(hitEnemies[0].gameObject.name);
		}

	}


	public IEnumerator AttackCoroutine()
	{
			yield return new WaitForSeconds(0.4f);
			isAttacking = false;

	}
}
