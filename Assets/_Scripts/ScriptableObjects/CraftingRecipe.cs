using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
	public string recipeName;
	public InventoryItemData OutputItem; // Üretilen eþya
	public int OutputAmount = 1; // Kaç tane üretilecek

	[System.Serializable]
	public struct RecipeIngredient
	{
		public InventoryItemData Item; // Gerekli malzeme
		public int Amount; // Gerekli miktar
	}

	public RecipeIngredient[] Ingredients; // Tarif için gerekli malzemeler
}
