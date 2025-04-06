using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] private int maxHealth = 3;
	[SerializeField] private int currentHealth;
	private Rigidbody2D rb;
	private bool isDead = false;
	private EnemyBase enemyBase;
	private bool invincible = false;
	private float invincibilityDuration = 0.05f;
	private SpriteRenderer spriteRenderer;
	public Color currentColor;

	void Start()
	{
		currentHealth = maxHealth;
		rb = GetComponent<Rigidbody2D>();
		enemyBase = GetComponent<EnemyBase>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		currentColor = spriteRenderer.color;
	}

	private void Update()
	{
		
	}

	public void TakeDamage(int damage)
	{
		if (isDead /*|| invincible*/) return;//ölünce tekrar çaðýrmasýn diye
		currentHealth -= damage;

		if (currentHealth <= 0)
		{
			Die();
		}
		else
		{
			StartCoroutine(InvincibilityCoroutine());
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
		if (isDead) return;
		isDead = true;
		GetComponent<EnemyDropSystem>()?.TryDropItem();
		enemyBase?.roomController?.EnemyDefeated();
		print("Öldü" + gameObject.name);
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

	private IEnumerator InvincibilityCoroutine()
	{
		invincible = true;
		currentColor.a = 0.5f;
		spriteRenderer.color = currentColor;
		yield return new WaitForSeconds(invincibilityDuration);
		invincible = false;
		currentColor.a = 1f;
		spriteRenderer.color = currentColor;
	}
}
