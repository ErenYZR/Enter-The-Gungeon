using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DynamicInventoryDisplay : InventoryDisplay
{
	[SerializeField] protected InventorySlot_UI slotPrefab;
	protected override void Start()
	{
		base.Start();
	}


	public void RefreshDynamicInventory(InventorySystem invToDisplay)
	{
		ClearSlot();
		inventorySystem = invToDisplay;
		AssignSlot(invToDisplay);
	}

	public override void AssignSlot(InventorySystem invToDisplay)
	{
		slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

		if (invToDisplay == null) return;

		for (int i = 0; i < invToDisplay.InventorySize; i++)
		{
			var uiSlot = Instantiate(slotPrefab, transform);
			SlotDictionary.Add(uiSlot, invToDisplay.InventorySlots[i]);
			uiSlot.Init(invToDisplay.InventorySlots[i]);
			uiSlot.UpdateUISlot();
		}
	}

	private void ClearSlot()
	{
		foreach (var item in transform.Cast<Transform>())
		{
			Destroy(item.gameObject);
		}

		if (slotDictionary != null) slotDictionary.Clear();
	}
}
