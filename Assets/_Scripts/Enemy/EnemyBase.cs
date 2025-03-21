using UnityEngine;
using Pathfinding;

public abstract class EnemyBase : MonoBehaviour
{
	protected AIPath path;
	[SerializeField] private float moveSpeed;
	[SerializeField] private float rotateSpeed;
	protected Transform target;
	[SerializeField] protected int contactDamage;
	[SerializeField] private float distanceToShoot = 6f;
	[SerializeField] private float distanceToStop = 3f;
	[SerializeField] private LayerMask obstacles;
	public RoomController roomController;

	protected virtual void Awake()
	{

	}


	protected virtual void Start()
	{
		path = GetComponent<AIPath>();
		target = GameObject.FindGameObjectWithTag("Player")?.transform;

		//if(roomController != null) roomController
	}

	protected virtual void Update()
	{
		if(target == null)
		{
			target = GameObject.FindGameObjectWithTag("Player")?.transform;
			if (target == null) return;
		}
			path.destination = target.position;
			if (canShoot()) RotateTowardsTarget();
		
		if (path.remainingDistance >= distanceToStop)//d��man�n oyuncuya yakla��nca durmas�n� sa�layan kod
		{
			path.maxSpeed = moveSpeed;
		}
		else
		{
			path.maxSpeed = 0;
		}

		Attack();
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			collision.GetComponent<Health>().TakeDamage(contactDamage);
		}
	}

	protected abstract void Attack();
	private void RotateTowardsTarget()
	{
		Vector2 targetDirection = target.position - transform.position;
		float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
		Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
		transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
	}


	public bool canShoot()
	{
		if (target != null)
		{
			RaycastHit2D Hit = Physics2D.Linecast(transform.position, target.position, obstacles);
			if (Hit.collider == null && path.remainingDistance <= distanceToShoot) return true;
			else return false;
		}
		else return false;
	}

	public void SetSpeedProduct(float speed)
	{
		moveSpeed *= speed;
	}

	public void SetRoomController(RoomController controller)
	{
		roomController = controller;
	}
}
