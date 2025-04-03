using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InventoryUIController : MonoBehaviour
{
    [FormerlySerializedAs("chestPanel")]public DynamicInventoryDisplay inventoryPanel;
	public DynamicInventoryDisplay playerBackpackPanel;
	public CraftingUI craftingPanel;

	private void Awake()
	{
		inventoryPanel.gameObject.SetActive(false);
		playerBackpackPanel.gameObject.SetActive(false);
		craftingPanel.gameObject.SetActive(false);
	}
	private void OnEnable()
	{
		InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
		PlayerInventoryHolder.OnPlayerInventoryDisplayRequested += DisplayPlayerInventory;
		CraftingStation.OnCraftingUIRequested += DisplayCraftingUI;
	}

	private void OnDisable()
	{
		InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
		PlayerInventoryHolder.OnPlayerInventoryDisplayRequested -= DisplayPlayerInventory;
		CraftingStation.OnCraftingUIRequested -= DisplayCraftingUI;
	}

	void Update()
    {
		if (inventoryPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) 
			inventoryPanel.gameObject.SetActive(false);

		if (playerBackpackPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) 
			playerBackpackPanel.gameObject.SetActive(false);

		if (craftingPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
			craftingPanel.gameObject.SetActive(false);
	}

	void DisplayInventory(InventorySystem invToDisplay,int offset)
	{
		inventoryPanel.gameObject.SetActive(true);
		inventoryPanel.RefreshDynamicInventory(invToDisplay,offset);
	}


	void DisplayPlayerInventory(InventorySystem invToDisplay, int offset)
	{
		playerBackpackPanel.gameObject.SetActive(true);
		playerBackpackPanel.RefreshDynamicInventory(invToDisplay, offset);
	}

	void DisplayCraftingUI()
	{
		craftingPanel.gameObject.SetActive(true);
		craftingPanel.RefreshCraftingUI();
	}
}
