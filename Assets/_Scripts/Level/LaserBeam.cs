using UnityEngine;

public class LaserBeam : MonoBehaviour
{
	[SerializeField] private float laserDuration = 0.5f;
	[SerializeField] private int damage = 20;
	[SerializeField] private LineRenderer lineRenderer;
	[SerializeField] private LayerMask layer;

	private Vector2 startPosition;
	private Vector2 endPosition;


	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}
	public void SetLaserDirection(Vector2 direction, float range)
	{
		startPosition = transform.position;
		RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, range, layer);

		if (hit.collider != null)
		{
			endPosition = hit.point;
			DealDamage(hit.collider);
		}
		else
		{
			endPosition = startPosition + direction * range;
		}

		DrawLaser();
		Destroy(gameObject, laserDuration);
	}

	private void DrawLaser()
	{
		lineRenderer.SetPosition(0, startPosition);
		lineRenderer.SetPosition(1, endPosition);
	}

	private void DealDamage(Collider2D target)
	{
		if (target.TryGetComponent(out Health targetHealth))
		{
			targetHealth.TakeDamage(damage);
		}
		else if (target.TryGetComponent(out EnemyHealth targetEnemyHealth))
		{
			targetEnemyHealth.TakeDamage(damage);
		}
	}
}
