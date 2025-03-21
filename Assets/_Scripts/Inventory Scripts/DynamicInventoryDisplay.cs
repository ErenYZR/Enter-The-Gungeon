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


	public void RefreshDynamicInventory(InventorySystem invToDisplay, int offset)
	{
		ClearSlot();
		inventorySystem = invToDisplay;
		if(inventorySystem != null)	inventorySystem.OnInventorySlotChanged += UpdateSlot;
		AssignSlot(invToDisplay,offset);
	}

	public override void AssignSlot(InventorySystem invToDisplay, int offset)
	{
		slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

		if (invToDisplay == null) return;

		for (int i = offset; i < invToDisplay.InventorySize; i++)
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

	private void OnDisable()
	{
		if (inventorySystem != null) inventorySystem.OnInventorySlotChanged -= UpdateSlot;
	}
}
