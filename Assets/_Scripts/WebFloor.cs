using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebFloor : MonoBehaviour
{
	[SerializeField] private float slowMultiplier = 0.6f;
	Rigidbody2D rb;
	PlayerMovement playerMovement;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")){
			playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
			playerMovement.movementSpeed *= slowMultiplier;
		}

		if (collision.CompareTag("Enemy"))
		{
		//enemyMove.MoveSpeed *= slowMultiplier;
		}

		print(collision.gameObject.name);

	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
			playerMovement.movementSpeed *= (1 / slowMultiplier);
		}

		if (collision.CompareTag("Enemy"))
		{
			//enemyMove.MoveSpeed *= (1/ slowMultiplier);
		}
	}
}
