using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CraftingStation : MonoBehaviour
{
	[SerializeField] private GameObject craftingUI; // Crafting UI Paneli

	private bool isPlayerNearby = false; // Oyuncu yakýnda mý?

	private void Update()
	{
		if (isPlayerNearby && Keyboard.current.eKey.wasPressedThisFrame)
		{
			ToggleCraftingUI(true);
		}

		if (craftingUI.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
		{
			ToggleCraftingUI(false);
		}
	}

	public static UnityAction OnCraftingUIRequested;

	public UnityAction<IInteractable> OnInteractionComplete { get; set; }

	public void Interact(Interactor interactor, out bool interactSuccessful)
	{
		OnCraftingUIRequested?.Invoke();
		interactSuccessful = true;
	}

	public void EndInteraction()
	{
		// UI'yý kapatmaya gerek yok, oyuncu escape'e basýnca kapanacak.
	}

	private void ToggleCraftingUI(bool state)
	{
		craftingUI.SetActive(state);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			isPlayerNearby = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			isPlayerNearby = false;
		}
	}
}





/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CraftingStation : MonoBehaviour, IInteractable
{
	public static UnityAction OnCraftingUIRequested;

	public UnityAction<IInteractable> OnInteractionComplete { get; set; }

	public void Interact(Interactor interactor, out bool interactSuccessful)
	{
		OnCraftingUIRequested?.Invoke(); 
		interactSuccessful = true;
	}

	public void EndInteraction()
	{
		// UI'yý kapatmaya gerek yok, oyuncu escape'e basýnca kapanacak.
	}
}*/





/*using UnityEngine;

public class CraftingStation : MonoBehaviour
{
	public CraftingRecipe[] Recipes; // Crafting istasyonunda üretilebilecek tarifler

	public bool CanCraftItem(PlayerInventoryHolder playerInventory, CraftingRecipe recipe)
	{
		foreach (var ingredient in recipe.Ingredients)
		{
			if (!playerInventory.HasItem(ingredient.Item, ingredient.Amount))
			{
				return false; // Gerekli malzemeler eksik
			}
		}
		return true;
	}

	public void CraftItem(PlayerInventoryHolder playerInventory, CraftingRecipe recipe)
	{
		if (!CanCraftItem(playerInventory, recipe))
		{
			Debug.Log("Gerekli malzemeler eksik!");
			return;
		}

		// Malzemeleri envanterden çýkar
		foreach (var ingredient in recipe.Ingredients)
		{
			playerInventory.RemoveItem(ingredient.Item, ingredient.Amount);
		}

		// Yeni itemi ekle
		playerInventory.AddToInventory(recipe.OutputItem, recipe.OutputAmount);
		Debug.Log(recipe.OutputItem.DisplayName + " üretildi!");
	}
}*/
