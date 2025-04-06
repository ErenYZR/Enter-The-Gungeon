using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingEnemyBullet : EnemyBullet
{
	[SerializeField] private int maxBounces = 3; // Maksimum sekme sayýsý
	private int bounceCount = 0;//Kod iþe yaramýyor

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
		{
			if (bounceCount < maxBounces)
			{
				// Çarpýþma normalini al
				Vector2 normal = collision.contacts[0].normal;

				// Mevcut yönü normal üzerinden yansýt
				Vector2 newDirection = Vector2.Reflect(rb.velocity.normalized, normal);

				// Yeni yönü uygula
				rb.velocity = newDirection * rb.velocity.magnitude;

				// Açýyý güncelle
				float angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler(0, 0, angle);

				// Sekme sayýsýný artýr
				bounceCount++;
			}
			else
			{
				Destroy(gameObject); // Sekme limiti dolunca mermiyi yok et
			}
		}
	}
}
