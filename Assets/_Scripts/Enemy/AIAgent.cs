using UnityEngine;
using Pathfinding;
using System.IO;

public class AIAgent : MonoBehaviour
{
	private AIPath path;
	[SerializeField] private Transform target;
	private EnemyHealth enemyHealth;
	public int contactDamage = 2;


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
		}
		path.maxSpeed = 4;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			collision.GetComponent<Health>().TakeDamage(contactDamage);
			target = null;
		}
	}

}
