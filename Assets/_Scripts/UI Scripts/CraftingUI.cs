using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
	[SerializeField] private Transform craftingSlotParent;
	[SerializeField] private CraftingSlot_UI craftingSlotPrefab;

	private List<CraftingSlot_UI> craftingSlots = new List<CraftingSlot_UI>();

	public void RefreshCraftingUI()
	{
		ClearCraftingSlots();

		var recipes = CraftingManager.Instance.GetAvailableRecipes();
		foreach (var recipe in recipes)
		{
			var slot = Instantiate(craftingSlotPrefab, craftingSlotParent);
			slot.SetRecipe(recipe);
			craftingSlots.Add(slot);
		}
	}

	private void ClearCraftingSlots()
	{
		foreach (var slot in craftingSlots)
		{
			Destroy(slot.gameObject);
		}
		craftingSlots.Clear();
	}
}
