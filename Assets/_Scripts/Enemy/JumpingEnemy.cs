using System.Collections;
using UnityEngine;

public class JumpingEnemy : EnemyBase
{
	public float jumpForce = 10f; // Zýplama kuvveti
	public float jumpRange = 2f;  // Oyuncuya ne kadar yaklaþýnca zýplayacak
	public float jumpCooldown = 2f; // Zýplama sonrasý bekleme süresi
	private bool isJumping = false;

	protected override void Update()
	{
		FindTarget();

		float distance = Vector2.Distance(transform.position, target.position);

		if (!isJumping)
		{
			if (distance <= jumpRange)
			{
				StartCoroutine(JumpAttack());
			}
			else
			{
				path.maxSpeed = moveSpeed;
			}
		}
	}

	private IEnumerator JumpAttack()
	{
		isJumping = true;
		path.enabled = false; // AIPath hareketini kapat

		yield return new WaitForSeconds(0.2f); // Hafif bekleme süresi

		// Oyuncuya doðru yön belirle
		Vector2 jumpDirection = (target.position - transform.position).normalized;

		// Rigidbody'ye doðrudan hýz vererek zýplama yaptýr
		rb.velocity = jumpDirection * jumpForce;

		yield return new WaitForSeconds(0.5f); // Zýplama süresi

		rb.velocity = Vector2.zero; // Düþmaný durdur

		yield return new WaitForSeconds(jumpCooldown); // Bekleme süresi

		path.enabled = true; // AIPath tekrar çalýþsýn
		path.maxSpeed = moveSpeed;

		yield return new WaitForSeconds(1);
		isJumping = false;
	}

	protected override void Attack()
	{
		Debug.Log("Jumping Enemy has hit the player!");
	}
}
