using UnityEngine;
using Pathfinding;

public class AIAgent : MonoBehaviour
{
	private AIPath path;
	[SerializeField] private float moveSpeed;
	[SerializeField] private Transform target;

	private void Start()
	{
		path = GetComponent<AIPath>();
	}

	private void Update()
	{
		if (GameObject.FindGameObjectWithTag("Player"))
		{
			target = GameObject.FindGameObjectWithTag("Player").transform;
			path.destination = target.position;
		}

		path.maxSpeed = moveSpeed;



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
			Destroy(gameObject);
			Destroy(collision.gameObject);
		}
	}

}
