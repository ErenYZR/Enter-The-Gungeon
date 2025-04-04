using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
	public static CraftingManager Instance { get; private set; }
	[SerializeField] private List<CraftingRecipe> recipes;

	private void Awake()
	{
		Instance = this;
	}
	public List<CraftingRecipe> GetAvailableRecipes()
	{
		return recipes;
	}

	public void TryCraftItem(CraftingRecipe recipe)
	{
		// Oyuncunun envanterini al
		PlayerInventoryHolder playerInventory = FindObjectOfType<PlayerInventoryHolder>();
		if (playerInventory == null)
		{
			Debug.LogWarning("Oyuncu envanteri bulunamadý!");
			return;
		}

		// Gerekli malzemeleri kontrol et
		foreach (var ingredient in recipe.Ingredients)
		{
			if (!playerInventory.HasItem(ingredient.Item, ingredient.Amount))
			{
				Debug.Log("Gerekli malzemeler yok!");
				return;
			}
		}

		// Malzemeleri tüket
		foreach (var ingredient in recipe.Ingredients)
		{
			playerInventory.RemoveItem(ingredient.Item, ingredient.Amount);
		}


		if (recipe.OutputItem is WeaponData weaponData)//silah üretildiyse silah envanterine yolla
		{
			// Silahý doðrudan oyuncuya ver
			PlayerWeaponManager weaponManager = FindObjectOfType<PlayerWeaponManager>();
			if (weaponManager != null)
			{
				weaponManager.AddNewWeapon(weaponData);
				Debug.Log($"{recipe.recipeName} üretildi ve silah olarak eklendi!");
			}
			else
			{
				Debug.LogWarning("PlayerWeaponManager bulunamadý!");
			}
		}
		else
		{
			// Normal eþya ise envantere ekle
			bool success = playerInventory.AddToInventory(recipe.OutputItem, recipe.OutputAmount);

			if (success)
				Debug.Log($"{recipe.recipeName} üretildi!");
			else
				Debug.LogWarning("Üretilen eþya envantere eklenemedi!");
		}



		/*bool success = playerInventory.AddToInventory(recipe.OutputItem, recipe.OutputAmount);

		if (success)
		{
			Debug.Log($"{recipe.recipeName} üretildi!");
		}
		else
		{
			Debug.LogWarning("Üretilen eþya envantere eklenemedi!");
		}*/

		// Üretilen eþyayý oyuncunun envanterine ekle
		//playerInventory.AddItem(recipe.OutputItem, recipe.OutputAmount);
		//Debug.Log($"{recipe.recipeName} üretildi!");		
	}
}