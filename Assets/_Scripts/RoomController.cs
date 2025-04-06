using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
	[SerializeField] private Collider2D roomCollider;
	[SerializeField] private GameObject[] doors; // Odadaki kapýlar
	[SerializeField] private EnemySpawner[] enemySpawners; // Spawner noktalarý
	private int enemiesAlive = 0;
	private bool roomActivated = false;

	private void Awake()
	{
		roomCollider = GetComponent<BoxCollider2D>();
	}

	private void Start()
	{
		DetectObjects();
		OpenDoors();

	}

	private void DetectObjects()
	{
		List<GameObject> detectedDoors = new List<GameObject>();
		List<EnemySpawner> detectedSpawners = new List<EnemySpawner>();

		// Odanýn collider sýnýrlarý içinde kalan nesneleri bul
		Collider2D[] objectsInRoom = Physics2D.OverlapBoxAll(roomCollider.bounds.center, roomCollider.bounds.size, 0);

		foreach (Collider2D obj in objectsInRoom)
		{
			if (obj.CompareTag("Door")) // Kapýlarý algýla
			{
				detectedDoors.Add(obj.gameObject);
			}
			else if (obj.TryGetComponent(out EnemySpawner spawner)) // Spawnerlarý algýla
			{
				detectedSpawners.Add(spawner);
			}
		}

		// Algýlanan nesneleri diziye çevir ve ata
		doors = detectedDoors.ToArray();
		enemySpawners = detectedSpawners.ToArray();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !roomActivated)
		{
			//ActivateRoom();
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
		enemiesAlive = Mathf.Max(0, enemiesAlive - 1);

		if (enemiesAlive <= 0)
		{
			OpenDoors(); // Tüm düþmanlar öldüðünde kapýlarý aç
		}
	}
	public void TryActivateRoom()
	{
		if (!roomActivated)
		{
			ActivateRoom();
		}
	}


	private void Update()
	{
		print(enemiesAlive);
	}
}
