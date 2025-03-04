using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class WebFloor : MonoBehaviour
{
	[SerializeField] private float slowMultiplier = 0.6f; // Hýz çarpaný (0.6 = %60 hýz)
	private HashSet<GameObject> affectedEntities = new HashSet<GameObject>(); // Ýçeride olan nesneleri takip etmek için

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
			if (playerMovement != null)
			{
				ApplySlowEffect(playerMovement, slowMultiplier);
				affectedEntities.Add(collision.gameObject);
			}
		}
		else if (collision.CompareTag("Enemy"))
		{
			EnemyBase enemyBase = collision.GetComponent<EnemyBase>();
			if (enemyBase != null)
			{
				ApplySlowEffect(enemyBase, slowMultiplier);
				affectedEntities.Add(collision.gameObject);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (affectedEntities.Contains(collision.gameObject))
		{
			if (collision.CompareTag("Player"))
			{
				PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
				if (playerMovement != null)
				{
					RemoveSlowEffect(playerMovement, slowMultiplier);
				}
			}
			else if (collision.CompareTag("Enemy"))
			{
				EnemyBase enemyBase = collision.GetComponent<EnemyBase>();
				if (enemyBase != null)
				{
					RemoveSlowEffect(enemyBase, slowMultiplier);
				}
			}
			affectedEntities.Remove(collision.gameObject);
		}
	}

	private void ApplySlowEffect(PlayerMovement player, float multiplier)
	{
		player.movementSpeed *= multiplier;
	}

	private void ApplySlowEffect(EnemyBase enemy, float multiplier)
	{
		//enemy.maxSpeed *= multiplier;
		enemy.SetSpeedProduct(multiplier);
	}

	private void RemoveSlowEffect(PlayerMovement player, float multiplier)
	{
		player.movementSpeed /= multiplier; // Hýzý eski haline getir
	}

	private void RemoveSlowEffect(EnemyBase enemy, float multiplier)
	{
		enemy.SetSpeedProduct(1 / multiplier);
	}
}
