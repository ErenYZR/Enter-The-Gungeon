using UnityEngine;
using Pathfinding;

public class ShotgunAIAgent : MonoBehaviour
{
	private AIPath path;
	[SerializeField] private float moveSpeed;
	[SerializeField] private float rotateSpeed;
	[SerializeField] private Transform target;
	[SerializeField] private float distanceToShoot = 6f;
	[SerializeField] private float distanceToStop = 3f;

	[SerializeField] private Transform firingPoint;
	[SerializeField] private float fireRate;
	[SerializeField] private float timeToFire;
	[SerializeField] GameObject enemyBulletPrefab;
	public LayerMask obstacles;
	private EnemyHealth enemyHealth;

	[SerializeField] private int pelletCount;
	[SerializeField] private float spreadAngle;


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

			path.maxSpeed = 4;
		}
		else
		{
			path.maxSpeed = 0;
		}

		Shoot();
		if (canShoot()) print("Ateþ et be");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Destroy(collision.gameObject);
			target = null;
		}
	}

	private void Shoot()
	{
		if (timeToFire <= 0f && canShoot())
		{
			for (int i = 0; i < pelletCount; i++)
			{
				float spread = Random.Range(-spreadAngle / 2, spreadAngle / 2);
				Quaternion bulletRotation = Quaternion.Euler(0, 0, firingPoint.rotation.eulerAngles.z + spread);
				Instantiate(enemyBulletPrefab, firingPoint.position, bulletRotation);
			}
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
		if (target != null)
		{
			RaycastHit2D Hit = Physics2D.Linecast(transform.position, target.position, obstacles);
			if (Hit.collider == null && path.remainingDistance <= distanceToShoot) return true;
			else return false;
		}
		else return false;
	}
}
