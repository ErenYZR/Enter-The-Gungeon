using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;

	private void Awake()
	{
		inventoryPanel.gameObject.SetActive(false);
	}
	private void OnEnable()
	{
		InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
	}

	private void OnDisable()
	{
		InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
	}

	void Update()
    {
		if (Keyboard.current.bKey.wasPressedThisFrame) DisplayInventory(new InventorySystem(Random.Range(20,30)));

		if (inventoryPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) inventoryPanel.gameObject.SetActive(false);
    }

	void DisplayInventory(InventorySystem invToDisplay)
	{
		inventoryPanel.gameObject.SetActive(true);
		inventoryPanel.RefreshDynamicInventory(invToDisplay);
	}
}
