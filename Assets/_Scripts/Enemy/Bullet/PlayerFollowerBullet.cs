using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowerBullet : EnemyBullet
{
	protected Transform target;


	public override void Start()
	{
		base.Start();
		target = GameObject.FindGameObjectWithTag("Player")?.transform;
	}

	public override void FixedUpdate()
	{
		Vector2 destination = target.position - rb.transform.position;
		rb.velocity = destination.normalized * speed;
	}
}
