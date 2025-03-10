using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] private int maxHealth = 3;
	private int currentHealth;
	private Rigidbody2D rb;

	void Start()
	{
		currentHealth = maxHealth;
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;

		if (currentHealth <= 0)
		{
			Die();
		}
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

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Bullet"))
		{
			int damage = collision.GetComponent<Bullet>().damage;
			TakeDamage(damage);
			Destroy(collision.gameObject);
		}
	}
}
