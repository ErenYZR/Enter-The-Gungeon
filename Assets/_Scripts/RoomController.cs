using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
	[SerializeField] private GameObject[] doors; // Odadaki kapýlar
	[SerializeField] private EnemySpawner[] enemySpawners; // Spawner noktalarý
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
			door.SetActive(true); // Kapýlarý kapat
		}
	}

	private void OpenDoors()
	{
		foreach (GameObject door in doors)
		{
			door.SetActive(false); // Kapýlarý aç
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
		enemiesAlive--;

		if (enemiesAlive <= 0)
		{
			OpenDoors(); // Tüm düþmanlar öldüðünde kapýlarý aç
		}
	}

	private void Update()
	{
		print(enemiesAlive);
	}
}
