using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] public float speed = 10f;

	[SerializeField] public float lifetime = 3f;

	public int damage = 1;

	private Rigidbody2D rb;


	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		Destroy(gameObject,lifetime);
	}

	private void FixedUpdate()
	{
		rb.velocity = transform.up * speed;
	}

}
