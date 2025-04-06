using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	[SerializeField] protected float speed = 10f;

	[Range(1, 10)]
	[SerializeField] private float lifetime = 3f;

	private int damage = 1; 

	protected Rigidbody2D rb;


	public virtual void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		Destroy(gameObject,lifetime);
	}

	public virtual void FixedUpdate()
	{
		rb.velocity = transform.up * speed;
	}

	public void SetDamage(int newDamage)
	{
		damage = newDamage;
	}

	public void SetSpeed(float newSpeed)
	{
		speed = newSpeed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			collision.GetComponent<Health>().TakeDamage(damage);
			Destroy(gameObject);
		}
	}
}
