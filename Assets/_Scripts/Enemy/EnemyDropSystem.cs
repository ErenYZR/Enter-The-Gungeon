using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItem
{
	public GameObject itemPrefab; // D��ecek e�yan�n prefab'�
	[Range(0f, 1f)]
	public float dropChance; // 0 ile 1 aras�nda bir ihtimal
}

public class EnemyDropSystem : MonoBehaviour
{
	[SerializeField] private List<DropItem> dropItems;

	public void TryDropItem()
	{
		foreach (var item in dropItems)
		{
			if (Random.value <= item.dropChance) // 0 ile 1 aras�nda rastgele de�er
			{
				Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
				break; // Bir e�ya d��t���nde di�erlerini kontrol etmeyi b�rak
			}
		}
	}
}
