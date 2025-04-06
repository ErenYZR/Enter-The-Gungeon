using System.Collections;
using UnityEngine;

public class JumpingEnemy : EnemyBase
{
	public float jumpForce = 10f; // Z�plama kuvveti
	public float jumpRange = 2f;  // Oyuncuya ne kadar yakla��nca z�playacak
	public float jumpCooldown = 2f; // Z�plama sonras� bekleme s�resi
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

		yield return new WaitForSeconds(0.2f); // Hafif bekleme s�resi

		// Oyuncuya do�ru y�n belirle
		Vector2 jumpDirection = (target.position - transform.position).normalized;

		// Rigidbody'ye do�rudan h�z vererek z�plama yapt�r
		rb.velocity = jumpDirection * jumpForce;

		yield return new WaitForSeconds(0.5f); // Z�plama s�resi

		rb.velocity = Vector2.zero; // D��man� durdur

		yield return new WaitForSeconds(jumpCooldown); // Bekleme s�resi

		path.enabled = true; // AIPath tekrar �al��s�n
		path.maxSpeed = moveSpeed;

		yield return new WaitForSeconds(1);
		isJumping = false;
	}

	protected override void Attack()
	{
		Debug.Log("Jumping Enemy has hit the player!");
	}
}
