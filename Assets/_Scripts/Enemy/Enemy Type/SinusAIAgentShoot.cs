using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusAIAgentShoot : EnemyShooterBase
{
	public float frequency = 10f;
	public float amplitude = 0.5f;

	private float time;

	protected override void Shoot()
	{
		base.Start();
	}

	/*private void Update()
	{
		time += Time.deltaTime;
	}*/

	private void FixedUpdate()
	{
		time += Time.fixedDeltaTime;

		//Vector2 forwardMove = transform.up
	}
}
