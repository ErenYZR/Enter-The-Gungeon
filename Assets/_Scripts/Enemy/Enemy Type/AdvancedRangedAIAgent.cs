using UnityEngine;
using Pathfinding;

public class AdvancedRangedAIAgent : MonoBehaviour
{
	private AIPath path;
	[SerializeField] private float moveSpeed;
	[SerializeField] private float rotateSpeed;
	[SerializeField] private Transform target;
	[SerializeField] private float distanceToShoot = 6f;
	[SerializeField] private float distanceToStop = 3f;

	[SerializeField] private Transform firingPoint;
	[SerializeField] private float fireRate;
	private float timeToFire;
	[SerializeField] GameObject enemyBulletPrefab;
	public LayerMask obstacles;
	private EnemyHealth enemyHealth;


	private void Start()
	{
		path = GetComponent<AIPath>();
		enemyHealth = GetComponent<EnemyHealth>();
	}

	private void Update()
	{
		if (GameObject.FindGameObjectWithTag("Player"))
		{
			target = GameObject.FindGameObjectWithTag("Player").transform;
			path.destination = target.position;
			if (canShoot()) RotateTowardsTarget();


		}

		if (path.remainingDistance >= distanceToStop)//düþmanýn oyuncuya yaklaþýnca durmasýný saðlayan kod
		{
			path.maxSpeed = moveSpeed;
		}
		else
		{
			path.maxSpeed = 0;
		}

		Shoot();

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Destroy(collision.gameObject);
			target = null;
		}
		else if (collision.gameObject.CompareTag("Bullet"))
		{
			//enemyHealth.TakeDamage(1);
			Destroy(collision.gameObject);
		}
	}

	private void Shoot()
	{
		if (timeToFire <= 0f && canShoot())
		{
			Instantiate(enemyBulletPrefab, firingPoint.position, transform.rotation);
			timeToFire = fireRate;
		}
		else
		{
			timeToFire -= Time.deltaTime;
		}
	}

	private void RotateTowardsTarget()
	{
		Vector2 targetDirection = target.position - transform.position;
		float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
		Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
		transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
	}


	private bool canShoot()
	{
		Vector2 A = target.position - transform.position;
		RaycastHit2D raycastHit2D = Physics2D.Linecast(transform.position, target.position, obstacles);
		if (raycastHit2D.collider == null && path.remainingDistance <= distanceToShoot) return true;
		else return false;
	}


}
