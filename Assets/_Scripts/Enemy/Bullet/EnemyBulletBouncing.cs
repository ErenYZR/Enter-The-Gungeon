using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingEnemyBullet : EnemyBullet
{
	[SerializeField] private int maxBounces = 3; // Maksimum sekme say�s�
	private int bounceCount = 0;//Kod i�e yaram�yor

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
		{
			if (bounceCount < maxBounces)
			{
				// �arp��ma normalini al
				Vector2 normal = collision.contacts[0].normal;

				// Mevcut y�n� normal �zerinden yans�t
				Vector2 newDirection = Vector2.Reflect(rb.velocity.normalized, normal);

				// Yeni y�n� uygula
				rb.velocity = newDirection * rb.velocity.magnitude;

				// A��y� g�ncelle
				float angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler(0, 0, angle);

				// Sekme say�s�n� art�r
				bounceCount++;
			}
			else
			{
				Destroy(gameObject); // Sekme limiti dolunca mermiyi yok et
			}
		}
	}
}
