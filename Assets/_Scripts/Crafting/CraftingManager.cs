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
			Debug.LogWarning("Oyuncu envanteri bulunamad�!");
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

		// Malzemeleri t�ket
		foreach (var ingredient in recipe.Ingredients)
		{
			playerInventory.RemoveItem(ingredient.Item, ingredient.Amount);
		}


		if (recipe.OutputItem is WeaponData weaponData)//silah �retildiyse silah envanterine yolla
		{
			// Silah� do�rudan oyuncuya ver
			PlayerWeaponManager weaponManager = FindObjectOfType<PlayerWeaponManager>();
			if (weaponManager != null)
			{
				weaponManager.AddNewWeapon(weaponData);
				Debug.Log($"{recipe.recipeName} �retildi ve silah olarak eklendi!");
			}
			else
			{
				Debug.LogWarning("PlayerWeaponManager bulunamad�!");
			}
		}
		else
		{
			// Normal e�ya ise envantere ekle
			bool success = playerInventory.AddToInventory(recipe.OutputItem, recipe.OutputAmount);

			if (success)
				Debug.Log($"{recipe.recipeName} �retildi!");
			else
				Debug.LogWarning("�retilen e�ya envantere eklenemedi!");
		}



		/*bool success = playerInventory.AddToInventory(recipe.OutputItem, recipe.OutputAmount);

		if (success)
		{
			Debug.Log($"{recipe.recipeName} �retildi!");
		}
		else
		{
			Debug.LogWarning("�retilen e�ya envantere eklenemedi!");
		}*/

		// �retilen e�yay� oyuncunun envanterine ekle
		//playerInventory.AddItem(recipe.OutputItem, recipe.OutputAmount);
		//Debug.Log($"{recipe.recipeName} �retildi!");		
	}
}