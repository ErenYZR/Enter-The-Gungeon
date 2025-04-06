using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder
{
	public static event Action OnInventoryUpdated;

	[SerializeField] private InventorySystem inventorySystem;

	public static UnityAction OnPlayerInventoryChanged;

	public static UnityAction<InventorySystem, int> OnPlayerInventoryDisplayRequested;
	private void Start()
	{
		SaveGameManager.data.playerInventory = new InventorySaveData(primaryInventorySystem);
	}

	protected override void LoadInventory(SaveData data)
	{
		//check the save data for this spesific
		if (data.playerInventory.InvSystem != null)
		{
			this.primaryInventorySystem = data.playerInventory.InvSystem;
            OnPlayerInventoryChanged?.Invoke();
		}
	}

	void Update()
    {
		if (Keyboard.current.bKey.wasPressedThisFrame) OnPlayerInventoryDisplayRequested?.Invoke(primaryInventorySystem, offset);
    }

    public bool AddToInventory(InventoryItemData data, int amount)
    {
        if (primaryInventorySystem.AddToInventory(data, amount))
        {
			OnPlayerInventoryChanged?.Invoke();
            return true;
        }

        return false;
    }

	public bool HasItem(InventoryItemData data, int amount)
	{
		OnPlayerInventoryChanged?.Invoke();
		return primaryInventorySystem.HasItem(data, amount);
	}

	public void RemoveItem(InventoryItemData data, int amount)
	{
		primaryInventorySystem.RemoveItem(data, amount);
		OnPlayerInventoryChanged?.Invoke();
	}

}
