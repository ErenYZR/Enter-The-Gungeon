using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
public class ItemPickUp : MonoBehaviour
{
	public float PickUpRadius = 1f;
	public InventoryItemData ItemData;

	private CircleCollider2D myCollider;

	private void Awake()
	{
		myCollider = GetComponent<CircleCollider2D>();
		myCollider.isTrigger = true;
		myCollider.radius = PickUpRadius;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var inventory = collision.transform.GetComponent<PlayerInventoryHolder>();
		if (!inventory) return;
		print("B");

		if (inventory.AddToInventory(ItemData, 1))
		{
			print("A");
			Destroy(this.gameObject);
		}
	}
}
