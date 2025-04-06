using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Obstacles : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{

        if (collision.gameObject.CompareTag("Bullet") || (collision.gameObject.CompareTag("EnemyBullet")))
		{
			Destroy(collision.gameObject);
		}
	}
}
