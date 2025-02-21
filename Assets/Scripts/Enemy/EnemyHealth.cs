using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] private int maxHealth = 3;
	private int currentHealth;
	private Rigidbody2D rb;

	[SerializeField] private float knockbackForce = 5f;
	[SerializeField] private float knockbackDuration = 0.2f;
	private bool isKnockedBack = false;

	void Start()
	{
		currentHealth = maxHealth;
		rb = GetComponent<Rigidbody2D>();
	}

	public void TakeDamage(int damage, Vector2 hitDirection)
	{
		currentHealth -= damage;

		if (currentHealth <= 0)
		{
			Die();
		}

		//if (isKnockedBack) return;
		//StartCoroutine(Knockback(hitDirection));
	}

	public void Heal(int healAmount)
	{
		currentHealth += healAmount;

		if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
	}

	private void Die()
	{
		Destroy(gameObject);
	}

	private IEnumerator Knockback(Vector2 hitDirection)
	{
		isKnockedBack = true;
		rb.velocity = -hitDirection.normalized * knockbackForce;
		print("B");
		yield return new WaitForSeconds(knockbackDuration);
		print("C");
		rb.velocity = Vector2.zero;
		isKnockedBack = false;
	}
}
