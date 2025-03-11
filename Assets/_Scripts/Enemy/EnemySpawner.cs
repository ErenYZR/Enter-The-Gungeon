using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private GameObject[] enemyPrefabs; // Spawn edilecek düþmanlar
	[SerializeField] private RoomController roomController;

	private void Start()
	{
		roomController = GetComponentInParent<RoomController>(); // Odanýn RoomController'ýný al
	}

	public void SpawnEnemy()
	{
		if (enemyPrefabs.Length == 0) return;

		GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
		GameObject spawnedEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

		EnemyBase enemy = spawnedEnemy.GetComponent<EnemyBase>();
		if (enemy != null)
		{
			enemy.SetRoomController(roomController);
		}
	}
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private float spawnRate = 1f;

	[SerializeField] private GameObject[] enemyPrefabs;

	[SerializeField] private bool canSpawn = false;

	private Vector2 distanceToPlayer;

	private SpriteRenderer spriteRenderer;

	[SerializeField] private float detectionRadius;

	private void Start()
	{
		StartCoroutine(nameof(Spawner));
		spriteRenderer = GetComponent<SpriteRenderer>();
		
	}

	private void Update()
	{
		if (GameObject.FindGameObjectWithTag("Player"))
		{
			distanceToPlayer = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
		}


		if (distanceToPlayer.magnitude < detectionRadius)
		{
			canSpawn = true;
			spriteRenderer.color = Color.black;
		}
		else
		{
			canSpawn = false;
			spriteRenderer.color = Color.white;
		}
	}

	private IEnumerator Spawner()
	{
		WaitForSeconds wait = new WaitForSeconds(spawnRate);

		while (true)
		{
			yield return wait;

			if (canSpawn)
			{
				int rand = Random.Range(0, enemyPrefabs.Length);
				GameObject enemyToSpawn = enemyPrefabs[rand];
				Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
			}
		}
	}
}*/
