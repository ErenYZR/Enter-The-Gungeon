using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleWeapon : Weapon
{
	public MeeleWeaponData meeleWeaponData;
	[SerializeField] private Animator animator;

	[SerializeField] protected int currentDurability;
	public Transform circleOrigin;
	public float radius;

	private HashSet<EnemyHealth> hitEnemies = new HashSet<EnemyHealth>(); // Hasar alan düþmanlar
	private bool isAttacking = false;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		currentDurability = meeleWeaponData.durability;

		if (meeleWeaponData.animatiorController != null)
		{
			animator.runtimeAnimatorController = meeleWeaponData.animatiorController;
		}
	}

	public override void Fire()
	{
		isAttacking = true;
		animator.SetTrigger("Attack");
		currentDurability--;

		// Saldýrý süresi boyunca çarpýþma tespiti yap
		StartCoroutine(AttackCoroutine());
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
		Collider2D[] colliders = Physics2D.OverlapCircleAll(circleOrigin.position, radius);

		foreach (Collider2D collider in colliders)
		{
			if (collider.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
			{
				if (!hitEnemies.Contains(enemyHealth)) // Eðer düþman daha önce vurulmadýysa
				{
					hitEnemies.Add(enemyHealth);
					enemyHealth.TakeDamage(meeleWeaponData.damage); // Hasarý anýnda ver
					Debug.Log(enemyHealth.gameObject.name + " anýnda hasar aldý!");
				}
			}
		}
	}

	private IEnumerator AttackCoroutine()
	{
		yield return new WaitForSeconds(0.4f); // Saldýrý animasyonu süresi kadar bekle
		hitEnemies.Clear(); // HashSet'i temizle
		isAttacking = false;
	}
}
