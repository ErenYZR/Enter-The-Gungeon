using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
	[SerializeField] private GameObject[] doors; // Odadaki kap�lar
	[SerializeField] private EnemySpawner[] enemySpawners; // Spawner noktalar�
	private int enemiesAlive = 0;
	private bool roomActivated = false;


	private void Start()
	{
		OpenDoors();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !roomActivated)
		{
			ActivateRoom();
		}
	}

	private void ActivateRoom()
	{
		roomActivated = true;
		CloseDoors();
		SpawnEnemies();
	}

	private void CloseDoors()
	{
		foreach (GameObject door in doors)
		{
			door.SetActive(true); // Kap�lar� kapat
		}
	}

	private void OpenDoors()
	{
		foreach (GameObject door in doors)
		{
			door.SetActive(false); // Kap�lar� a�
		}
	}

	private void SpawnEnemies()
	{
		foreach (EnemySpawner spawner in enemySpawners)
		{
			spawner.SpawnEnemy();
			enemiesAlive++;
		}
	}

	public void EnemyDefeated()
	{
		enemiesAlive = Mathf.Max(0, enemiesAlive - 1);

		if (enemiesAlive <= 0)
		{
			OpenDoors(); // T�m d��manlar �ld���nde kap�lar� a�
		}
	}

	private void Update()
	{
		print(enemiesAlive);
	}
}
