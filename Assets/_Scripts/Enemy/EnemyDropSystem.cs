using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItem
{
	public GameObject itemPrefab; // Düþecek eþyanýn prefab'ý
	[Range(0f, 1f)]
	public float dropChance; // 0 ile 1 arasýnda bir ihtimal
}

public class EnemyDropSystem : MonoBehaviour
{
	[SerializeField] private List<DropItem> dropItems;

	public void TryDropItem()
	{
		foreach (var item in dropItems)
		{
			if (Random.value <= item.dropChance) // 0 ile 1 arasýnda rastgele deðer
			{
				Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
				break; // Bir eþya düþtüðünde diðerlerini kontrol etmeyi býrak
			}
		}
	}
}
